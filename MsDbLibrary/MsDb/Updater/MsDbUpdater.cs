using System;
using System.Collections.Generic;
using MsDbLibraryNS.MsDbNS.FillerNS;
using MsDbLibraryNS.MsDbNS.UpdaterNS.StaffUpdaterNS;
using MsDbLibraryNS.MsDbNS.CleanerNS;

namespace MsDbLibraryNS.MsDbNS.UpdaterNS
{
    public class MsDbUpdater : MsDbBaseClass
    {
        public MsDbUpdater(String NameorConnectionString = "name=StaffContext") 
            : base(new ScheduleFiller(NameorConnectionString), new MainStaffUpdater(NameorConnectionString), new MsDbCleaner(NameorConnectionString)) { }
   
        public void UpdateSchedulesDb(List<object[]> AllClasses)
        {
            msDbCleaner.clearAllTablesBesidesPupils();
            scheduleFiller.FillSchedulesDb(AllClasses);
        }

        public void UpdateStaffDb(List<object[]> AllStaff)
        {
            mainStaffUpdater.MainUpdateStaff(AllStaff);
        }

        public void UpdateMsDb(List<object[]> AllStaff, List<object[]> AllClasses)
        {
            UpdateStaffDb(AllStaff);
            msDbCleaner.clearAllTablesBesidesPupils();
            UpdateSchedulesDb(AllClasses);
        }

    }
}
