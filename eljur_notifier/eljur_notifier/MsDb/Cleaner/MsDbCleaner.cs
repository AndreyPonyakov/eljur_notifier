using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eljur_notifier.StaffModel;
using eljur_notifier.AppCommonNS;


namespace eljur_notifier.MsDbNS.CleanerNS
{
    public class MsDbCleaner: EljurBaseClass
    {
        internal protected String nameorConnectionString { get; set; }

        public MsDbCleaner(String NameorConnectionString = "name=StaffContext") 
            : base(new Message(), new StaffContext(NameorConnectionString)) {
            this.nameorConnectionString = NameorConnectionString;
        }
            
        public void clearTableDb(String TableName)
        {
            using (this.StaffCtx = new StaffContext(nameorConnectionString))
            {
                this.StaffCtx.Database.ExecuteSqlCommand("TRUNCATE TABLE [" + TableName + "]");
            }

        }
        
        public void clearAllTablesBesidesPupils()
        {
            this.clearTableDb("Events");
            message.Display("TABLE Events MsDb DATABASE was cleared", "Warn");
            this.clearTableDb("Schedules");
            message.Display("TABLE Schedules MsDb DATABASE was cleared", "Warn");
        }

    }
}
