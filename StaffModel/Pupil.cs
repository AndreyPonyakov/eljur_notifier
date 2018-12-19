using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace eljur_notifier.StaffModel
{
    public class Pupil
    {
        [Key]     
        public int PupilId { get; set; }
        [MaxLength(500)]
        public string FirstName { get; set; }
        [MaxLength(500)]
        public string LastName { get; set; }
        [MaxLength(500)]
        public string MiddleName { get; set; }
        [MaxLength(1000)]
        public string FullFIO { get; set; }
        [MaxLength(10)]
        public string Class { get; set; }
        [MaxLength(500)]
        public string EljurAccount { get; set; }   

    }
}
