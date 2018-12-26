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
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;


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

            EljurApiRequester elRequester = new EljurApiRequester();
            elRequester.data = elRequester.getData();

            Console.ReadKey();


            Message message = new Message();
            Config Config = new Config();         
            Firebird Firebird = new Firebird(Config.ConStrFbDB);        
            MsDb MsDb = new MsDb(Config.ConStrMsDB);
            MsDbChecker MsDbChecker = new MsDbChecker(Config, MsDb, Firebird);
            EventHandlerEljur EventHandler = new EventHandlerEljur(Config, MsDb, Firebird, MsDbChecker);
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            var GetDataFb = EventHandler.GetDataFb(cancellationTokenSource.Token);

            var SendNotifyParents = EventHandler.SendNotifyParents(cancellationTokenSource.Token);

            var CheckMsDb = EventHandler.ChecTimekMsDb(cancellationTokenSource.Token, new Action(delegate
            {
                cancellationTokenSource.Cancel();
                Task.WaitAll(GetDataFb, SendNotifyParents);
                Task.Delay(60000);
                //restart
                SqlConnection.ClearAllPools();                              
                Run(MainMethodArgs.ToArray());

            }));
       
            while (true) { }
        }
   
    }
}

