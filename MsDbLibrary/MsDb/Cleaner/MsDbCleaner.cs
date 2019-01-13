using System;
using MsDbLibraryNS.StaffModel;


namespace MsDbLibraryNS.MsDbNS.CleanerNS
{
    public class MsDbCleaner: MsDbBaseClass
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
