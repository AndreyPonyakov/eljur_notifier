using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using eljur_notifier.FirebirdNS;
using eljur_notifier.MsDbNS;
using eljur_notifier.MsDbNS.CheckerNS;
using eljur_notifier.AppCommonNS;
using eljur_notifier.MsDbNS.CleanerNS;
using eljur_notifier.MsDbNS.UpdaterNS;
using eljur_notifier.EljurNS;

namespace eljur_notifier.EventHandlerNS
{
    public class AppRunner : EljurBaseClass
    {       
        internal protected CancellationTokenSource cancellationTokenSource { get; set; }     
        internal protected Action actionBeforeClosing { get; set; }

        public AppRunner() : base(new Message(), new Config(), new Firebird(), new AllStaffAdder(), new MsDb(), new MsDbChecker(), new MsDbUpdater(), new MsDbCleaner(), new EventHandlerEljur())
        {
            this.cancellationTokenSource = new CancellationTokenSource();
        }

        public void Run(string[] args)
        {
            Boolean IsDbExist = msDbChecker.CheckMsDb();
            if (IsDbExist)
            {
                var AllStaff = new List<object[]>();
                AllStaff = firebird.getAllStaff();
                foreach (object[] row in AllStaff)
                {
                    Console.WriteLine(row[0]);
                    Console.WriteLine(row[1]);
                    Console.WriteLine(row[2]);
                    Console.WriteLine(row[3]);
                    Console.WriteLine(row[4]);
                    Console.WriteLine(row[22]);
                    break;
                }
                AllStaff = allStaffAdder.AddClassAndEljurId(AllStaff);
                foreach (object[] row in AllStaff)
                {
                    Console.WriteLine(row[0]);
                    Console.WriteLine(row[1]);
                    Console.WriteLine(row[2]);
                    Console.WriteLine(row[3]);
                    Console.WriteLine(row[4]);
                    Console.WriteLine(row[20]);
                    Console.WriteLine(row[21]);
                    Console.WriteLine(row[22]);
                    Console.ReadKey();
                    break;
                }
                msDbUpdater.UpdateMsDb(AllStaff);
            }
            var GetDataFb = eventHandlerEljur.GetDataFb(cancellationTokenSource.Token);

            var CatchEventFirstPass = eventHandlerEljur.CatchEventFirstPass(cancellationTokenSource.Token);

            var CatchEventLastPass = eventHandlerEljur.CatchEventLastPass(cancellationTokenSource.Token);           

            var CheckMsDb = eventHandlerEljur.CheckTimekMsDb(cancellationTokenSource.Token, new Action(delegate
            {
                cancellationTokenSource.Cancel();
                Task.WaitAll(GetDataFb, CatchEventFirstPass, CatchEventLastPass);
                msDbCleaner.clearAllTablesBesidesPupils();
                var AllStaff = new List<object[]>();
                AllStaff = firebird.getAllStaff();
                //NEED AllStaffAdder!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                msDbUpdater.UpdateMsDb(AllStaff);
                Task.Delay(TimeSpan.FromMilliseconds(config.IntervalSleepBeforeReset));
                //restart 
                AppRunner appRunner = new AppRunner();
                appRunner.Run(args);
            }));




            while (true) { }
        }

    }
}
