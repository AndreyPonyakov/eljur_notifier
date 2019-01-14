
using eljur_notifier.EljurNS;
using eljur_notifier.EventHandlerNS;
using eljur_notifier.FirebirdNS;
using eljur_notifier.DbCommonNS;
using MsDbLibraryNS.MsDbNS.CatcherNS;
using MsDbLibraryNS.MsDbNS.CheckerNS;
using MsDbLibraryNS.MsDbNS.CleanerNS;
using MsDbLibraryNS.MsDbNS.RequesterNS;
using MsDbLibraryNS.MsDbNS.SetterNS;
using MsDbLibraryNS.MsDbNS.UpdaterNS;
using MsDbLibraryNS;
using eljur_notifier.MsDbNS;



namespace eljur_notifier.AppCommonNS
{
    public class EljurBaseClass : DbCommonClass
    {
        internal protected Message message { get; set; }
        public  Config config { get; set; }
        internal protected EljurApiRequester eljurApiRequester { get; set; }
        internal protected MsDbRequester msDbRequester { get; set; }
        internal protected MsDbSetter msDbSetter { get; set; }
        internal protected Firebird firebird { get; set; }
        internal protected AllStaffAdder allStaffAdder { get; set; }
        internal protected MsDbChecker msDbChecker { get; set; }
        internal protected MsDbUpdater msDbUpdater { get; set; }
        internal protected MsDbCleaner msDbCleaner { get; set; }
        internal protected EventHandlerEljur eventHandlerEljur { get; set; }
        internal protected MsDb msDb { get; set; }
        internal protected TimeChecker timeChecker { get; set; }
        internal protected EljurApiSender eljurApiSender { get; set; }
        internal protected MsDbCatcherFirstPass msDbCatcherFirstPass { get; set; }
        internal protected MsDbCatcherLastPass msDbCatcherLastPass { get; set; }


        public EljurBaseClass(Message Message)
        {
            this.message = Message;
        }

        public EljurBaseClass(Message Message, Config Config)
        {
            this.message = Message;
            this.config = Config;
        }

        public EljurBaseClass(Message Message, EljurApiRequester EljurApiRequester)
        {
            this.message = Message;
            this.eljurApiRequester = EljurApiRequester;
        }

        public EljurBaseClass(Message Message, MsDbRequester MsDbRequester, MsDbSetter MsDbSetter)
        {
            this.message = Message;
            this.msDbRequester = MsDbRequester;
            this.msDbSetter = MsDbSetter;
        }


        public EljurBaseClass(Message Message, Config Config, MsDb MsDb, Firebird Firebird, 
            TimeChecker TimeChecker, EljurApiSender EljurApiSender, MsDbCatcherFirstPass MsDbCatcherFirstPass, MsDbCatcherLastPass MsDbCatcherLastPass,
            MsDbSetter MsDbSetter)
        {
            this.message = Message;
            this.config = Config;
            this.msDb = new MsDb(config.ConfigsTreeIdResourceOutput1, config.ConfigsTreeIdResourceOutput2, config.ConfigsTreeIdResourceInput1, config.ConfigsTreeIdResourceInput2);
            this.firebird = Firebird;
            this.timeChecker = new TimeChecker(config.timeFromDel, config.timeToDel);
            this.eljurApiSender = EljurApiSender;
            this.msDbCatcherFirstPass = MsDbCatcherFirstPass;
            this.msDbCatcherLastPass = MsDbCatcherLastPass;
            this.msDbSetter = MsDbSetter;
        }

        public EljurBaseClass(Message Message, Config Config, Firebird Firebird, AllStaffAdder allStaffAdder, MsDbChecker MsDbChecker, MsDbUpdater MsDbUpdater, MsDbCleaner MsDbCleaner, EventHandlerEljur EventHandlerEljur)
        {
            this.message = new Message();
            this.config = new Config();
            this.firebird = new Firebird();
            this.allStaffAdder = new AllStaffAdder();
            this.msDbChecker = new MsDbChecker();
            this.msDbUpdater = new MsDbUpdater();
            this.msDbCleaner = new MsDbCleaner();
            this.eventHandlerEljur = new EventHandlerEljur();
            
        }

    }
}
