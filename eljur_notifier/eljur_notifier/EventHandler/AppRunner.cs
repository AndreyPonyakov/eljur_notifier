using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using eljur_notifier.FirebirdNS;
using MsDbLibraryNS.MsDbNS.CheckerNS;
using eljur_notifier.AppCommonNS;
using MsDbLibraryNS.MsDbNS.CleanerNS;
using MsDbLibraryNS.MsDbNS.UpdaterNS;
using eljur_notifier.EljurNS;

namespace eljur_notifier.EventHandlerNS
{
    public class AppRunner : EljurBaseClass
    {       
        internal protected CancellationTokenSource cancellationTokenSource { get; set; }     
        internal protected Action actionBeforeClosing { get; set; }

        public AppRunner() 
            : base(new Message(), new Config(), new Firebird(), new AllStaffAdder(), new MsDbChecker(), new MsDbUpdater(), new MsDbCleaner(), new EventHandlerEljur())
        {
            this.cancellationTokenSource = new CancellationTokenSource();
        }

        public void Run(string[] args)
        {
            Boolean IsDbExist = msDbChecker.CheckMsDb();
            if (!IsDbExist)
            {
                CreateNewMsDbThroughUpdating();
            }
            var GetDataFb = eventHandlerEljur.GetDataFb(cancellationTokenSource.Token);

            var CatchEventFirstPass = eventHandlerEljur.CatchEventFirstPass(cancellationTokenSource.Token);

            var CatchEventLastPass = eventHandlerEljur.CatchEventLastPass(cancellationTokenSource.Token);           

            var CheckMsDb = eventHandlerEljur.CheckTimekMsDb(cancellationTokenSource.Token, new Action(delegate
            {
                cancellationTokenSource.Cancel();
                Task.WaitAll(GetDataFb, CatchEventFirstPass, CatchEventLastPass);
                msDbCleaner.clearAllTablesBesidesPupils();
                CreateNewMsDbThroughUpdating();
                Task.Delay(TimeSpan.FromMilliseconds(config.IntervalSleepBeforeReset));
                //restart 
                AppRunner appRunner = new AppRunner();
                appRunner.Run(args);
            }));

            while (true) { }
        }


        public void CreateNewMsDbThroughUpdating()
        {
            var AllStaff = new List<object[]>();
            AllStaff = firebird.getAllStaff();
            AllStaff = allStaffAdder.AddClassAndEljurId(AllStaff);
            var AllClasses = new List<object[]>();
            AllClasses = eljurApiRequester.getClasesObjects();
            msDbUpdater.UpdateMsDb(AllStaff, AllClasses);
        }

    }
}
