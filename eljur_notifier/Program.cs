using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using eljur_notifier;
using eljur_notifier.StaffModel;

namespace eljur_notifier
{
    class Program
    {
        static void Main(string[] args)
        {

            var Config = new Config();
            //var StaffDb = new StaffDb();
            //Task taskGetDataFb = new Task(() => GetDataFb(Config.ConnectStr, Config.IntervalRequest));
            //Task taskSendNotifyParents = new Task(() => SendNotifyParents(Config.EljurApiTocken, Config.FrenchLeaveInterval));
            //taskGetDataFb.Start();
            //taskSendNotifyParents.Start();



            using (StaffContext context = new StaffContext())
            {
                Console.WriteLine("Inside using");

                Pupil firstStudent = new Pupil { PupilId = 1, FirstName = "Иван", LastName = "Иванов" , MiddleName = "Иванович", FullFIO = "Иван Иванов Иванович", Class = "1B", };
                context.Pupils.Add(firstStudent);
                context.SaveChanges();
                Console.WriteLine("firstStudent success saved");

                var students = context.Pupils;
                Console.WriteLine("List of objects:");
                foreach (Pupil p in students)
                {
                    Console.WriteLine("{0}.{1} - {2}", p.PupilId, p.FirstName, p.LastName, p.MiddleName, p.FullFIO, p.Class);
                }


             

            }





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

    
        static void t_Elapsed(Firebird Firebird, TimeSpan IntervalRequest)
        {
            //DateTime curTime = Convert.ToDateTime("2010-12-25 09:24:00");
            DateTime curTime = DateTime.Now;
            //curTime = curTime.Add(new TimeSpan(-8, 0, 0));


            //var curStaff = Firebird.getStaffByTimeStamp(curTime, IntervalRequest);

            //var curStaff = Firebird.getAllStaff();

            //using (StaffDb db = new StaffDb())
            //{
            //    Console.WriteLine("Inside using");
            //    //foreach (object[] row in curStaff)
            //    //{
            //        Staff firstStudent = new Staff { Staff_Id = 1, FirstName = "Tom", LastName = "Nicht" };
            //        db.Staffs.Add(firstStudent);
            //        db.SaveChanges();
            //        Console.WriteLine("firstStudent success saved");

            //        var students = db.Staffs;
            //        Console.WriteLine("List of objects:");
            //        foreach (Staff u in students)
            //        {
            //            Console.WriteLine("{0}.{1} - {2}", u.FirstName, u.LastName, u.Staff_Id);
            //        }


            //        //break;
            //    //}

            //}
          
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

