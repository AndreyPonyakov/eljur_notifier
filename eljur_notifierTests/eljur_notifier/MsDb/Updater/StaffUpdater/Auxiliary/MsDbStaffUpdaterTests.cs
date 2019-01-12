using Microsoft.VisualStudio.TestTools.UnitTesting;
using eljur_notifier.MsDbNS.UpdaterNS.StaffUpdaterNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eljur_notifier.MsDbNS.RequesterNS;

namespace eljur_notifier.MsDbNS.UpdaterNS.StaffUpdaterNS.Tests
{
    [TestClass()]
    public class MsDbStaffUpdaterTests
    {
        [TestMethod()]
        public void UpdateStaffTest()
        {
            MsDbStaffUpdater msDbStaffUpdater = new MsDbStaffUpdater("name=StaffContextTests");
            var AllStaff = getStaffListTest();
            msDbStaffUpdater.UpdateStaff(AllStaff);
            MsDbRequester msDbRequester = new MsDbRequester("name=StaffContextTests");
            String FullFIO1 = msDbRequester.getFullFIOByPupilIdOld(5000);
            String FullFIO2 = msDbRequester.getFullFIOByPupilIdOld(5001);
            String FullFIO3 = msDbRequester.getFullFIOByPupilIdOld(5002);
            Assert.IsTrue(FullFIO1 == "Иванова Ивана Ивановна");
            Assert.IsTrue(FullFIO2 == "Петрова Петра Петровна");
            Assert.IsTrue(FullFIO3 == "Сидорова Сидора Сидоровна");
        }

        public List<object[]> getStaffListTest()
        {
            var AllStaff = new List<object[]>();
            object[] student1 = new object[23];
            student1[0] = 5000;
            student1[1] = "Иванова";
            student1[2] = "Ивана";
            student1[3] = "Ивановна";
            student1[22] = "Иванова Ивана Ивановна";
            AllStaff.Add(student1);
            object[] student2 = new object[23];
            student2[0] = 5001;
            student2[1] = "Петрова";
            student2[2] = "Петра";
            student2[3] = "Петровна";
            student2[22] = "Петрова Петра Петровна";
            AllStaff.Add(student2);
            object[] student3 = new object[23];
            student3[0] = 5002;
            student3[1] = "Сидорова";
            student3[2] = "Сидора";
            student3[3] = "Сидоровна";
            student3[22] = "Сидорова Сидора Сидоровна";
            AllStaff.Add(student3);
            return AllStaff;
        }

    }
}