using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Data;
using System.Data.SqlClient;
using eljur_notifier;
using eljur_notifier.StaffModel;
using eljur_notifier.AppCommon;
using eljur_notifier.FirebirdNS;
using eljur_notifier.MsDbNS;
using eljur_notifier.EventHandlerNS;
using NLog;


namespace eljur_notifier
{
    class Program
    {
        public static List<string> MainMethodArgs = new List<string>();
        static void Main(string[] args)
        {
            MainMethodArgs = args.ToList();
            Run(MainMethodArgs.ToArray());
        }

        public static void Run(string[] args)
        {
            Message message = new Message();
            Config Config = new Config();         
            Firebird Firebird = new Firebird(Config.ConStrFbDB);
            MsDb MsDb = new MsDb(Config.ConStrMsDB);
            MsDbChecker MsDbChecker = new MsDbChecker(MsDb, Config, Firebird);
            EventHandlerEljur EventHandler = new EventHandlerEljur(Config, Firebird, MsDb, MsDbChecker);
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            var GetDataFb = EventHandler.GetDataFb(cancellationTokenSource.Token);

            var SendNotifyParents = EventHandler.SendNotifyParents(cancellationTokenSource.Token);



            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
            cancellationTokenSource.Cancel();
            Task.WaitAll(GetDataFb, SendNotifyParents);
            //restart
            Run(MainMethodArgs.ToArray());
        }
   
    }
}

