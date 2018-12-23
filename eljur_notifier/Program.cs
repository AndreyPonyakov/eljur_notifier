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
using NLog;


namespace eljur_notifier
{
    class Program
    {
        static void Main(string[] args)
        { 
            Message message = new Message();

            //try
            //{
            //    throw new Exception();
            //}
            //catch (Exception ex)
            //{
            //    message.Display("fatal message", "Fatal", ex);
            //}


            Config Config = new Config();
            TimeSpan IntervalRequestTS = TimeSpan.FromMilliseconds(Config.IntervalRequest);
            Firebird Firebird = new Firebird(Config.ConStrFbDB);
            MsDb MsDb = new MsDb(Config.ConStrMsDB);


      
            Console.WriteLine("MsSQLDB is exist: " + MsDb.IsDbExistVar.ToString());
            if (MsDb.IsDbExistVar)
            {

                MsDb.deleteDb(Config.ConStrMsDB);
                Console.WriteLine("DATABASE was deleted");
            }
            else
            {
                MsDb.createDb(Config.ConStrMsDB);
                Console.WriteLine("TABLE Pupils was cleared");
                Console.WriteLine("TABLE Events was cleared");


                var AllStaff = Firebird.getAllStaff();
                MsDb.FillStaffDb(AllStaff);
            }


         
            //Task taskGetDataFb = new Task(() => GetDataFb(Config.ConnectStr, Config.IntervalRequest));
            Task taskGetDataFb = new Task(() => GetDataFb(Config));
            //Task taskSendNotifyParents = new Task(() => SendNotifyParents(Config.EljurApiTocken, Config.FrenchLeaveInterval));
            taskGetDataFb.Start();
            //taskSendNotifyParents.Start();













            Console.ReadKey();
        }


       







        //static void GetDataFb(String ConnectStr, Double IntervalRequest)
        static void GetDataFb(Config Config)
        {
            //TimeSpan IntervalRequestTS = TimeSpan.FromMilliseconds(Config.IntervalRequest);
            //var Firebird = new Firebird(Config.ConnectStr);









            //var timerFb = new System.Timers.Timer();           
            //timerFb.AutoReset = true;
            //timerFb.Elapsed += delegate { t_Elapsed(Firebird, Config); };
            //timerFb.Interval = Config.IntervalRequest;
            //timerFb.Start();

        }


        //static void t_Elapsed(Firebird Firebird, TimeSpan IntervalRequest)
        static void t_Elapsed(Firebird Firebird, Config Config)
        {
            TimeSpan IntervalRequestTS = TimeSpan.FromMilliseconds(Config.IntervalRequest);
            //DateTime curTime = Convert.ToDateTime("2010-12-25 09:24:00");
            DateTime curTime = DateTime.Now;
            //curTime = curTime.Add(new TimeSpan(-9, 0, 0));

            Console.WriteLine("CurTime is " + curTime);



            var curEvents = Firebird.getStaffByTimeStamp(curTime, IntervalRequestTS);





            MsDb MsDb = new MsDb(Config.ConStrMsDB);

            MsDb.CheckEventsDb(curEvents);

        }

        static void SendNotifyParents(String EljurApiTocken, Double FrenchLeaveInterval)
        {
            while (true)
            {
                Console.WriteLine("I'm from second task!!!!");
                //Task.Delay(1000);
                Thread.Sleep(1000);
            }

        }



    }
}

