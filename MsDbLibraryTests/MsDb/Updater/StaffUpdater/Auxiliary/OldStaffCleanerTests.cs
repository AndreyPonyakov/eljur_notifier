using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using MsDbLibraryNS.MsDbNS.CheckerNS;

namespace MsDbLibraryNS.MsDbNS.UpdaterNS.StaffUpdaterNS.Tests
{
    [TestClass()]
    public class OldStaffCleanerTests
    {
        [TestMethod()]
        public void CleanOldStaffTest()
        {
            OldStaffCleaner oldStaffCleaner = new OldStaffCleaner("name=StaffContextTests");
            var AllStaff = getStaffListTest();
            oldStaffCleaner.CleanOldStaff(AllStaff);
            EmptyChecker emptyChecker = new EmptyChecker("StaffContextTests");
            Assert.IsTrue(true == emptyChecker.IsTableEmpty("Pupils"));

        }


        public List<object[]> getStaffListTest()
        {
            var AllStaff = new List<object[]>();
            object[] student1 = new object[23];
            student1[0] = 4999;
            student1[1] = "Удаляев";
            student1[2] = "Удаляй";
            student1[3] = "Удаляевич";
            student1[22] = "Удаляев Удаляй Удаляевич";
            AllStaff.Add(student1);
            return AllStaff;
        }

    }
}
