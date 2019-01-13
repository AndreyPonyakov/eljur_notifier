using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using MsDbLibraryNS.MsDbNS.RequesterNS;

namespace MsDbLibraryNS.MsDbNS.UpdaterNS.StaffUpdaterNS.Tests
{
    [TestClass()]
    public class MainStaffUpdaterTests
    {
        [TestMethod()]
        public void MainUpdateStaffTest()
        {
            MainStaffUpdater mainStaffUpdater = new MainStaffUpdater("name=StaffContextTests");
            var AllStaff = getStaffListTest();
            mainStaffUpdater.MainUpdateStaff(AllStaff);

            MsDbRequester msDbRequester = new MsDbRequester("name=StaffContextTests");
            String FullFIO1 = msDbRequester.getFullFIOByPupilIdOld(5000);
            String FullFIO2 = msDbRequester.getFullFIOByPupilIdOld(5001);
            String FullFIO3 = msDbRequester.getFullFIOByPupilIdOld(5002);
            Assert.IsTrue(FullFIO1 == "Иванов Иван Иванович");
            Assert.IsTrue(FullFIO2 == "Петров Петр Петрович");
            Assert.IsTrue(FullFIO3 == "Сидоров Сидор Сидорович");
        }

        public List<object[]> getStaffListTest()
        {
            var AllStaff = new List<object[]>();
            object[] student1 = new object[23];
            student1[0] = 5000;
            student1[1] = "Иванов";
            student1[2] = "Иван";
            student1[3] = "Иванович";
            student1[22] = "Иванов Иван Иванович";
            student1[21] = "1А";
            student1[20] = 666;
            AllStaff.Add(student1);
            object[] student2 = new object[23];
            student2[0] = 5001;
            student2[1] = "Петров";
            student2[2] = "Петр";
            student2[3] = "Петрович";
            student2[22] = "Петров Петр Петрович";
            student2[21] = "1А";
            student2[20] = 666;
            AllStaff.Add(student2);
            object[] student3 = new object[23];
            student3[0] = 5002;
            student3[1] = "Сидоров";
            student3[2] = "Сидор";
            student3[3] = "Сидорович";
            student3[22] = "Сидоров Сидор Сидорович";
            student3[21] = "1А";
            student3[20] = 666;
            AllStaff.Add(student3);
            return AllStaff;
        }

    }
}

