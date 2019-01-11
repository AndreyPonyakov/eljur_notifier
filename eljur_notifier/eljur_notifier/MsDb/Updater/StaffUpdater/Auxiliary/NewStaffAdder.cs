using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eljur_notifier.AppCommonNS;
using eljur_notifier.EljurNS;
using eljur_notifier.StaffModel;

namespace eljur_notifier.MsDbNS.UpdaterNS.StaffUpdaterNS
{
    public class NewStaffAdder
    {
        internal protected Message message { get; set; }
        internal protected Config config { get; set; }
        internal protected StaffContext StaffCtx { get; set; }
        internal protected EljurApiRequester eljurApiRequester { get; set; }

        public NewStaffAdder(Config Config)
        {
            this.message = new Message();
            this.config = Config;
            this.eljurApiRequester = new EljurApiRequester(config);
        }

        public void AddNewPupil(List<object[]> AllStaff)
        {
            using (this.StaffCtx = new StaffContext())
            {
                EljurApiRequester elRequester = new EljurApiRequester(config);

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
