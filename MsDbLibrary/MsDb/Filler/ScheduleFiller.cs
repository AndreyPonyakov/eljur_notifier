using System;
using System.Collections.Generic;
using MsDbLibraryNS.StaffModel;
using eljur_notifier.AppCommonNS;

namespace MsDbLibraryNS.MsDbNS.FillerNS
{
    public class ScheduleFiller : MsDbBaseClass
    {
        internal protected String nameorConnectionString { get; set; }

        public ScheduleFiller(String NameorConnectionString = "name=StaffContext") 
            : base(new Message(), new StaffContext(NameorConnectionString)) {
            this.nameorConnectionString = NameorConnectionString;
        }
   
        public void FillSchedulesDb(List<object[]> AllClasses)
        {
            using (this.StaffCtx = new StaffContext(nameorConnectionString))
            {
                foreach (object[] ScheduleRow in AllClasses)
                {
                    Schedule ScheduleItem = new Schedule();
                    ScheduleItem.Clas = ScheduleRow[0].ToString();
                    ScheduleItem.StartTimeLessons = TimeSpan.Parse(ScheduleRow[1].ToString());
                    ScheduleItem.EndTimeLessons = TimeSpan.Parse(ScheduleRow[2].ToString());
                    StaffCtx.Schedules.Add(ScheduleItem);
                    StaffCtx.SaveChanges();
                    message.Display("ScheduleItem success saved", "Warn");
                }
            }
        }

    }
}
