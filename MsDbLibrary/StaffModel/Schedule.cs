using System;
using System.ComponentModel.DataAnnotations;


namespace MsDbLibraryNS.StaffModel
{
    public class Schedule
    {
        [Key]
        public int ScheduleId { get; set; }
        [MaxLength(10)]
        public String Clas { get; set; }
        public TimeSpan StartTimeLessons { get; set; }
        public TimeSpan EndTimeLessons { get; set; }
    }
}
