using System;
using System.Collections.Generic;
using System.Linq;
using eljur_notifier.AppCommonNS;
using eljur_notifier.StaffModel;


namespace eljur_notifier.MsDbNS.UpdaterNS.StaffUpdaterNS
{
    public class OldStaffCleaner : EljurBaseClass
    {
        public OldStaffCleaner(String NameorConnectionString = "name=StaffContext") : base(new Message(), new StaffContext(NameorConnectionString)) { }
  
        public void CleanOldStaff(List<object[]> AllStaff)
        {
            using (this.StaffCtx)
            {
                IQueryable<Pupil> Pupils = from p in StaffCtx.Pupils select p;
                var PupilsToDel = new List<Pupil>();
                foreach (Pupil p in Pupils)
                {
                    var result = AllStaff.SingleOrDefault(s => Convert.ToInt32(s[0]) == p.PupilIdOld );
                    if (result == null)
                    {
                        PupilsToDel.Add(p);                     
                    }
                }

                foreach (Pupil p in PupilsToDel)
                {
                    StaffCtx.Pupils.Remove(p);
                    StaffCtx.SaveChanges();
                    message.Display("Pupil with " + p.PupilIdOld + " PupilIdOld was cleared in Pupils Table", "Warn");
                }

            }
        }



    }
}
