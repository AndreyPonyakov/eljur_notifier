using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Data.SqlClient;
using eljur_notifier.AppCommon;
using eljur_notifier.FirebirdNS;
using eljur_notifier.MsDbNS.CreatorNS;
using eljur_notifier.MsDbNS.FillerNS;
using eljur_notifier.MsDbNS.RequesterNS;

namespace eljur_notifier.MsDbNS.CheckerNS
{
    class MsDbChecker
    {
        internal protected Message message { get; set; }
        internal protected Firebird firebird { get; set; }
        internal protected MsDb msDb { get; set; }
        internal protected CleanCreator cleanCreator { get; set; }
        internal protected MsDbCreator msDbCreator { get; set; }
        internal protected MsDbFiller msDbFiller { get; set; }
        internal protected ScheduleFiller scheduleFiller { get; set; }
        internal protected Config config { get; set; }
        internal protected Requester requester { get; set; }
        internal protected TimeSpan timeFromDel { get; set; }
        internal protected TimeSpan timeToDel { get; set; }

        public MsDbChecker(Config Config, MsDb MsDb, Firebird Firebird)
        {
            this.message = new Message();
            this.msDb = MsDb;
            this.config = Config;
            this.firebird = Firebird;
            this.timeFromDel = new TimeSpan(23, 58, 59);
            this.timeToDel = new TimeSpan(23, 59, 59);            
            this.msDbCreator = new MsDbCreator(config);
            this.msDbFiller = new MsDbFiller(config);
            this.scheduleFiller = new ScheduleFiller(config);
            this.requester = new Requester(config);
            this.CheckSomeIssuesInConstructor();
            
        }

        public void CheckSomeIssuesInConstructor()
        {
            this.CheckMsDb();
            if (msDb.IsTableExist("Pupils") && msDb.IsTableExist("Schedules"))
            {
                message.Display("msDb already exist", "Warn");
            }
            else
            {
                msDbCreator.CreateMsDb();
            }
            if (msDb.IsTableEmpty("Schedules"))
            {
                message.Display("Schedules is Empty", "Warn");
                scheduleFiller.FillSchedulesDb();
            }
            else
            {
                message.Display("Schedules is not Empty", "Warn");
                msDb.clearTableDb("Schedules");
                scheduleFiller.FillSchedulesDb();
            }
            if ((Convert.ToInt32(DateTime.Today.Day) == 1) && (Convert.ToInt32(DateTime.Today.Month) == 9))
            {
                message.Display("Today is 01.09 and we will change all Pupils in Pupils Table", "Info");
                msDbCreator.CreateMsDb();
            }

        }

        public void CheckTime(Action actionAtMidnight)
        {
            var timeNow = DateTime.Now.TimeOfDay;
            //timeFromDel = new TimeSpan(18, 55, 0);
            //timeToDel = new TimeSpan(20, 25, 0);
            if (timeNow > timeFromDel && timeNow < timeToDel)
            {
                message.Display("between " + timeFromDel.ToString() + " and " + timeToDel.ToString(), "Warn");
                using (msDb.dbcon = new SqlConnection(config.ConStrMsDB))
                {
                    if (msDb.IsDbExist(msDb.dbcon, "CheckTime func"))
                    {
                        DateTime ModifyDate = requester.getModifyDate();
                        message.Display("DATABASE was modified: " + ModifyDate.ToString(), "Warn");
                        TimeSpan diff = DateTime.Now.Subtract(ModifyDate);
                        if (diff.TotalMilliseconds < 30000)
                        {
                            message.Display("diff is " + diff.TotalMilliseconds.ToString(), "Warn");
                            message.Display("DATABASE MsDb was modify recently!!! No needed to clear tables again!!!", "Warn");
                        }
                        else
                        {
                            //msDb.deleteDb(config.ConStrMsDB); // NEVER DELETE THIS DATABASE WHOLE
                            msDb.clearTableDb("Pupils"); //CLEAR ALL TABLES
                            message.Display("TABLE Pupils MsDb DATABASE was cleared", "Warn");
                            msDb.clearTableDb("Events");
                            message.Display("TABLE Events MsDb DATABASE was cleared", "Warn");
                            msDb.clearTableDb("Schedules");                        
                            message.Display("TABLE Schedules MsDb DATABASE was cleared", "Warn");
                            actionAtMidnight();
                        }
                    }
                    else
                    {
                        message.Display("Cannot connect to database MsDb from CheckTime(Action actionAtMidnight)", "Warn");
                    }
                }
            }          
        }

        public void CheckMsDb()
        {
            //SqlConnection.ClearAllPools();
            msDb.dbcon = new SqlConnection(config.ConStrMsDB);

            message.Display("MsSQLDB is exist: " + msDb.IsDbExist(msDb.dbcon, "CheckMsDb func").ToString(), "Warn");        
            if (msDb.IsDbExist(msDb.dbcon, "CheckMsDb func"))
            {      
                DateTime ModifyDate = requester.getModifyDate();
                message.Display("DATABASE was modified: " + ModifyDate.ToString(), "Warn");
                DateTime dateOnly = ModifyDate.Date;
                if (dateOnly == DateTime.Today)
                {
                    message.Display("DATABASE MsDb was modified Today!!!", "Warn");
                }
            }
            else
            {
                try
                {
                    throw new Exception();
                }
                catch (Exception ex)
                {
                    message.Display("Cannot connect to MsDb", "Fatal", ex);
                }                                
            }
        }

    }
}
