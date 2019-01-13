using eljur_notifier.StaffModel;
using eljur_notifier.MsDbNS.SetterNS;
using eljur_notifier.EljurNS;
using eljur_notifier.MsDbNS.RequesterNS;
using eljur_notifier.MsDbNS.CleanerNS;
using eljur_notifier.MsDbNS.UpdaterNS;
using eljur_notifier.EventHandlerNS;
using eljur_notifier.FirebirdNS;
using eljur_notifier.MsDbNS;
using eljur_notifier.MsDbNS.CheckerNS;
using eljur_notifier.DbCommonNS;
using eljur_notifier.MsDbNS.FillerNS;
using System.Data.SqlClient;
using eljur_notifier.MsDbNS.UpdaterNS.StaffUpdaterNS;
using eljur_notifier.MsDbNS.CatcherNS;


namespace eljur_notifier.AppCommonNS
{
    public class EljurBaseClass : DbCommonClass
    {
        internal protected Message message { get; set; }
        internal protected Config config { get; set; }
        internal protected StaffContext StaffCtx { get; set; }
        internal protected MsDbSetter msDbSetter { get; set; }
        internal protected EljurApiSender eljurApiSender { get; set; }
        internal protected MsDbRequester msDbRequester { get; set; }
        internal protected Firebird firebird { get; set; }
        internal protected MsDb msDb { get; set; }
        internal protected EventHandlerEljur eventHandlerEljur { get; set; }
        internal protected MsDbCleaner msDbCleaner { get; set; }
        internal protected MsDbChecker msDbChecker { get; set; }
        internal protected MsDbUpdater msDbUpdater { get; set; }
        internal protected MsDbFiller msDbFiller { get; set; }
        internal protected ExistChecker existChecker { get; set; }
        internal protected EmptyChecker emptyChecker { get; set; }
        internal protected SqlConnection dbcon { get; set; }
        internal protected StaffFiller staffFiller { get; set; }
        internal protected ScheduleFiller scheduleFiller { get; set; }
        internal protected EljurApiRequester eljurApiRequester { get; set; }
        internal protected MainStaffUpdater mainStaffUpdater { get; set; }
        internal protected MsDbStaffUpdater msDbStaffUpdater { get; set; }
        internal protected NewStaffAdder newStaffAdder { get; set; }
        internal protected OldStaffCleaner oldStaffCleaner { get; set; }
        internal protected TimeChecker timeChecker { get; set; }
        internal protected MsDbCatcherFirstPass msDbCatcherFirstPass { get; set; }
        internal protected MsDbCatcherLastPass msDbCatcherLastPass { get; set; }

        public EljurBaseClass(Message Message)
        {
            this.message = Message;
        }

        public EljurBaseClass(StaffContext StaffContext)
        {
            this.StaffCtx = StaffContext;
        }

        public EljurBaseClass(Message Message, Config Config)
        {
            this.message = Message;
            this.config = Config;
        }

        public EljurBaseClass(StaffContext StaffContext, MsDbSetter MsDbSetter)
        {
            this.StaffCtx = StaffContext;
            this.msDbSetter = MsDbSetter;
        }

        public EljurBaseClass(StaffFiller StaffFiller, ScheduleFiller ScheduleFiller)
        {
            this.staffFiller = StaffFiller;
            this.scheduleFiller = ScheduleFiller;
        }

        public EljurBaseClass(Message Message, SqlConnection SqlConnection)
        {
            this.message = Message;
            this.dbcon = SqlConnection;
        }

        public EljurBaseClass(Message Message, StaffContext StaffContext)
        {
            this.message = Message;
            this.StaffCtx = StaffContext;
        }

        public EljurBaseClass(ScheduleFiller ScheduleFiller, MainStaffUpdater MainStaffUpdater)
        {
            this.scheduleFiller = new ScheduleFiller();
            this.mainStaffUpdater = new MainStaffUpdater();
        }

        public EljurBaseClass(Message Message, Config Config, StaffContext StaffContext)
        {
            this.message = Message;
            this.config = Config;
            this.StaffCtx = StaffContext;
        }

        public EljurBaseClass(Message Message, Config Config, SqlConnection SqlConnection)
        {
            this.message = Message;
            this.config = Config;
            this.dbcon = SqlConnection;
        }

        public EljurBaseClass(Message Message, MsDbRequester MsDbRequester, MsDbSetter MsDbSetter)
        {
            this.message = Message;
            this.msDbRequester = MsDbRequester;
            this.msDbSetter = MsDbSetter;
        }

        public EljurBaseClass(Message Message, StaffContext StaffContext, EljurApiRequester EljurApiRequester)
        {
            this.message = Message;
            this.StaffCtx = StaffContext;
            this.eljurApiRequester = EljurApiRequester;
        }

        public EljurBaseClass(Message Message, StaffContext StaffContext, MsDbSetter MsDbSetter, EljurApiSender EljurApiSender)
        {
            this.message = Message;
            this.StaffCtx = StaffContext;
            this.msDbSetter = MsDbSetter;
            this.eljurApiSender = EljurApiSender;
        }

        public EljurBaseClass(MsDbStaffUpdater MsDbStaffUpdater, NewStaffAdder NewStaffAdder, OldStaffCleaner OldStaffCleaner)
        {
            this.msDbStaffUpdater = MsDbStaffUpdater;
            this.newStaffAdder = NewStaffAdder;
            this.oldStaffCleaner = OldStaffCleaner;
        }

        public EljurBaseClass(Message Message, Firebird Firebird, StaffFiller StaffFiller, ScheduleFiller ScheduleFiller)
        {
            this.message = Message;
            this.firebird = Firebird;
            this.staffFiller = StaffFiller;
            this.scheduleFiller = ScheduleFiller;
        }

        public EljurBaseClass(Message Message, MsDbFiller MsDbFiller, ExistChecker ExistChecker, EmptyChecker EmptyChecker, MsDbCleaner MsDbCleaner)
        {
            this.message = Message;
            this.msDbFiller = MsDbFiller;
            this.existChecker = ExistChecker;
            this.emptyChecker = EmptyChecker;
            this.msDbCleaner = MsDbCleaner;
        }

        public EljurBaseClass(Message Message, Config Config, MsDb MsDb, Firebird Firebird, 
            TimeChecker TimeChecker, EljurApiSender EljurApiSender, MsDbCatcherFirstPass MsDbCatcherFirstPass, MsDbCatcherLastPass MsDbCatcherLastPass,
            MsDbSetter MsDbSetter)
        {
            this.message = Message;
            this.config = Config;
            this.msDb = MsDb;
            this.firebird = Firebird;
            this.timeChecker = TimeChecker;
            this.eljurApiSender = EljurApiSender;
            this.msDbCatcherFirstPass = MsDbCatcherFirstPass;
            this.msDbCatcherLastPass = MsDbCatcherLastPass;
            this.msDbSetter = MsDbSetter;
        }

        public EljurBaseClass(Message Message, Config Config, Firebird Firebird, MsDb MsDb, MsDbChecker MsDbChecker, MsDbUpdater MsDbUpdater, MsDbCleaner MsDbCleaner, EventHandlerEljur EventHandlerEljur)
        {
            this.message = new Message();
            this.config = new Config();
            this.firebird = new Firebird();
            this.msDb = new MsDb();
            this.msDbChecker = new MsDbChecker();
            this.msDbUpdater = new MsDbUpdater();
            this.msDbCleaner = new MsDbCleaner();
            this.eventHandlerEljur = new EventHandlerEljur();
            
        }

    }
}
