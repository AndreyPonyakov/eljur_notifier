using System;
using System.Threading.Tasks;
using System.Threading;
using eljur_notifier.FirebirdNS;
using eljur_notifier.MsDbNS;
using eljur_notifier.AppCommon;

namespace eljur_notifier.EventHandlerNS
{
    class AppRunner
    {
        internal protected Message message { get; set; }
        internal protected Config config { get; set; }
        internal protected MsDb msDb { get; set; }
        internal protected Firebird firebird { get; set; }
        internal protected MsDbChecker msDbChecker { get; set; }
        internal protected EventHandlerEljur eventHandlerEljur { get; set; }
        internal protected CancellationTokenSource cancellationTokenSource { get; set; }

        public AppRunner()
        {
            this.message = new Message();
            this.config = new Config();
            this.firebird = new Firebird(config.ConStrFbDB);
            this.msDb = new MsDb(config);
            this.msDbChecker = new MsDbChecker(config, msDb, firebird);
            this.eventHandlerEljur = new EventHandlerEljur(config, msDb, firebird, msDbChecker);
            this.cancellationTokenSource = new CancellationTokenSource();
        }

        public void Run(string[] args)
        {
            var GetDataFb = eventHandlerEljur.GetDataFb(cancellationTokenSource.Token);

            var SendNotifyParents = eventHandlerEljur.SendNotifyParents(cancellationTokenSource.Token);

            var CatchEventFirstPass = eventHandlerEljur.CatchEventFirstPass(cancellationTokenSource.Token);

            var CatchEventLastPass = eventHandlerEljur.CatchEventLastPass(cancellationTokenSource.Token);

            var CheckMsDb = eventHandlerEljur.ChecTimekMsDb(cancellationTokenSource.Token, new Action(delegate
            {
                cancellationTokenSource.Cancel();
                Task.WaitAll(GetDataFb, SendNotifyParents, CatchEventFirstPass, CatchEventLastPass);
                Task.Delay(TimeSpan.FromMilliseconds(config.IntervalSleepBeforeReset));
                //restart                              
                Run(args);

            }));

            while (true) { }
        }

    }
}
