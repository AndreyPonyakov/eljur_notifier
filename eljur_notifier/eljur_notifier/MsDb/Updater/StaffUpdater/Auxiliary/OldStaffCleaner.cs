using System;
using System.Collections.Generic;
using System.Linq;
using eljur_notifier.AppCommonNS;
using eljur_notifier.StaffModel;

namespace eljur_notifier.MsDbNS.UpdaterNS.StaffUpdaterNS
{
    public class OldStaffCleaner : EljurBaseClass
    {
        public OldStaffCleaner() : base(new Message(), new StaffContext()) { }
  
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
                        message.Display("Pupil with " + p.PupilIdOld + " PupilIdOld was cleared in Pupils Table", "Warn");
                    }
                }
            }
        }



    }
}
