using System;
using System.Threading.Tasks;
using System.Threading;
using eljur_notifier.FirebirdNS;
using eljur_notifier.MsDbNS;
using eljur_notifier.MsDbNS.CheckerNS;
using eljur_notifier.AppCommonNS;
using eljur_notifier.MsDbNS.CleanerNS;
using eljur_notifier.MsDbNS.UpdaterNS;

namespace eljur_notifier.EventHandlerNS
{
    public class AppRunner : EljurBaseClass
    {       
        internal protected CancellationTokenSource cancellationTokenSource { get; set; }     
        internal protected Action actionBeforeClosing { get; set; }

        public AppRunner() : base(new Message(), new Config(), new Firebird(), new MsDb(), new MsDbChecker(), new MsDbUpdater(), new MsDbCleaner(), new EventHandlerEljur())
        {
            this.cancellationTokenSource = new CancellationTokenSource();
        }

        public void Run(string[] args)
        {
            var GetDataFb = eventHandlerEljur.GetDataFb(cancellationTokenSource.Token);

            var CatchEventFirstPass = eventHandlerEljur.CatchEventFirstPass(cancellationTokenSource.Token);

            var CatchEventLastPass = eventHandlerEljur.CatchEventLastPass(cancellationTokenSource.Token);           

            var CheckMsDb = eventHandlerEljur.CheckTimekMsDb(cancellationTokenSource.Token, new Action(delegate
            {
                cancellationTokenSource.Cancel();
                Task.WaitAll(GetDataFb, CatchEventFirstPass, CatchEventLastPass);
                msDbCleaner.clearAllTablesBesidesPupils();
                msDbUpdater.UpdateStaffDb();
                Task.Delay(TimeSpan.FromMilliseconds(config.IntervalSleepBeforeReset));
                //restart 
                AppRunner appRunner = new AppRunner();
                appRunner.Run(args);
            }));




            while (true) { }
        }

    }
}
