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
            var Firebird = new Firebird(Config.ConnectStr);
            var staff = new List<object[]>();
            //staff = Firebird.getOneStaff(1040);
            //staff = Firebird.getAllStaff();

            //DateTime curTime = DateTime.Now;
            DateTime curTime = Convert.ToDateTime("2010-12-25 16:58:00");      
            DateTime curTimeSubstracted = curTime.Add(new TimeSpan(0, -1, 0));
            DateTime curTimeFuture = curTime.AddMinutes(1);

            Console.WriteLine(curTime.GetType());
            Console.WriteLine(curTime.ToLongTimeString());
            Console.WriteLine(curTime.AddMinutes(1).ToLongTimeString());  
            Console.WriteLine(curTimeSubstracted.ToLongTimeString());


            //Console.WriteLine(curTime.TotalMinutes);
            //Console.WriteLine(curTime.ToShortTimeString());

            //Console.WriteLine(curTime);
            //Console.WriteLine(curTime.TotalMinutes.ToString());

            //var curStaff = Firebird.getStaffByTimeStamp(curTime);
            Console.ReadKey();
        }
    }
}
