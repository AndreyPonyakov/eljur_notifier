using System;
using System.Linq;
using eljur_notifier.AppCommonNS;
using MsDbLibraryNS.StaffModel;


namespace MsDbLibraryNS.MsDbNS.SetterNS
{
    public class MsDbSetter : MsDbBaseClass
    {
        internal protected String nameorConnectionString { get; set; }

        public MsDbSetter(String NameorConnectionString = "name=StaffContext") 
            : base(new Message(), new StaffContext(NameorConnectionString)) {
            this.nameorConnectionString = NameorConnectionString;
        }

        public void SetClasByPupilIdOld(int PupilIdOld, String Clas)
        {
            using (this.StaffCtx = new StaffContext(nameorConnectionString))
            {
                var result = StaffCtx.Pupils.SingleOrDefault(e => e.PupilIdOld == PupilIdOld);
                if (result != null)
                {
                    result.Clas = Clas;
                    StaffCtx.SaveChanges();
                    message.Display("Clas to " + PupilIdOld + " PupilIdOld was updated", "Info");
                }
            }
        }


        public void SetStatusWentHomeTooEarly(int PupilIdOld)
        {
            using (this.StaffCtx = new StaffContext(nameorConnectionString))
            {
                var result = StaffCtx.Events.SingleOrDefault(e => e.PupilIdOld == PupilIdOld);
                if (result != null)
                {
                    result.EventName = "Прогул";
                    StaffCtx.SaveChanges();
                    message.Display("WentHomeTooEarly to " + PupilIdOld + " PupilIdOld was set", "Trace");
                }
            }
        }


        public void SetStatusCameTooLate(int PupilIdOld)
        {
            using (this.StaffCtx = new StaffContext(nameorConnectionString))
            {
                var result = StaffCtx.Events.SingleOrDefault(e => e.PupilIdOld == PupilIdOld);
                if (result != null)
                {
                    result.EventName = "Опоздал";
                    StaffCtx.SaveChanges();
                    message.Display("CameTooLate to " + PupilIdOld + " PupilIdOld was set", "Trace");
                }
            }
        }


        public void SetStatusNotifyWasSend(int PupilIdOld)
        {
            using (this.StaffCtx = new StaffContext(nameorConnectionString))
            {
                var result = StaffCtx.Events.SingleOrDefault(e => e.PupilIdOld == PupilIdOld);
                if (result != null)
                {
                    result.NotifyWasSend = true;
                    StaffCtx.SaveChanges();
                    message.Display("Status NotifyWasSend to " + PupilIdOld + " PupilIdOld was set", "Trace");
                }
            }
        }

        public void SetOneFullEventForTesting(Event TestEvent)
        {
            using (this.StaffCtx = new StaffContext(nameorConnectionString))
            {
                StaffCtx.Events.Add(TestEvent);
                StaffCtx.SaveChanges();
                message.Display("TestEvent success saved", "Warn");
            }
        }

        public void SetDelAllEventsForTesting()
        {
            using (this.StaffCtx = new StaffContext(nameorConnectionString))
            {
                var rows = from e in StaffCtx.Events
                           select e;
                foreach (var row in rows)
                {
                    StaffCtx.Events.Remove(row);
                }
                StaffCtx.SaveChanges();
                message.Display("TestEvent success removed", "Warn");
            }
        }


        public void SetOneTestPupilForTesting(Pupil TestPupil)
        {
            using (this.StaffCtx = new StaffContext(nameorConnectionString))
            {
                StaffCtx.Pupils.Add(TestPupil);
                StaffCtx.SaveChanges();
                message.Display("TestPupil success saved", "Warn");
            }
        }

        public void SetDelAllPupilsForTesting()
        {
            using (this.StaffCtx = new StaffContext(nameorConnectionString))
            {
                var rows = from p in StaffCtx.Pupils
                           select p;
                foreach (var row in rows)
                {
                    StaffCtx.Pupils.Remove(row);
                }
                StaffCtx.SaveChanges();
                message.Display("TestPupil success removed", "Warn");
            }
        }


        public void SetTestScheduleForTesting(Schedule TestSchedule)
        {
            using (this.StaffCtx = new StaffContext(nameorConnectionString))
            {
                StaffCtx.Schedules.Add(TestSchedule);
                StaffCtx.SaveChanges();
                message.Display("TestSchedule success saved", "Warn");
            }
        }

        public void SetDelAllSchedulesForTesting()
        {
            using (this.StaffCtx = new StaffContext(nameorConnectionString))
            {
                var rows = from s in StaffCtx.Schedules
                           select s;
                foreach (var row in rows)
                {
                    StaffCtx.Schedules.Remove(row);
                }
                StaffCtx.SaveChanges();
                message.Display("TestSchedule success removed", "Warn");
            }
        }



    }
}
