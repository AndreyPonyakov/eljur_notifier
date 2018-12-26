using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eljur_notifier.StaffModel
{
    public class Schedule
    {
        [Key]
        public int ScheduleId { get; set; }
        [MaxLength(10)]
        public string Clas { get; set; }
        public TimeSpan StartTimeLessons { get; set; }
        public TimeSpan EndTimeLessons { get; set; }
    }
}
