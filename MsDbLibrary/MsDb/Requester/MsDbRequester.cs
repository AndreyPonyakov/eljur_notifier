using System;
using System.Linq;
using MsDbLibraryNS.StaffModel;
using System.Collections.Generic;




namespace MsDbLibraryNS.MsDbNS.RequesterNS
{
    public class MsDbRequester : MsDbBaseClass
    {
        internal protected String nameorConnectionString { get; set; }

        public MsDbRequester(String NameorConnectionString = "name=StaffContext") 
            : base(new Message(), new StaffContext(NameorConnectionString)) {
            this.nameorConnectionString = NameorConnectionString;
        }

        public static string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "АБВ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public Event getEventdByPupilIdOld(int PupilIdOld)
        {
            using (this.StaffCtx = new StaffContext(nameorConnectionString))
            {
                var query = from e in StaffCtx.Events
                            where e.PupilIdOld == PupilIdOld
                            select e;
                Event retEvent = query.SingleOrDefault();
                return retEvent;
            }
        }

        public int getPupilIdOldByFullFio(String FullFIO)
        {
            using (this.StaffCtx = new StaffContext(nameorConnectionString))
            {
                var query = from p in StaffCtx.Pupils
                            where p.FullFIO == FullFIO
                            select p.PupilIdOld;          
                int PupilIdOld = query.SingleOrDefault();
                return PupilIdOld;
            }
        }

        public String getClasByPupilIdOld(int PupilIdOld)
        {
            using (this.StaffCtx = new StaffContext(nameorConnectionString))
            {
                var query = from p in StaffCtx.Pupils
                            where p.PupilIdOld == PupilIdOld
                            select p.Clas;
                String Clas = query.SingleOrDefault().ToString();
                return Clas;
            }
        }

        public TimeSpan getEndTimeLessonsByClas(String Clas)
        {
            using (this.StaffCtx = new StaffContext(nameorConnectionString))
            {
                var query = from s in StaffCtx.Schedules
                            where s.Clas == Clas
                            select s.EndTimeLessons;
                TimeSpan EndTimeLessons = query.SingleOrDefault();
                return EndTimeLessons;
            }
        }

        public TimeSpan getStartTimeLessonsByClas(String Clas)
        {
            using (this.StaffCtx = new StaffContext(nameorConnectionString))
            {
                var query = from s in StaffCtx.Schedules
                            where s.Clas == Clas
                            select s.StartTimeLessons;
                TimeSpan StartTimeLessons = query.SingleOrDefault();
                return StartTimeLessons;
            }
        }

        public int getEljurAccountIdByPupilIdOld(int PupilIdOld)
        {
            using (this.StaffCtx = new StaffContext(nameorConnectionString))
            {
                var query = from p in StaffCtx.Pupils
                            where p.PupilIdOld == PupilIdOld
                            select p.EljurAccountId;
                int EljurAccountId = query.SingleOrDefault();
                return EljurAccountId;
            }
        }


        public String getFullFIOByPupilIdOld(int PupilIdOld)
        {
            using (this.StaffCtx = new StaffContext(nameorConnectionString))
            {
                var query = from p in StaffCtx.Pupils
                            where p.PupilIdOld == PupilIdOld
                            select p.FullFIO;
                String FullFIO = query.SingleOrDefault();
                return FullFIO;
            }
        }

        public Boolean getNotifyEnableByPupilIdOld(int PupilIdOld)
        {
            using (this.StaffCtx = new StaffContext(nameorConnectionString))
            {
                var query = from p in StaffCtx.Pupils
                            where p.PupilIdOld == PupilIdOld
                            select p.NotifyEnable;
                Boolean NotifyEnable = query.SingleOrDefault();
                return NotifyEnable;
            }
        }

        public String getEventNameByPupilIdOld(int PupilIdOld)
        {
            using (this.StaffCtx = new StaffContext(nameorConnectionString))
            {
                var query = from e in StaffCtx.Events
                            where e.PupilIdOld == PupilIdOld
                            select e.EventName;
                String EventName = query.SingleOrDefault();
                return EventName;
            }
        }

        public Boolean getStatusNotifyWasSendByPupilIdOld(int PupilIdOld)
        {
            using (this.StaffCtx = new StaffContext(nameorConnectionString))
            {
                var query = from e in StaffCtx.Events
                            where e.PupilIdOld == PupilIdOld
                            select e.NotifyWasSend;
                Boolean NotifyWasSend = query.SingleOrDefault();
                return NotifyWasSend;
            }
        }

        public List<Pupil> getListAllPupils()
        {
            using (this.StaffCtx = new StaffContext(nameorConnectionString))
            {
                var query = from p in StaffCtx.Pupils
                            select p;
                var list = new List<Pupil>();
                list = query.ToList();
                return list;
            }

        }


    }
}
