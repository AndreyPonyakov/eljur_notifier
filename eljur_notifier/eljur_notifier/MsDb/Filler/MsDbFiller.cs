using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eljur_notifier.FirebirdNS;
using eljur_notifier.AppCommon;
using eljur_notifier.MsDbNS.FillerNS;

namespace eljur_notifier.MsDbNS.FillerNS
{
    public class MsDbFiller
    {
        internal protected Message message { get; set; }
        internal protected Firebird firebird { get; set; }
        internal protected Config config { get; set; }
        internal protected StaffFiller staffFiller { get; set; }
        internal protected ScheduleFiller scheduleFiller { get; set; }

        public MsDbFiller(Config Config)
        {
            this.message = new Message();  
            this.config = Config;
            this.firebird = new Firebird(config.ConStrFbDB);
            this.staffFiller = new StaffFiller(config);
            this.scheduleFiller = new ScheduleFiller(config);

        }

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
