﻿using System;
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
    public class AppRunner
    {
        internal protected Message message { get; set; }
        internal protected Config config { get; set; }
        internal protected MsDb msDb { get; set; }
        internal protected Firebird firebird { get; set; }    
        internal protected EventHandlerEljur eventHandlerEljur { get; set; }
        internal protected CancellationTokenSource cancellationTokenSource { get; set; }
        internal protected MsDbCleaner msDbCleaner { get; set; }
        internal protected MsDbChecker msDbChecker { get; set; }
        internal protected MsDbUpdater msDbUpdater { get; set; }
        internal protected Action actionBeforeClosing { get; set; }


        public AppRunner()
        {
            this.message = new Message();
            this.config = new Config();
            this.firebird = new Firebird(config.ConStrFbDB);
            this.msDb = new MsDb(config);
            this.msDbChecker = new MsDbChecker(config);
            this.msDbUpdater = new MsDbUpdater(config);
            this.msDbCleaner = new MsDbCleaner();
            this.eventHandlerEljur = new EventHandlerEljur(config, msDb, firebird);
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