using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eljur_notifier.FirebirdNS;
using eljur_notifier.AppCommonNS;
using eljur_notifier.MsDbNS.FillerNS;

namespace eljur_notifier.MsDbNS.FillerNS
{
    public class MsDbFiller : EljurBaseClass
    {
        public MsDbFiller(String NameorConnectionString = "name=StaffContext") 
            : base(new StaffFiller(NameorConnectionString), new ScheduleFiller(NameorConnectionString)) { }
  
        public void FillOnlySchedules(List<object[]> AllClasses)
        {
            scheduleFiller.FillSchedulesDb(AllClasses);
        }

        public void FillOnlyPupils(List<object[]> AllStaff)
        {
            staffFiller.FillStaffDb(AllStaff);
        }

        public void FillMsDb(List<object[]> AllStaff, List<object[]> AllClasses)
        {
            FillOnlyPupils(AllStaff);
            FillOnlySchedules(AllClasses);
        }

    }
}
