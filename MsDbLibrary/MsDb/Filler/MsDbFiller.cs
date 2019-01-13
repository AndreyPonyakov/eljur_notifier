using System;
using System.Collections.Generic;
using eljur_notifier.AppCommonNS;
using MsDbLibraryNS.MsDbNS.FillerNS;

namespace MsDbLibraryNS.MsDbNS.FillerNS
{
    public class MsDbFiller : MsDbBaseClass
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
