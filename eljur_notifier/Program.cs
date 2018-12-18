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
            Task taskGetDataFb = new Task(() => GetDataFb(Config.ConnectStr, Config.IntervalRequest));
            taskGetDataFb.Start();

            Console.ReadKey();
        }

        static void GetDataFb(String ConnectStr, Double IntervalRequest)
        {           
            var Firebird = new Firebird(ConnectStr);         
            var timerFb = new System.Timers.Timer();
            TimeSpan IntervalRequestTS = TimeSpan.FromMilliseconds(IntervalRequest);
            timerFb.AutoReset = true;
            timerFb.Elapsed += delegate { t_Elapsed(Firebird, IntervalRequestTS); };
            timerFb.Interval = IntervalRequest;
            timerFb.Start();
          
        }

        static double GetInterval()
        {
            //DateTime now = DateTime.Now;
            //return ((60 - now.Second) * 1000 - now.Millisecond);
            //return 61000; //61 sec because Firebird sql operation Between  is inclusive
            return 5000;
        }

        static void t_Elapsed(Firebird Firebird, TimeSpan IntervalRequest)
        {
            //DateTime curTime = Convert.ToDateTime("2010-12-25 09:24:00");
            DateTime curTime = DateTime.Now;
            //curTime = curTime.Add(new TimeSpan(-8, 0, 0));
            var curStaff = Firebird.getStaffByTimeStamp(curTime, IntervalRequest);
          
        }




    }
}

