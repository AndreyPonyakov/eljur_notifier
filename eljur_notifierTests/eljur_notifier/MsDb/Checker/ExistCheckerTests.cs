using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using eljur_notifier.MsDbNS.FillerNS;
using eljur_notifier.MsDbNS.CleanerNS;

namespace eljur_notifier.MsDbNS.CheckerNS.Tests
{
    [TestClass()]
    public class ExistCheckerTests
    {
        [TestMethod()]
        public void IsTableExistTest()
        {
            Boolean IsTableExist = false;
            ExistChecker existChecker = new ExistChecker("StaffContextTests");
           
            MsDbFiller msDbFiller = new MsDbFiller("name=StaffContextTests");
            var AllStaff = getStaffListTest();
            msDbFiller.FillMsDb(AllStaff);
            IsTableExist = existChecker.IsTableExist("Pupils");
            Assert.IsTrue(IsTableExist == true);
            IsTableExist = existChecker.IsTableExist("Events");
            Assert.IsTrue(IsTableExist == true);
            IsTableExist = existChecker.IsTableExist("Schedules");
            Assert.IsTrue(IsTableExist == true);

            MsDbCleaner msDbCleaner = new MsDbCleaner("name=StaffContextTests");
            msDbCleaner.clearTableDb("Pupils");
            msDbCleaner.clearTableDb("Schedules");
            msDbCleaner.clearTableDb("Events");
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
            AllStaff.Add(student1);
            object[] student2 = new object[23];
            student2[0] = 5001;
            student2[1] = "Петров";
            student2[2] = "Петр";
            student2[3] = "Петрович";
            student2[22] = "Петров Петр Петрович";
            AllStaff.Add(student2);
            object[] student3 = new object[23];
            student3[0] = 5002;
            student3[1] = "Сидоров";
            student3[2] = "Сидор";
            student3[3] = "Сидорович";
            student3[22] = "Сидоров Сидор Сидорович";
            AllStaff.Add(student3);
            return AllStaff;
        }
    }
}