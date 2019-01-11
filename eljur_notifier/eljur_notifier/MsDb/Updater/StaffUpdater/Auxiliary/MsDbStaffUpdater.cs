using System;
using System.Collections.Generic;
using System.Linq;
using eljur_notifier.StaffModel;
using eljur_notifier.EljurNS;
using eljur_notifier.AppCommonNS;

namespace eljur_notifier.MsDbNS.UpdaterNS.StaffUpdaterNS
{
    public class MsDbStaffUpdater : EljurBaseClass
    {
        public MsDbStaffUpdater() : base(new Message(), new StaffContext(), new EljurApiRequester()) { }
 
        public void UpdateStaff(List<object[]> AllStaff)
        {
            using (this.StaffCtx = new StaffContext())
            {

                foreach (object[] row in AllStaff)
                {
                    var result = StaffCtx.Pupils.SingleOrDefault(p => p.PupilIdOld == Convert.ToInt32(row[0]));
                    if (result != null)
                    {
                        result.FirstName = row[2].ToString();
                        result.LastName = row[1].ToString();
                        result.MiddleName = row[3].ToString();
                        result.FullFIO = row[22].ToString();
                        result.FirstName = row[2].ToString();

                        String clas = eljurApiRequester.getClasByFullFIO(row[22].ToString());
                        result.Clas = clas;

                        int eljurAccountId = eljurApiRequester.getEljurAccountIdByFullFIO(row[22].ToString());
                        result.EljurAccountId = eljurAccountId;

                        StaffCtx.SaveChanges();
                    }

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
