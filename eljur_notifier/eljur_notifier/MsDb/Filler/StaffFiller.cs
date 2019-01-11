using System;
using System.Collections.Generic;
using eljur_notifier.AppCommonNS;
using eljur_notifier.StaffModel;
using eljur_notifier.EljurNS;


namespace eljur_notifier.MsDbNS.FillerNS
{
    public class StaffFiller : EljurBaseClass
    {
        public StaffFiller() : base(new Message(), new StaffContext(), new EljurApiRequester()) { }
 
        public void FillStaffDb(List<object[]> AllStaff)
        {
            using (this.StaffCtx = new StaffContext())
            {
                foreach (object[] row in AllStaff)
                {

                    Pupil Student = new Pupil();
                    Student.PupilIdOld = Convert.ToInt32(row[0]);
                    Student.FirstName = row[2].ToString();
                    Student.LastName = row[1].ToString();
                    Student.MiddleName = row[3].ToString();
                    Student.FullFIO = row[22].ToString();

                    String clas = eljurApiRequester.getClasByFullFIO(Student.FullFIO);
                    Student.Clas = clas;

                    int eljurAccountId = eljurApiRequester.getEljurAccountIdByFullFIO(Student.FullFIO);
                    Student.EljurAccountId = eljurAccountId;

                    StaffCtx.Pupils.Add(Student);
                    StaffCtx.SaveChanges();
                    message.Display("Student success saved", "Warn");
                }
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
