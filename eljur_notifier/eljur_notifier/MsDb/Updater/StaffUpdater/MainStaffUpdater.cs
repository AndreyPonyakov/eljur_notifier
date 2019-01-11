using eljur_notifier.AppCommonNS;
using eljur_notifier.FirebirdNS;

namespace eljur_notifier.MsDbNS.UpdaterNS.StaffUpdaterNS
{
    public class MainStaffUpdater : EljurBaseClass
    {
        public MainStaffUpdater() : base(new Firebird(), new MsDbStaffUpdater(), new NewStaffAdder(), new OldStaffCleaner()) { }

        public void MainUpdateStaff()
        {
            var AllStaff = firebird.getAllStaff();
            msDbStaffUpdater.UpdateStaff(AllStaff);
            newStaffAdder.AddNewPupil(AllStaff);
            oldStaffCleaner.CleanOldStaff(AllStaff);
        }

    }
}
