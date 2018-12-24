using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
        public EventHandlerEljur(Config Config, Firebird Firebird, MsDb MsDb)
        {
            this.message = new Message();
            this.config = Config;
            this.firebird = Firebird;
            this.msDb = MsDb;

        }
        public void GetDataFb()
        {
            while (true)
            {
                DateTime startTime = DateTime.Now;

                List<object[]> curEvents = firebird.getStaffByTimeStamp(config);


                TimeSpan deltaTime = DateTime.Now - startTime;
                TimeSpan IntervalRequest = TimeSpan.FromMilliseconds(config.IntervalRequest);
                TimeSpan sleepTime = IntervalRequest - deltaTime;
                message.Display("sleepTime is: " + sleepTime.ToString(), "Trace");
                Thread.Sleep(sleepTime);
            }
        }

    }
}
