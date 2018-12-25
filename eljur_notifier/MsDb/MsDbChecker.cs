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
            this.CheckMsDb();
        }
        public void CheckTime(Action actionAtMidnight)
        {
            var timeNow = DateTime.Now.TimeOfDay;
            //timeFromDel = new TimeSpan(20, 24, 0);
            //timeToDel = new TimeSpan(20, 25, 0);
            if (timeNow > timeFromDel && timeNow < timeToDel)
            {
                message.Display("between " + timeFromDel.ToString() + " and " + timeToDel.ToString(), "Warn");
                if (msDb.IsDbExist(msDb.dbcon))
                {
                    DateTime CreationDate = msDb.getModifyDate();
                    message.Display("DATABASE was created: " + CreationDate.ToString(), "Warn");
                    TimeSpan diff = DateTime.Now.Subtract(CreationDate);
                    if (diff.TotalMilliseconds < 10000)
                    {
                        message.Display("diff is " + diff.TotalMilliseconds.ToString(), "Warn");
                        message.Display("DATABASE MsDb was modify recently!!! No needed to clear tables again!!!", "Warn");
                    }
                    else
                    {
                        //msDb.deleteDb(config.ConStrMsDB);
                        msDb.clearTableDb("Pupils");
                        msDb.clearTableDb("Events");
                        message.Display("TABLES Pupils and Events MsDb DATABASE was cleared", "Warn");
                        actionAtMidnight();
                    }
                }
                else
                {
                    message.Display("Cannot connect to database MsDb from CheckTime(Action actionAtMidnight)", "Warn");
                    SqlConnection.ClearAllPools();
                    msDb.dbcon = new SqlConnection(config.ConStrMsDB);
                    CreateMsDb();
                }
                
            }          
        }
        public void CheckMsDb()
        {
            SqlConnection.ClearAllPools();
            msDb.dbcon = new SqlConnection(config.ConStrMsDB);

            message.Display("MsSQLDB is exist: " + msDb.IsDbExist(msDb.dbcon).ToString(), "Warn");        
            if (msDb.IsDbExist(msDb.dbcon))
            {      
                DateTime ModifyDate = msDb.getModifyDate();
                message.Display("DATABASE was modified: " + ModifyDate.ToString(), "Warn");
                DateTime dateOnly = ModifyDate.Date;
                if (dateOnly == DateTime.Today)
                {
                    message.Display("DATABASE MsDb was modified Today!!!", "Warn");
                }
                else
                {   
                    // DO NOTHING HERE!!!
                    //msDb.deleteDb(config.ConStrMsDB);
                    //message.Display("DATABASE MsDb was deleted because it was created not Today!!!", "Warn");
                    //CreateMsDb();
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
                //message.Display("Cannot connect to database MsDb from CheckMsDb()", "Warn");
                //SqlConnection.ClearAllPools();
                //msDb.dbcon = new SqlConnection(config.ConStrMsDB);
                //CreateMsDb();                                
            }
        }
        public void CreateMsDb()
        {
            SqlConnection.ClearAllPools();
            //msDb = new MsDb(config.ConStrMsDB); //DON'T DO THIS TERRABLE THING HERE!!!
            msDb.dbcon = new SqlConnection(config.ConStrMsDB);

            msDb.createDb(config.ConStrMsDB);
            
            var AllStaff = firebird.getAllStaff();
            msDb.FillStaffDb(AllStaff);
        }
    }
}
