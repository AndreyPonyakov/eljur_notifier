using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eljur_notifier.StaffModel;
using eljur_notifier.MsDbNS;
using eljur_notifier.AppCommonNS;
using eljur_notifier.MsDbNS.SetterNS;


namespace eljur_notifier.MsDbNS.RequesterNS
{
    public class MsDbRequester
    {
        internal protected Message message { get; set; }
        internal protected MsDbSetter msDbSetter { get; set; }
        internal protected StaffContext StaffCtx { get; set; }

        public MsDbRequester()
        {
            this.message = new Message();
            this.msDbSetter = new MsDbSetter();

        }

        public static string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "АБВ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public int getPupilIdOldByFullFio(String FullFIO)
        {
            using (this.StaffCtx = new StaffContext())
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
            using (this.StaffCtx = new StaffContext())
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
            using (this.StaffCtx = new StaffContext())
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
            using (this.StaffCtx = new StaffContext())
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
            using (this.StaffCtx = new StaffContext())
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
            using (this.StaffCtx = new StaffContext())
            {
                var query = from p in StaffCtx.Pupils
                            where p.PupilIdOld == PupilIdOld
                            select p.FullFIO;
                String FullFIO = query.SingleOrDefault();
                return FullFIO;
            }
        }


    }
}
