using System;
using System.Collections.Generic;
using eljur_notifier.AppCommonNS;
using eljur_notifier.MsDbNS.FillerNS;
using eljur_notifier.MsDbNS.UpdaterNS.StaffUpdaterNS;
using eljur_notifier.MsDbNS.CleanerNS;

namespace eljur_notifier.MsDbNS.UpdaterNS
{
    public class MsDbUpdater : EljurBaseClass
    {
        public MsDbUpdater(String NameorConnectionString = "name=StaffContext") 
            : base(new ScheduleFiller(NameorConnectionString), new MainStaffUpdater(NameorConnectionString), new MsDbCleaner(NameorConnectionString)) { }
   
        public void UpdateSchedulesDb()
        {
            msDbCleaner.clearAllTablesBesidesPupils();
            scheduleFiller.FillSchedulesDb();
        }

        public void UpdateStaffDb(List<object[]> AllStaff)
        {
            mainStaffUpdater.MainUpdateStaff(AllStaff);
        }

        public void UpdateMsDb(List<object[]> AllStaff)
        {
            UpdateStaffDb(AllStaff);
            msDbCleaner.clearAllTablesBesidesPupils();
            UpdateSchedulesDb();
        }

    }
}
