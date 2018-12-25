using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Data.SqlClient;IsDbExist
using eljur_notifier.AppCommon;
using eljur_notifier.FirebirdNS;

namespace eljur_notifier.MsDbNS
{
    class MsDbChecker
    {
        internal protected Message message { get; set; }
        internal protected MsDb msDb { get; set; }
        internal protected Firebird firebird { get; set; }
        internal protected Config config { get; set; }
        internal protected TimeSpan timeFromDel { get; set; }
        internal protected TimeSpan timeToDel { get; set; }

        public MsDbChecker(MsDb MsDb, Config Config, Firebird Firebird)
        {
            this.message = new Message();
            this.msDb = MsDb;
            this.config = Config;
            this.firebird = Firebird;
            this.timeFromDel = new TimeSpan(23, 58, 0);
            this.timeToDel = new TimeSpan(23, 59, 59);
            this.CheckMsDb();
            //while (msDb.IsDbExistVar == false)
            //{
            //    this.CheckMsDb();;
            //}
            //if (!msDb.WasDeletedToday)
            //{
            //    this.CheckMsDb();
            //}
        }
        public void CheckTime(Action actionAtMidnight)
        {
            var timeNow = DateTime.Now.TimeOfDay;
            timeFromDel = new TimeSpan(14, 31, 0);
            timeToDel = new TimeSpan(14, 32, 0);
            if (timeNow > timeFromDel && timeNow < timeToDel)
            {
                message.Display("between " + timeFromDel.ToString() + " and " + timeToDel.ToString(), "Warn");
                if (msDb.IsDbExist(msDb.dbcon))
                {
                    DateTime CreationDate = msDb.getCreationDate();
                    message.Display("DATABASE was created: " + CreationDate.ToString(), "Warn");
                    TimeSpan diff = DateTime.Now.Subtract(CreationDate);
                    if (diff.TotalMilliseconds < 60000)
                    {
                        message.Display("diff is " + diff.TotalMilliseconds.ToString(), "Warn");
                        message.Display("DATABASE MsDb was created recently!!! No needed to delete!!!", "Warn");
                    }
                    else
                    {
                        msDb.deleteDb(config.ConStrMsDB);
                        msDb.IsDbExistVar = false;
                        message.Display("DATABASE MsDb was deleted", "Warn");
                        actionAtMidnight();
                    }

                }
                else
                {
                    SqlConnection.ClearAllPools();
                    CreateMsDb();


                }
                
            }          
        }
        public void CheckMsDb()
        {
            SqlConnection.ClearAllPools();
            msDb.IsDbExistVar = msDb.IsDbExist(msDb.dbcon);
            message.Display("MsSQLDB is exist: " + msDb.IsDbExist(msDb.dbcon).ToString(), "Warn");

            if (msDb.IsDbExist(msDb.dbcon))
            {
                message.Display(" DateTime CreationDate = msDb.getCreationDate(); ", "Warn");        
                DateTime CreationDate = msDb.getCreationDate();
                message.Display("DATABASE was created: " + CreationDate.ToString(), "Warn");
                DateTime dateOnly = CreationDate.Date;
                if (dateOnly == DateTime.Today)
                {
                    message.Display("DATABASE MsDb was created Today!!!", "Warn");
                }
                else
                {                  
                    msDb.deleteDb(config.ConStrMsDB);
                    message.Display("DATABASE MsDb was deleted", "Warn");
                    CreateMsDb();
                    msDb.IsDbExistVar = true;
                }
            }
            else
            {
                CreateMsDb();                                
            }
        }
        public void CreateMsDb()
        {
            MsDb msDb = new MsDb(config.ConStrMsDB);
            msDb.createDb(config.ConStrMsDB);
            message.Display("TABLE Pupils was cleared", "Warn");
            message.Display("TABLE Events was cleared", "Warn");
            var AllStaff = firebird.getAllStaff();
            msDb.FillStaffDb(AllStaff);
        }
    }
}
