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
    public class EventHandlerEljur
    {
        internal protected Message message { get; set; }
        internal protected Config config { get; set; }
        internal protected MsDb msDb { get; set; }
        internal protected Firebird firebird { get; set; }
        internal protected TimeChecker timeChecker { get; set; }
        
        public EventHandlerEljur(Config Config, MsDb MsDb, Firebird Firebird)
        {
            this.message = new Message();
            this.config = Config;
            this.msDb = MsDb;
            this.firebird = Firebird;
            this.timeChecker = new TimeChecker(config);

        }

        public void WrapperToActionWithMsDb(Action actionWithMsDb, String TaskName, Action actionBeforeClosing)
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
                        message.Display("Cannot connect to MsDb from Task " + TaskName, "Error", ex);
                    }
                }
            }

        }

        public async Task GetDataFb(CancellationToken cancellationToken, Action actionBeforeClosing) 
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    DateTime startTime = DateTime.Now;
                    List<object[]> curEvents = firebird.getStaffByTimeStamp(config);
                    WrapperToActionWithMsDb(new Action(delegate
                    {
                        msDb.CheckEventsDb(curEvents);
                    }), "GetDataFb", actionBeforeClosing);
                    TimeSpan deltaTime = DateTime.Now - startTime;
                    TimeSpan IntervalRequest = TimeSpan.FromMilliseconds(config.IntervalRequest);
                    TimeSpan sleepTime = IntervalRequest - deltaTime;
                    message.Display("sleepTime is: " + sleepTime.ToString(), "Trace");
                    await Task.Delay(sleepTime);
                }
            }
            catch (Exception ex)
            {
                message.Display("An unprocessed error has occurred in Task GetDataFb. See log for more information.", "Error", ex);
            }
        }        

      

        public async Task CheckTimekMsDb(CancellationToken cancellationToken, Action actionAtMidnight, Action actionBeforeClosing)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    timeChecker.CheckTime(actionAtMidnight);
                    await Task.Delay(1000);
                }
            }
            catch (Exception ex)
            {
                message.Display("An unprocessed error has occurred in Task CheckTimekMsDb. See log for more information.", "Error", ex);
            }
        }

        public async Task CatchEventFirstPass(CancellationToken cancellationToken, Action actionBeforeClosing)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    WrapperToActionWithMsDb(new Action(delegate
                    {
                        MsDbCatcherFirstPass msDbCatcherFirstPass = new MsDbCatcherFirstPass(config, msDb);
                        msDbCatcherFirstPass.catchFirstPass();
                    }), "CatchEventFirstPass", actionBeforeClosing);
                    await Task.Delay(10000);
                }
            }
            catch (Exception ex)
            {
                message.Display("An unprocessed error has occurred in Task CatchEventFirstPass. See log for more information.", "Error", ex);
            }
        }

        public async Task CatchEventLastPass(CancellationToken cancellationToken, Action actionBeforeClosing)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    WrapperToActionWithMsDb(new Action(delegate
                    {
                        MsDbCatcherLastPass msDbCatcherLastPass = new MsDbCatcherLastPass(config, msDb);
                        msDbCatcherLastPass.catchLastPass();
                    }), "CatchEventLastPass", actionBeforeClosing);
                    await Task.Delay(10000);
                }
            }
            catch (Exception ex)
            {
                message.Display("An unprocessed error has occurred in Task CatchEventLastPass. See log for more information.", "Error", ex);
            }
        }






    }
}
