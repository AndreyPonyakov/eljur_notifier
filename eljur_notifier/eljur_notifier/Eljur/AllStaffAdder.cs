using System;
using System.Collections.Generic;
using eljur_notifier.AppCommonNS;
using MsDbLibraryNS;

namespace eljur_notifier.EljurNS
{
    public class AllStaffAdder : EljurBaseClass
    {
        public AllStaffAdder() : base(new Message(), new EljurApiRequester()) { }

        public List<object[]> AddClassAndEljurId(List<object[]> AllStaff)
        {
            foreach (object[] row in AllStaff)
            {
                String FullFIO = row[22].ToString();
                String Clas = eljurApiRequester.getClasByFullFIO(FullFIO);
                row[21] = Clas;
                int eljurAccountId = eljurApiRequester.getEljurAccountIdByFullFIO(FullFIO);
                row[20] = eljurAccountId;
            }
            return AllStaff;
        }


    }
}
