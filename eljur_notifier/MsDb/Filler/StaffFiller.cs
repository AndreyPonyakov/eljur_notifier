using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eljur_notifier.AppCommon;
using eljur_notifier.StaffModel;
using eljur_notifier.EljurNS;
using eljur_notifier.MsDbNS.RequesterNS;



namespace eljur_notifier.MsDbNS.FillerNS
{
    class StaffFiller
    {
        internal protected Message message { get; set; }
        internal protected Config config { get; set; }
        internal protected StaffContext StaffCtx { get; set; }
        internal protected Requester requester { get; set; }
        internal protected EljurApiRequester eljurApiRequester { get; set; }
        


        public StaffFiller(Config Config)
        {
            this.message = new Message();
            this.config = Config;
            this.requester = new Requester(config);
            this.eljurApiRequester = new EljurApiRequester(config);
        }


        public void FillStaffDb(List<object[]> AllStaff)
        {
            using (this.StaffCtx = new StaffContext())
            {
                EljurApiRequester elRequester = new EljurApiRequester(config);
                foreach (object[] row in AllStaff)
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
                    message.Display("Student success saved", "Warn");
                }
                var students = StaffCtx.Pupils;
                Console.WriteLine("List of objects:");
                foreach (Pupil p in students)
                {
                    Console.WriteLine("{0}.{1} - {2} - {3} - {4} - {5}", p.PupilId, p.FirstName, p.LastName, p.MiddleName, p.FullFIO, p.Clas);
                }

            }
        }

    }
}
