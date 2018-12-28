using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Data.SqlClient;
using eljur_notifier.AppCommon;
using eljur_notifier.FirebirdNS;

namespace eljur_notifier.MsDbNS
{
    class MsDbChecker
    {
        internal protected Message message { get; set; }
        internal protected Firebird firebird { get; set; }
        internal protected MsDb msDb { get; set; }
        internal protected Config config { get; set; }
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
                this.CreateMsDb();
            }
            if (msDb.IsTableEmpty("Schedules"))
            {
                message.Display("Schedules is Empty", "Warn");
                msDb.FillSchedulesDb();
            }
            else
            {
                message.Display("Schedules is not Empty", "Warn");
                //msDb.clearTableDb("Schedules");
                //msDb.FillSchedulesDb();
            }
            if ((Convert.ToInt32(DateTime.Today.Day) == 1) && (Convert.ToInt32(DateTime.Today.Month) == 9))
            {
                message.Display("Today is 01.09 and we will change all Pupils in Pupils Table", "Info");
                this.CreateMsDb();
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
                if (msDb.IsDbExist(msDb.dbcon, "CheckTime func"))
                {
                    DateTime ModifyDate = msDb.getModifyDate();
                    message.Display("DATABASE was modified: " + ModifyDate.ToString(), "Warn");
                    TimeSpan diff = DateTime.Now.Subtract(ModifyDate);
                    if (diff.TotalMilliseconds < 10000)
                    {
                        message.Display("diff is " + diff.TotalMilliseconds.ToString(), "Warn");
                        message.Display("DATABASE MsDb was modify recently!!! No needed to clear tables again!!!", "Warn");
                    }
                    else
                    {
                        //msDb.deleteDb(config.ConStrMsDB); // NEVER DELETE THIS DATABASE WHOLE
                        //msDb.clearTableDb("Pupils"); //NEVER CLEAR THIS TABLE. ONLY LAZY UPDATING IN NEW TASK
                        msDb.clearTableDb("Events");
                        msDb.clearTableDb("Schedules");
                        message.Display("TABLE Events MsDb DATABASE was cleared", "Warn");
                        message.Display("TABLE Schedules MsDb DATABASE was cleared", "Warn");
                        actionAtMidnight();
                    }
                }
                else
                {
                    message.Display("Cannot connect to database MsDb from CheckTime(Action actionAtMidnight)", "Warn");
                    //SqlConnection.ClearAllPools();
                    msDb.dbcon = new SqlConnection(config.ConStrMsDB);
                    CreateMsDb();
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
                DateTime ModifyDate = msDb.getModifyDate();
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
        public void CreateMsDb()
        {
            //SqlConnection.ClearAllPools();
            //msDb = new MsDb(config.ConStrMsDB); //DON'T DO THIS TERRABLE THING HERE!!!
            msDb.dbcon = new SqlConnection(config.ConStrMsDB);

            msDb.createCleanMsDb(config.ConStrMsDB);
            
            var AllStaff = firebird.getAllStaff();
            msDb.FillStaffDb(AllStaff);
            msDb.FillSchedulesDb();
        }
    }
}
