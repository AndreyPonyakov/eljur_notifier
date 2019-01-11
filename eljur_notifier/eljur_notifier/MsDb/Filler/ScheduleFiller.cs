using System;
using eljur_notifier.StaffModel;
using eljur_notifier.EljurNS;
using eljur_notifier.AppCommonNS;

namespace eljur_notifier.MsDbNS.FillerNS
{
    public class ScheduleFiller : EljurBaseClass
    {
        public ScheduleFiller() : base(new Message(), new StaffContext(), new EljurApiRequester()) { }
   
        public void FillSchedulesDb()
        {
            using (this.StaffCtx = new StaffContext())
            {
                String[] ClasesArr = eljurApiRequester.getClases();
                foreach (String clas in ClasesArr)
                {
                    Schedule ScheduleItem = new Schedule();
                    ScheduleItem.Clas = clas;
                    ScheduleItem.StartTimeLessons = eljurApiRequester.getStartTimeLessonsByClas(clas);
                    ScheduleItem.EndTimeLessons = eljurApiRequester.getEndTimeLessonsByClas(clas);
                    StaffCtx.Schedules.Add(ScheduleItem);
                    StaffCtx.SaveChanges();
                    message.Display("ScheduleItem success saved", "Warn");
                }
            }
        }

    }
}
