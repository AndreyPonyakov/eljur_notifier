using System;
using System.Collections.Generic;
using eljur_notifier.AppCommonNS;
using eljur_notifier.FirebirdNS;

namespace eljur_notifier.MsDbNS.UpdaterNS.StaffUpdaterNS
{
    public class MainStaffUpdater : EljurBaseClass
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
