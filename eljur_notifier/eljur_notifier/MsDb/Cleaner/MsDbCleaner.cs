using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eljur_notifier.StaffModel;
using eljur_notifier.AppCommon;


namespace eljur_notifier.MsDbNS.CleanerNS
{
    public class MsDbCleaner
    {
        internal protected Message message { get; set; }
        internal protected StaffContext StaffCtx { get; set; }

        public MsDbCleaner()
        {
            this.message = new Message();
        }


        public void clearTableDb(String TableName)
        {
            using (this.StaffCtx = new StaffContext())
            {
                this.StaffCtx.Database.ExecuteSqlCommand("TRUNCATE TABLE [" + TableName + "]");
            }

        }

        public void clearAllTables()  
        {
            this.clearTableDb("Events");
            message.Display("TABLE Events MsDb DATABASE was cleared", "Warn");
            this.clearTableDb("Pupils");
            message.Display("TABLE Pupils MsDb DATABASE was cleared", "Warn");
            this.clearTableDb("Schedules");
            message.Display("TABLE Schedules MsDb DATABASE was cleared", "Warn");
        }

    }
}
