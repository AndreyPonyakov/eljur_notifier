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
        public NewStaffAdder() : base(new Message(), new StaffContext(), new EljurApiRequester()) { }

        public void AddNewPupil(List<object[]> AllStaff)
        {
            using (this.StaffCtx = new StaffContext())
            {
                EljurApiRequester elRequester = new EljurApiRequester();

                foreach (object[] row in AllStaff)
                {
                    var result = StaffCtx.Pupils.SingleOrDefault(p => p.PupilIdOld == Convert.ToInt32(row[0]));
                    if (result == null)
                    {
                        Pupil Student = new Pupil();
                        Student.PupilIdOld = Convert.ToInt32(row[0]);
                        Student.FirstName = row[2].ToString();
                        Student.LastName = row[1].ToString();
                        Student.MiddleName = row[3].ToString();
                        Student.FullFIO = row[22].ToString();

                        String clas = elRequester.getClasByFullFIO(Student.FullFIO);
                        Student.Clas = clas;

                        int eljurAccountId = eljurApiRequester.getEljurAccountIdByFullFIO(Student.FullFIO);
                        Student.EljurAccountId = eljurAccountId;

                        StaffCtx.Pupils.Add(Student);
                        StaffCtx.SaveChanges();
                        message.Display("New Student success saved", "Warn");
                    }
                }

            }

        }

    }
}
