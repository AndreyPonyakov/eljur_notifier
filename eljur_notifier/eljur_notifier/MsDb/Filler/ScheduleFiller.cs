using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eljur_notifier.StaffModel;
using eljur_notifier.EljurNS;
using eljur_notifier.AppCommonNS;

namespace eljur_notifier.MsDbNS.FillerNS
{
    public class ScheduleFiller
    {
        internal protected Message message { get; set; }
        internal protected Config config { get; set; }
        internal protected StaffContext StaffCtx { get; set; }


        public ScheduleFiller(Config Config)
        {
            this.message = new Message();
            this.config = Config;
        }

        public void FillSchedulesDb()
        {
            using (this.StaffCtx = new StaffContext())
            {
                EljurApiRequester elRequester = new EljurApiRequester(config);
                String[] ClasesArr = elRequester.getClases();
                foreach (String clas in ClasesArr)
                {
                    Schedule ScheduleItem = new Schedule();
                    ScheduleItem.Clas = clas;
                    ScheduleItem.StartTimeLessons = elRequester.getStartTimeLessonsByClas(clas);
                    ScheduleItem.EndTimeLessons = elRequester.getEndTimeLessonsByClas(clas);
                    StaffCtx.Schedules.Add(ScheduleItem);
                    StaffCtx.SaveChanges();
                    message.Display("ScheduleItem success saved", "Warn");
                }
            }
        }

    }
}
