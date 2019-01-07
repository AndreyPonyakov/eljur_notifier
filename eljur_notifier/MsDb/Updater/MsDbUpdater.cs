using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eljur_notifier.AppCommon;
using eljur_notifier.StaffModel;
using eljur_notifier.EljurNS;

namespace eljur_notifier.MsDbNS.Updater
{
    class MsDbUpdater
    {
        internal protected Message message { get; set; }
        internal protected Config config { get; set; }
        internal protected StaffContext StaffCtx { get; set; }

        public MsDbUpdater(Config Config)
        {
            this.message = new Message();
            this.config = Config;
        }


        public void UpdateSchedulesDb()
        {
            using (this.StaffCtx = new StaffContext())
            {
                EljurApiRequester elRequester = new EljurApiRequester(config);
                var Clases = elRequester.getClases();
                foreach (String clas in Clases)
                {
                    var ScheduleItem = StaffCtx.Schedules.SingleOrDefault(e => e.Clas == clas);
                    if (ScheduleItem != null)
                    {
                        TimeSpan StartTimeLessons = elRequester.getStartTimeLessonsByClas(clas);
                        ScheduleItem.StartTimeLessons = StartTimeLessons;
                        TimeSpan EndTimeLessons = elRequester.getEndTimeLessonsByClas(clas);
                        ScheduleItem.EndTimeLessons = EndTimeLessons;
                        StaffCtx.SaveChanges();
                        message.Display("ScheduleItem success saved", "Warn");
                    }                    
                }
                var Schedules = StaffCtx.Schedules;
                message.Display("List of objects: ", "Warn");
                foreach (Schedule s in Schedules)
                {
                    message.Display(s.ScheduleId + "." + s.Clas + "-" + s.StartTimeLessons + "-" + s.EndTimeLessons, "Trace");
                }

            }
        }



    }
}
