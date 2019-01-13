using System;
using System.Collections.Generic;
using eljur_notifier.AppCommonNS;

namespace MsDbLibraryNS.MsDbNS.UpdaterNS.StaffUpdaterNS
{
    public class MainStaffUpdater : MsDbBaseClass
    {
        public MainStaffUpdater(String NameorConnectionString = "name=StaffContext") 
            : base(new MsDbStaffUpdater(NameorConnectionString), new NewStaffAdder(NameorConnectionString), new OldStaffCleaner(NameorConnectionString)) {}


        public void MainUpdateStaff(List<object[]> AllStaff)
        {
            msDbStaffUpdater.UpdateStaff(AllStaff);
            newStaffAdder.AddNewPupil(AllStaff);
            oldStaffCleaner.CleanOldStaff(AllStaff);
        }

    }
}
