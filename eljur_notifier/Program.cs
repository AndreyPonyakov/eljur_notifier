using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using eljur_notifier;

namespace eljur_notifier
{
    class Program
    {
        static void Main(string[] args)
        {

            var Config = new Config();
            Task taskGetDataFb = new Task(() => GetDataFb(Config.ConnectStr));
            taskGetDataFb.Start();


            
            //var Firebird = new Firebird(Config.ConnectStr);
            //var staff = new List<object[]>();


            //staff = Firebird.getOneStaff(1040);
            //staff = Firebird.getAllStaff();

            //DateTime curTime = DateTime.Now;
            //DateTime curTime = Convert.ToDateTime("2010-12-25 16:58:00");
            //DateTime curTimeSubstracted = curTime.Add(new TimeSpan(0, -1, 0));
            //DateTime curTimeFuture = curTime.AddMinutes(1);

            //Console.WriteLine(curTime.GetType());
            //Console.WriteLine(curTime.ToLongTimeString());
            //Console.WriteLine(curTime.AddMinutes(1).ToLongTimeString());
            //Console.WriteLine(curTimeSubstracted.ToLongTimeString());


            //Console.WriteLine(curTime.TotalMinutes);
            //Console.WriteLine(curTime.ToShortTimeString());

            //Console.WriteLine(curTime);
            //Console.WriteLine(curTime.TotalMinutes.ToString());

            //var curStaff = Firebird.getStaffByTimeStamp(curTime);
            Console.ReadKey();
        }

        static void GetDataFb(String ConnectStr)
        {           
            var Firebird = new Firebird(ConnectStr);         
            var timerFb = new System.Timers.Timer();
            timerFb.AutoReset = true;
            timerFb.Elapsed += delegate { t_Elapsed(Firebird); };
            timerFb.Interval = GetInterval();
            timerFb.Start();
          
        }

        static double GetInterval()
        {
            DateTime now = DateTime.Now;
            //return ((60 - now.Second) * 1000 - now.Millisecond);
            return 5000;
        }

        static void t_Elapsed(Firebird Firebird)
        {
            //Console.WriteLine(DateTime.Now.ToString("o"));
            //DateTime curTime = DateTime.Now;
            DateTime curTime = Convert.ToDateTime("2010-12-25 09:24:00");
            DateTime curTimeSubstracted = curTime.Add(new TimeSpan(0, -1, 0));
            DateTime curTimeFuture = curTime.AddMinutes(1);


            //Console.WriteLine(curTime.GetType());
            //Console.WriteLine(curTime.ToLongTimeString());
            //Console.WriteLine(curTime.AddMinutes(1).ToLongTimeString());
            //Console.WriteLine(curTimeSubstracted.ToLongTimeString());

            var curStaff = Firebird.getStaffByTimeStamp(curTime);
          
        }

    }
}

