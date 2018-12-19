using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eljur_notifier.StaffModel
{
    public class Pupil
    {
        //[Key]
        //[ForeignKey("PupilId")]
        public int PupilId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string FullFIO { get; set; }
        public string Class { get; set; }
    }
}
