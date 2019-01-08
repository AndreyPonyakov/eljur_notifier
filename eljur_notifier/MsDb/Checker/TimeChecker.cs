using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eljur_notifier.AppCommon;
using System.Data.SqlClient;
using eljur_notifier.MsDbNS.RequesterNS;
using eljur_notifier.MsDbNS.CleanerNS;

namespace eljur_notifier.MsDbNS.CheckerNS
{
    class TimeChecker
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

        public TimeChecker(Config Config, MsDb MsDb)
        {
            this.message = new Message();
            this.msDb = MsDb;
            this.config = Config;
            this.msDbRequester = new MsDbRequester(config);
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
                        DateTime ModifyDate = msDbRequester.getModifyDate();
                        message.Display("DATABASE was modified: " + ModifyDate.ToString(), "Warn");
                        TimeSpan diff = DateTime.Now.Subtract(ModifyDate);
                        if (diff.TotalMilliseconds < config.IntervalDel)
                        {
                            message.Display("diff is " + diff.TotalMilliseconds.ToString(), "Warn");
                            message.Display("DATABASE MsDb was modify recently!!! No needed to clear tables again!!!", "Warn");
                        }
                        else
                        {
                            //CLEAR ALL TABLES
                            msDbCleaner.clearAllTables();
                            actionAtMidnight();
                        }
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
