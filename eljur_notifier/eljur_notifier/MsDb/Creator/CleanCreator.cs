using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eljur_notifier.AppCommonNS;
using eljur_notifier.StaffModel;

namespace eljur_notifier.MsDbNS.CreatorNS
{
    public class CleanCreator
    {
        internal protected Message message { get; set; }
        internal protected StaffContext StaffCtx { get; set; }

        public CleanCreator()
        {
            this.message = new Message();

        }

        public void createCleanMsDb(String conStr)
        {
            using (this.StaffCtx = new StaffContext())
            {
                Pupil firstStudent = new Pupil();
                firstStudent.PupilId = 1;
                firstStudent.PupilIdOld = 5001;
                firstStudent.FirstName = "Иван";
                firstStudent.LastName = "Иванов";
                firstStudent.MiddleName = "Иванович";
                firstStudent.FullFIO = "Иван Иванов Иванович";
                firstStudent.Clas = "1Б";
                firstStudent.EljurAccountId = 666;
                firstStudent.NotifyEnable = false;

                Event firstEvent = new Event();
                firstEvent.EventId = 1;
                firstEvent.PupilIdOld = 5001;
                firstEvent.EventTime = DateTime.Now.TimeOfDay;
                firstEvent.EventName = "Прогул";
                firstEvent.NotifyWasSend = false;



                Schedule firstScheduleToDayItem = new Schedule();
                firstScheduleToDayItem.ScheduleId = 1;
                firstScheduleToDayItem.Clas = "1A";
                firstScheduleToDayItem.StartTimeLessons = DateTime.Now.TimeOfDay;
                firstScheduleToDayItem.EndTimeLessons = DateTime.Now.TimeOfDay;


                StaffCtx.Pupils.Add(firstStudent);
                StaffCtx.Events.Add(firstEvent);
                StaffCtx.Schedules.Add(firstScheduleToDayItem);
                StaffCtx.SaveChanges();
                message.Display("firstStudent success saved", "Warn");

                var students = StaffCtx.Pupils;
                var evets = StaffCtx.Events;
                var schedules = StaffCtx.Schedules;
                Console.WriteLine("List of objects:");
                foreach (Pupil p in students)
                {
                    Console.WriteLine("{0}.{1} - {2} - {3} - {4} - {5} - {6}", p.PupilIdOld, p.FirstName, p.LastName, p.MiddleName, p.FullFIO, p.Clas, p.NotifyEnable);
                }
                foreach (Event e in evets)
                {
                    Console.WriteLine("{0}.{1} - {2} - {3} - {4}", e.EventId, e.PupilIdOld, e.EventTime, e.EventName, e.NotifyWasSend);
                }
                foreach (Schedule s in schedules)
                {
                    Console.WriteLine("{0}.{1} - {2} - {3}", s.ScheduleId, s.Clas, s.StartTimeLessons, s.EndTimeLessons);
                }
                this.StaffCtx.Database.ExecuteSqlCommand("TRUNCATE TABLE [Pupils]");
                message.Display("TABLE Pupils was cleared", "Warn");
                this.StaffCtx.Database.ExecuteSqlCommand("TRUNCATE TABLE [Events]");
                message.Display("TABLE Events was cleared", "Warn");
                this.StaffCtx.Database.ExecuteSqlCommand("TRUNCATE TABLE [Schedules]");
                message.Display("TABLE Schedules was cleared", "Warn");

            }
        }




    }
}
