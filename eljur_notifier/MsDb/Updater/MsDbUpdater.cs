using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eljur_notifier.AppCommon;
using eljur_notifier.StaffModel;
using eljur_notifier.EljurNS;
using eljur_notifier.MsDbNS.FillerNS;
using eljur_notifier.FirebirdNS;

namespace eljur_notifier.MsDbNS.Updater
{
    class MsDbUpdater
    {
        internal protected Message message { get; set; }
        internal protected Config config { get; set; }
        internal protected StaffContext StaffCtx { get; set; }
        internal protected ScheduleFiller scheduleFiller { get; set; }
        internal protected StaffFiller staffFiller { get; set; }
        internal protected Firebird firebird { get; set; }

        public MsDbUpdater(Config Config)
        {
            this.message = new Message();
            this.config = Config;
            this.scheduleFiller = new ScheduleFiller(config);
            this.staffFiller = new StaffFiller(config);
            this.firebird = new Firebird(config.ConStrFbDB);
        }


        public void UpdateSchedulesDb()
        {
            scheduleFiller.FillSchedulesDb();
        }


        public void UpdateStaffDb()
        {
            var AllStaff = firebird.getAllStaff();
            staffFiller.FillStaffDb(AllStaff);
        }

        public void UpdateMsDb()
        {
            UpdateStaffDb();
            UpdateSchedulesDb();
        }

    }
}
