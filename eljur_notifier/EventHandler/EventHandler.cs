using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Data.SqlClient;
using eljur_notifier;
using eljur_notifier.FirebirdNS;
using eljur_notifier.MsDbNS;
using eljur_notifier.MsDbNS.CheckerNS;
using eljur_notifier.AppCommon;
using eljur_notifier.MsDbNS.CatcherNS;

namespace eljur_notifier.EventHandlerNS
{
    class EventHandlerEljur
    {
        internal protected Message message { get; set; }
        internal protected Config config { get; set; }
        internal protected MsDb msDb { get; set; }
        internal protected Firebird firebird { get; set; }
        internal protected TimeChecker timeChecker { get; set; }
        
        public EventHandlerEljur(Config Config, MsDb MsDb, Firebird Firebird, TimeChecker timeChecker)
        {
            this.message = new Message();
            this.config = Config;
            this.msDb = MsDb;
            this.firebird = Firebird;
            this.timeChecker = timeChecker;

        }

        public void WrapperToActionWithMsDb(Action actionWithMsDb, String TaskName)
        {
            using (msDb.dbcon = new SqlConnection(config.ConStrMsDB))
            {
                if (msDb.IsDbExist(msDb.dbcon, "Task " + TaskName))
                {
                    actionWithMsDb();
                }
                else
                {
                    try
                    {
                        msDb.dbcon = new SqlConnection(config.ConStrMsDB);
                    }
                    catch (Exception ex)
                    {
                        message.Display("Cannot connect to MsDb from Task " + TaskName, "Fatal", ex);
                    }
                }
            }

        }

        public async Task GetDataFb(CancellationToken cancellationToken) 
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                DateTime startTime = DateTime.Now;
                List<object[]> curEvents = firebird.getStaffByTimeStamp(config);
                WrapperToActionWithMsDb(new Action(delegate
                {
                    msDb.CheckEventsDb(curEvents);
                }), "GetDataFb");
                TimeSpan deltaTime = DateTime.Now - startTime;
                TimeSpan IntervalRequest = TimeSpan.FromMilliseconds(config.IntervalRequest);
                TimeSpan sleepTime = IntervalRequest - deltaTime;
                message.Display("sleepTime is: " + sleepTime.ToString(), "Trace");
                await Task.Delay(sleepTime);
            }
        }        

      

        public async Task CheckTimekMsDb(CancellationToken cancellationToken, Action actionAtMidnight)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                timeChecker.CheckTime(actionAtMidnight);
                await Task.Delay(1000);
            }        
        }

        public async Task CatchEventFirstPass(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                WrapperToActionWithMsDb(new Action(delegate
                {
                    MsDbCatcherFirstPass msDbCatcherFirstPass = new MsDbCatcherFirstPass(config, msDb);
                    msDbCatcherFirstPass.catchFirstPass();
                }), "CatchEventFirstPass");
                await Task.Delay(10000);
            }
        }

        public async Task CatchEventLastPass(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                WrapperToActionWithMsDb(new Action(delegate
                {
                    MsDbCatcherLastPass msDbCatcherLastPass = new MsDbCatcherLastPass(config, msDb);
                    msDbCatcherLastPass.catchLastPass();
                }), "CatchEventLastPass");
                await Task.Delay(10000);
            }
        }






    }
}
