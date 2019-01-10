using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eljur_notifier.AppCommon;
using eljur_notifier.StaffModel;

namespace eljur_notifier.MsDbNS.UpdaterNS.StaffUpdaterNS
{
    public class OldStaffCleaner
    {
        internal protected Message message { get; set; }
        internal protected StaffContext StaffCtx { get; set; }

        public OldStaffCleaner()
        {
            this.message = new Message();
        }

        public void CleanOldStaff(List<object[]> AllStaff)
        {
            using (this.StaffCtx = new StaffContext())
            {
                foreach (Pupil p in StaffCtx.Pupils)
                {
                    var result = AllStaff.SingleOrDefault(s => Convert.ToInt32(s[0]) == p.PupilIdOld );
                    if (result == null)
                    {
                        StaffCtx.Pupils.Remove(p);
                        StaffCtx.SaveChanges();
                    }
                }
            }
        }



    }
}
