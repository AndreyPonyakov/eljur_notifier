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
using eljur_notifier.MsDbNS.UpdaterNS.StaffUpdaterNS;

namespace eljur_notifier.MsDbNS.UpdaterNS
{
    public class MsDbUpdater
    {
        internal protected Message message { get; set; }
        internal protected Config config { get; set; }
        internal protected StaffContext StaffCtx { get; set; }
        internal protected ScheduleFiller scheduleFiller { get; set; }
        internal protected StaffFiller staffFiller { get; set; }
        internal protected MainStaffUpdater mainStaffUpdater { get; set; }

        public MsDbUpdater(Config Config)
        {
            this.message = new Message();
            this.config = Config;
            this.scheduleFiller = new ScheduleFiller(config);
            this.staffFiller = new StaffFiller(config);
            this.mainStaffUpdater = new MainStaffUpdater(config);
        }


        public void UpdateSchedulesDb()
        {
            scheduleFiller.FillSchedulesDb();
        }


        public void UpdateStaffDb()
        {
            mainStaffUpdater.MainUpdateStaff();
        }

        public void UpdateMsDb()
        {
            UpdateStaffDb();
            UpdateSchedulesDb();
        }

    }
}
