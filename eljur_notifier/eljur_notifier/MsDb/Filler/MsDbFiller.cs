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
        public MsDbFiller() : base(new Message(), new Firebird(), new StaffFiller(), new ScheduleFiller()) { }
  
        public void FillOnlySchedules()
        {
            scheduleFiller.FillSchedulesDb();
        }

        public void FillOnlyPupils()
        {
            var AllStaff = firebird.getAllStaff();
            staffFiller.FillStaffDb(AllStaff);
        }

        public void FillMsDb()
        {
            FillOnlyPupils();
            FillOnlySchedules();
        }

    }
}
