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
using eljur_notifier.AppCommon;

namespace eljur_notifier.EventHandlerNS
{
    class EventHandlerEljur
    {
        internal protected Message message { get; set; }
        internal protected Config config { get; set; }
        internal protected Firebird firebird { get; set; }
        internal protected MsDbChecker msDbChecker { get; set; }
        public EventHandlerEljur(Config Config, Firebird Firebird, MsDbChecker MsDbChecker)
        {
            this.message = new Message();
            this.config = Config;
            this.firebird = Firebird;
            this.msDbChecker = MsDbChecker;

        }


        public async Task GetDataFb(CancellationToken cancellationToken) 
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                DateTime startTime = DateTime.Now;
                List<object[]> curEvents = firebird.getStaffByTimeStamp(config);
                if (msDb.IsDbExist(msDb.dbcon))
                {
                    msDb.CheckEventsDb(curEvents);
                }
                else
                {
                    message.Display("Cannot connect to database MsDb from Task GetDataFb", "Warn");
                    SqlConnection.ClearAllPools();
                    msDb.dbcon = new SqlConnection(config.ConStrMsDB);
                    msDbChecker.CreateMsDb();
                }
                                      
                TimeSpan deltaTime = DateTime.Now - startTime;
                TimeSpan IntervalRequest = TimeSpan.FromMilliseconds(config.IntervalRequest);
                TimeSpan sleepTime = IntervalRequest - deltaTime;
                message.Display("sleepTime is: " + sleepTime.ToString(), "Trace");
                await Task.Delay(sleepTime);
            }
        }

        public async Task CheckMsDb(CancellationToken cancellationToken, Action actionAtMidnight)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                msDbChecker.CheckTime(actionAtMidnight);
                await Task.Delay(1000);
            }        
        }


        public async Task SendNotifyParents(CancellationToken cancellationToken)
        {
            String EljurApiTocken = config.EljurApiTocken;
            Double FrenchLeaveInterval = config.FrenchLeaveInterval;

            while (!cancellationToken.IsCancellationRequested)
            {
                Console.WriteLine("I'm from second task!!!!");             
                await Task.Delay(1000);
            }

        }

        public void CreateMsDb()
        {
            SqlConnection.ClearAllPools();
            msDb = new MsDb(config.ConStrMsDB);
            msDb.dbcon = new SqlConnection(config.ConStrMsDB);

            msDb.createDb(config.ConStrMsDB);
            message.Display("TABLE Pupils was cleared", "Warn");
            message.Display("TABLE Events was cleared", "Warn");
            var AllStaff = firebird.getAllStaff();
            msDb.FillStaffDb(AllStaff);
        }


    }
}
