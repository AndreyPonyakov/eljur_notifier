using System.Collections.Generic;
using eljur_notifier.AppCommonNS;
using eljur_notifier.MsDbNS.FillerNS;
using eljur_notifier.MsDbNS.UpdaterNS.StaffUpdaterNS;

namespace eljur_notifier.MsDbNS.UpdaterNS
{
    public class MsDbUpdater : EljurBaseClass
    {
        public MsDbUpdater() : base(new ScheduleFiller(), new MainStaffUpdater()) { }
   
        public void UpdateSchedulesDb()
        {
            scheduleFiller.FillSchedulesDb();
        }

        public void UpdateStaffDb(List<object[]> AllStaff)
        {
            mainStaffUpdater.MainUpdateStaff(AllStaff);
        }

        public void UpdateMsDb(List<object[]> AllStaff)
        {
            UpdateStaffDb(AllStaff);
            UpdateSchedulesDb();
        }

    }
}
