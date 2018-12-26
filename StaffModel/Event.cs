using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace eljur_notifier.StaffModel
{
    public class Event
    {     
        [Key]     
        public int EventId { get; set; }
        public int PupilIdOld { get; set; }
        public TimeSpan EventTime { get; set; }
        [MaxLength(500)]
        public string EventName { get; set; }       
        public Boolean NotifyEnable { get; set; }
        public Boolean NotifyEnableDirector { get; set; }
        public Boolean NotifyWasSend { get; set; }
        public Boolean NotifyWasSendDirector { get; set; }
    }
}
