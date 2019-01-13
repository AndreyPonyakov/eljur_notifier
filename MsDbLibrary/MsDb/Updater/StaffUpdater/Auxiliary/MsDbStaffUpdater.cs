using System;
using System.Collections.Generic;
using System.Linq;
using MsDbLibraryNS.StaffModel;
using eljur_notifier.AppCommonNS;

namespace MsDbLibraryNS.MsDbNS.UpdaterNS.StaffUpdaterNS
{
    public class MsDbStaffUpdater : MsDbBaseClass
    {
        internal protected String nameorConnectionString { get; set; }

        public MsDbStaffUpdater(String NameorConnectionString = "name=StaffContext")
            : base(new Message(), new StaffContext(NameorConnectionString)) {
            this.nameorConnectionString = NameorConnectionString;
        }
 
        public void UpdateStaff(List<object[]> AllStaff)
        {
            using (this.StaffCtx = new StaffContext(nameorConnectionString))
            {
                foreach (object[] row in AllStaff)
                {
                    int Int32Row0 = Convert.ToInt32(row[0]);
                    var result = StaffCtx.Pupils.SingleOrDefault(p => p.PupilIdOld == Int32Row0);
                    if (result != null)
                    {
                        result.FirstName = row[2].ToString();
                        result.LastName = row[1].ToString();
                        result.MiddleName = row[3].ToString();
                        result.FullFIO = row[22].ToString();
                        result.FirstName = row[2].ToString();
                        result.Clas = row[21].ToString();
                        result.EljurAccountId = Convert.ToInt32(row[20]);
                    }

                }
                StaffCtx.SaveChanges();

                var students = StaffCtx.Pupils;
                message.Display("List of objects:", "Info");
                foreach (Pupil p in students)
                {
                    message.Display(String.Format("{0}.{1} - {2} - {3} - {4} - {5}", p.PupilId, p.FirstName, p.LastName, p.MiddleName, p.FullFIO, p.Clas), "Info");
                }

            }
        }

    }
}
