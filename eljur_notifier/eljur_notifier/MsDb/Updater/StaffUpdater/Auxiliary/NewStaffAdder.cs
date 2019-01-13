using System;
using System.Collections.Generic;
using System.Linq;
using eljur_notifier.AppCommonNS;
using eljur_notifier.EljurNS;
using eljur_notifier.StaffModel;

namespace eljur_notifier.MsDbNS.UpdaterNS.StaffUpdaterNS
{
    public class NewStaffAdder : EljurBaseClass
    {
        internal protected String nameorConnectionString { get; set; }

        public NewStaffAdder(String NameorConnectionString = "name=StaffContext") 
            : base(new Message(), new StaffContext(NameorConnectionString)) {
            this.nameorConnectionString = NameorConnectionString;
        }

        public void AddNewPupil(List<object[]> AllStaff)
        {
            using (this.StaffCtx = new StaffContext(nameorConnectionString))
            {
                var PupilsToAdd = new List<Pupil>();
                foreach (object[] row in AllStaff)
                {
                    int Int32Row0 = Convert.ToInt32(row[0]);
                    var result = StaffCtx.Pupils.SingleOrDefault(p => p.PupilIdOld == Int32Row0);
                    if (result == null)
                    {
                        Pupil Student = new Pupil();
                        Student.PupilIdOld = Convert.ToInt32(row[0]);
                        Student.FirstName = row[2].ToString();
                        Student.LastName = row[1].ToString();
                        Student.MiddleName = row[3].ToString();
                        Student.FullFIO = row[22].ToString();
                        Student.Clas = row[21].ToString();
                        Student.EljurAccountId = Convert.ToInt32(row[20]);                    
                        PupilsToAdd.Add(Student);                      
                    }
                }
                foreach (Pupil p in PupilsToAdd)
                {
                    StaffCtx.Pupils.Add(p);
                    StaffCtx.SaveChanges();
                    message.Display("New Student success saved", "Warn");
                }

            }

        }

    }
}
