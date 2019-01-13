using MsDbLibraryNS.StaffModel;
using MsDbLibraryNS.MsDbNS.SetterNS;
using MsDbLibraryNS.MsDbNS.RequesterNS;
using MsDbLibraryNS.MsDbNS.CleanerNS;
using MsDbLibraryNS.MsDbNS.UpdaterNS;
using MsDbLibraryNS.MsDbNS;
using MsDbLibraryNS.MsDbNS.CheckerNS;
using MsDbLibraryNS.MsDbNS.FillerNS;
using System.Data.SqlClient;
using MsDbLibraryNS.MsDbNS.UpdaterNS.StaffUpdaterNS;
using MsDbLibraryNS.MsDbNS.CatcherNS;
using MsDbLibraryNS;

namespace MsDbLibraryNS
{
    public class MsDbBaseClass
    {
        internal protected Message message { get; set; }
        internal protected StaffContext StaffCtx { get; set; }
        internal protected MsDbSetter msDbSetter { get; set; }
        internal protected MsDbRequester msDbRequester { get; set; }
        internal protected MsDb msDb { get; set; }
        internal protected MsDbCleaner msDbCleaner { get; set; }
        internal protected MsDbChecker msDbChecker { get; set; }
        internal protected MsDbUpdater msDbUpdater { get; set; }
        internal protected MsDbFiller msDbFiller { get; set; }
        internal protected ExistChecker existChecker { get; set; }
        internal protected EmptyChecker emptyChecker { get; set; }
        internal protected SqlConnection dbcon { get; set; }
        internal protected StaffFiller staffFiller { get; set; }
        internal protected ScheduleFiller scheduleFiller { get; set; }
        internal protected MainStaffUpdater mainStaffUpdater { get; set; }
        internal protected MsDbStaffUpdater msDbStaffUpdater { get; set; }
        internal protected NewStaffAdder newStaffAdder { get; set; }
        internal protected OldStaffCleaner oldStaffCleaner { get; set; }
        internal protected TimeChecker timeChecker { get; set; }
        internal protected MsDbCatcherFirstPass msDbCatcherFirstPass { get; set; }
        internal protected MsDbCatcherLastPass msDbCatcherLastPass { get; set; }

        public MsDbBaseClass(Message Message)
        {
            this.message = Message;
        }

        public MsDbBaseClass(StaffContext StaffContext)
        {
            this.StaffCtx = StaffContext;
        }

        public MsDbBaseClass(Message Message, StaffContext StaffContext)
        {
            this.message = Message;
            this.StaffCtx = StaffContext;
        }

        public MsDbBaseClass(Message Message, SqlConnection SqlConnection)
        {
            this.message = Message;
            this.dbcon = SqlConnection;
        }

        public MsDbBaseClass(Message Message, ExistChecker ExistChecker)
        {
            this.message = Message;
            this.existChecker = ExistChecker;
        }

        public MsDbBaseClass(StaffFiller StaffFiller, ScheduleFiller ScheduleFiller)
        {
            this.staffFiller = StaffFiller;
            this.scheduleFiller = ScheduleFiller;
        }

        public MsDbBaseClass(MsDbStaffUpdater MsDbStaffUpdater, NewStaffAdder NewStaffAdder, OldStaffCleaner OldStaffCleaner)
        {
            this.msDbStaffUpdater = MsDbStaffUpdater;
            this.newStaffAdder = NewStaffAdder;
            this.oldStaffCleaner = OldStaffCleaner;
        }

        public MsDbBaseClass(ScheduleFiller ScheduleFiller, MainStaffUpdater MainStaffUpdater, MsDbCleaner MsDbCleaner)
        {
            this.scheduleFiller = ScheduleFiller;
            this.mainStaffUpdater = MainStaffUpdater;
            this.msDbCleaner = MsDbCleaner;
        }

    }
}
