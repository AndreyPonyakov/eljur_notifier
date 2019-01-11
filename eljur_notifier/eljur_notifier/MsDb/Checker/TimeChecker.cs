using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eljur_notifier.AppCommonNS;
using System.Data.SqlClient;
using eljur_notifier.MsDbNS.RequesterNS;
using eljur_notifier.MsDbNS.CleanerNS;

namespace eljur_notifier.MsDbNS.CheckerNS
{
    public class TimeChecker
    {
        internal protected Message message { get; set; }
        internal protected Config config { get; set; }
        internal protected MsDb msDb { get; set; }
        internal protected TimeSpan timeFromDel { get; set; }
        internal protected TimeSpan timeToDel { get; set; }
        internal protected SqlConnection dbcon { get; set; }
        internal protected ExistChecker existChecker { get; set; }
        internal protected MsDbRequester msDbRequester { get; set; }
        internal protected MsDbCleaner msDbCleaner { get; set; }

        public TimeChecker(Config Config)
        {
            this.message = new Message();          
            this.config = Config;
            this.msDb = new MsDb(config);
            this.msDbRequester = new MsDbRequester();
            this.msDbCleaner = new MsDbCleaner();
            //this.timeFromDel = new TimeSpan(23, 57, 59);
            //this.timeToDel = new TimeSpan(23, 59, 59);
            this.existChecker = new ExistChecker(config);
            this.timeFromDel = config.timeFromDel;
            this.timeToDel = config.timeToDel;
        }

        public void CheckTime(Action actionAtMidnight)
        {
            var timeNow = DateTime.Now.TimeOfDay;
            if (timeNow > timeFromDel && timeNow < timeToDel)
            {
                message.Display("Time now is between " + timeFromDel.ToString() + " and " + timeToDel.ToString(), "Warn");
                using (this.dbcon = new SqlConnection(config.ConStrMsDB))
                {
                    if (msDb.IsDbExist(this.dbcon, "CheckTime func"))
                    {                     
                        //GOTO AppRunner                          
                        actionAtMidnight();                       
                    }
                    else
                    {
                        message.Display("Cannot connect to database MsDb from CheckTime(Action actionAtMidnight)", "Error");
                    }
                }
            }
        }



    }
}
