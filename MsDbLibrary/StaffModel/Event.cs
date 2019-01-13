using System;
using System.ComponentModel.DataAnnotations;


namespace MsDbLibraryNS.StaffModel
{
    public class Event
    {     
        [Key]     
        public int EventId { get; set; }
        public int PupilIdOld { get; set; }
        public TimeSpan EventTime { get; set; }
        [MaxLength(500)]
        public string EventName { get; set; }          
        public Boolean NotifyWasSend { get; set; }
    }
}
  