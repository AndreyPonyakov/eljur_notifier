using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using MsDbLibraryNS.MsDbNS.RequesterNS;
using MsDbLibraryNS.MsDbNS.CheckerNS;


namespace MsDbLibraryNS.MsDbNS.UpdaterNS.Tests
{
    [TestClass()]
    public class MsDbUpdaterTests
    {
        [TestMethod()]
        public void UpdateSchedulesDbTest()
        {
            MsDbUpdater msDbUpdater = new MsDbUpdater("name=StaffContextTests");
            var AllClasses = getClassesListTest();
            msDbUpdater.UpdateSchedulesDb(AllClasses);
        }

        [TestMethod()]
        public void UpdateStaffDbTest()
        {
            MsDbUpdater msDbUpdater = new MsDbUpdater("name=StaffContextTests");
            var AllStaff = getStaffListTest();
            msDbUpdater.UpdateStaffDb(AllStaff);

            MsDbRequester msDbRequester = new MsDbRequester("name=StaffContextTests");
            String FullFIO1 = msDbRequester.getFullFIOByPupilIdOld(5000);
            String FullFIO2 = msDbRequester.getFullFIOByPupilIdOld(5001);
            String FullFIO3 = msDbRequester.getFullFIOByPupilIdOld(5002);
            Assert.IsTrue(FullFIO1 == "Иванов Иван Иванович");
            Assert.IsTrue(FullFIO2 == "Петров Петр Петрович");
            Assert.IsTrue(FullFIO3 == "Сидоров Сидор Сидорович");
            EmptyChecker emptyChecker = new EmptyChecker("StaffContextTests");
            Assert.IsTrue(false == emptyChecker.IsTableEmpty("Schedules"));
        }

        [TestMethod()]
        public void UpdateMsDbTest()
        {
            MsDbUpdater msDbUpdater = new MsDbUpdater("name=StaffContextTests");
            var AllStaff = getStaffListTest();
            var AllClasses = getClassesListTest();
            msDbUpdater.UpdateMsDb(AllStaff, AllClasses);

            EmptyChecker emptyChecker = new EmptyChecker("StaffContextTests");
            Assert.IsTrue(false == emptyChecker.IsTableEmpty("Schedules"));
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
        public List<object[]> getClassesListTest()
        {
            var AllClasses = new List<object[]>();
            object[] clas1 = new object[3];
            clas1[0] = 5000;
            clas1[1] = TimeSpan.Parse("07:00:00");
            clas1[2] = TimeSpan.Parse("13:00:00");
            object[] clas2 = new object[3];
            clas2[0] = 5000;
            clas2[1] = TimeSpan.Parse("07:00:00");
            clas2[2] = TimeSpan.Parse("13:00:00");
            AllClasses.Add(clas2);
            object[] clas3 = new object[3];
            clas3[0] = 5000;
            clas3[1] = TimeSpan.Parse("07:00:00");
            clas3[2] = TimeSpan.Parse("13:00:00");
            AllClasses.Add(clas3);
            return AllClasses;
        }

    }
}

