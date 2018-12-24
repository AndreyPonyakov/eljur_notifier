using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }
        public void CheckTime()
        {
            var timeNow = DateTime.Now.TimeOfDay;
            if (timeNow > timeFromDel && timeNow < timeToDel)
            {
                message.Display("between " + timeFromDel.ToString() + " and " + timeToDel.ToString(), "Warn");
            }          

        }
        public void CheckMsDb()
        {
            message.Display("MsSQLDB is exist: " + msDb.IsDbExistVar.ToString(), "Warn");
            if (msDb.IsDbExistVar)
            {
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
                    message.Display("DATABASE was deleted", "Warn");
                    CreateMsDb();
                }
            }
            else
            {
                CreateMsDb();
            }
        }
        public void CreateMsDb()
        {
            msDb.createDb(config.ConStrMsDB);
            message.Display("TABLE Pupils was cleared", "Warn");
            message.Display("TABLE Events was cleared", "Warn");
            var AllStaff = firebird.getAllStaff();
            msDb.FillStaffDb(AllStaff);
        }
    }
}
