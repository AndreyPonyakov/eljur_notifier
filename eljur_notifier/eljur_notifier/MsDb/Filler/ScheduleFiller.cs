using System;
using System.Collections.Generic;
using eljur_notifier.StaffModel;
using eljur_notifier.EljurNS;
using eljur_notifier.AppCommonNS;

namespace eljur_notifier.MsDbNS.FillerNS
{
    public class ScheduleFiller : EljurBaseClass
    {
        internal protected String nameorConnectionString { get; set; }

        public ScheduleFiller(String NameorConnectionString = "name=StaffContext") 
            : base(new Message(), new StaffContext(NameorConnectionString), new EljurApiRequester()) {
            this.nameorConnectionString = NameorConnectionString;
        }
   
        public void FillSchedulesDb(List<object[]> AllClasses)
        {
            using (this.StaffCtx = new StaffContext(nameorConnectionString))
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
