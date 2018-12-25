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
            //Message message = new Message();
            //DateTime Dt1 = Convert.ToDateTime("2000-12-31 23:59:59");
            //DateTime Dt2 = Convert.ToDateTime("2001-1-1 0:0:1");
            //TimeSpan diff = Dt2.Subtract(Dt1);
            ////TimeSpan diff = Dt1.Subtract(Dt2);

            //message.Display("Dt1 is " + Dt1.ToString() , "Warn");
            //message.Display("Dt2 is " + Dt2.ToString(), "Warn");
            //message.Display("diff is " + diff.ToString(), "Warn");
            //message.Display("diff is " + diff.TotalMilliseconds.ToString(), "Warn");
            //message.Display("diff is " + diff.TotalMilliseconds.GetType(), "Warn");
            //Console.ReadKey();


            Message message = new Message();
            Config Config = new Config();         
            Firebird Firebird = new Firebird(Config.ConStrFbDB);
            MsDb MsDb = new MsDb(Config.ConStrMsDB);
            MsDbChecker MsDbChecker = new MsDbChecker(MsDb, Config, Firebird);
            EventHandlerEljur EventHandler = new EventHandlerEljur(Config, Firebird, MsDb, MsDbChecker);
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            var GetDataFb = EventHandler.GetDataFb(cancellationTokenSource.Token);

            var SendNotifyParents = EventHandler.SendNotifyParents(cancellationTokenSource.Token);

            var CheckMsDb = EventHandler.CheckMsDb(cancellationTokenSource.Token, new Action(delegate
            {
                message.Display("message from actionAtMidnight", "Warn");
                cancellationTokenSource.Cancel();
                Task.WaitAll(GetDataFb, SendNotifyParents);
                //Task.WaitAll(SendNotifyParents);
                //restart
                SqlConnection.ClearAllPools();
                message.Display("message from actionAtMidnight", "Warn");
                Run(MainMethodArgs.ToArray());

            }));

            
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
            cancellationTokenSource.Cancel();
            //Task.WaitAll(GetDataFb, SendNotifyParents);
            //restart
            Run(MainMethodArgs.ToArray());


            //CloseProgram(new Action(delegate
            //{
            //    SMTP smtp = new SMTP();
            //    smtp.SendEmail(MesStr + exception.Message);
            //    //smtp.SendEmail(this.exeption.ToString());
            //    Console.WriteLine(MesStr);
            //    Thread.Sleep(10000);

            //}));
            //public static void CloseProgram(Action actionBeforeClosing)
            //{
            //    actionBeforeClosing();
            //    CloseProgram();
            //}
            //public static void CloseProgram()
            //{
            //    //Process.GetCurrentProcess().Kill();
            //    Environment.Exit(1);
            //}
        }
   
    }
}

