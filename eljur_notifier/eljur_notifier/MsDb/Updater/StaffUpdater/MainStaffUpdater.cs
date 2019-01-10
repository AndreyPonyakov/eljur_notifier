using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eljur_notifier.AppCommon;
using eljur_notifier.FirebirdNS;

namespace eljur_notifier.MsDbNS.UpdaterNS.StaffUpdaterNS
{
    public class MainStaffUpdater
    {
        internal protected Message message { get; set; }
        internal protected Config config { get; set; }
        internal protected MsDbStaffUpdater msDbStaffUpdater { get; set; }
        internal protected NewStaffAdder newStaffAdder { get; set; }
        internal protected OldStaffCleaner oldStaffCleaner { get; set; }     
        internal protected Firebird firebird { get; set; }

        public MainStaffUpdater(Config Config)
        {
            this.message = new Message();
            this.config = Config;
            this.firebird = new Firebird(config.ConStrFbDB);
        }

        public void MainUpdateStaff()
        {
            msDbStaffUpdater = new MsDbStaffUpdater(config);
            newStaffAdder = new NewStaffAdder(config);
            oldStaffCleaner = new OldStaffCleaner();
            var AllStaff = firebird.getAllStaff();
            msDbStaffUpdater.UpdateStaff(AllStaff);
            newStaffAdder.AddNewPupil(AllStaff);
            oldStaffCleaner.CleanOldStaff(AllStaff);
        }

    }
}
