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
        internal protected MsDb msDb { get; set; }
        internal protected MsDbChecker msDbChecker { get; set; }
        public EventHandlerEljur(Config Config, Firebird Firebird, MsDb MsDb, MsDbChecker MsDbChecker)
        {
            this.message = new Message();
            this.config = Config;
            this.firebird = Firebird;
            this.msDb = MsDb;
            this.msDbChecker = MsDbChecker;

        }


        public async Task GetDataFb(CancellationToken cancellationToken) 
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                DateTime startTime = DateTime.Now;

                List<object[]> curEvents = firebird.getStaffByTimeStamp(config);
                msDb.CheckEventsDb(curEvents);
                msDbChecker.CheckTime();

                TimeSpan deltaTime = DateTime.Now - startTime;
                TimeSpan IntervalRequest = TimeSpan.FromMilliseconds(config.IntervalRequest);
                TimeSpan sleepTime = IntervalRequest - deltaTime;
                message.Display("sleepTime is: " + sleepTime.ToString(), "Trace");
                await Task.Delay(sleepTime);
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


    }
}
