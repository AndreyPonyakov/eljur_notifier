using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using MsDbLibraryNS.MsDbNS.RequesterNS;

namespace MsDbLibraryNS.MsDbNS.UpdaterNS.StaffUpdaterNS.Tests
{
    [TestClass()]
    public class NewStaffAdderTests
    {
        [TestMethod()]
        public void AddNewPupilTest()
        {
            NewStaffAdder newStaffAdder = new NewStaffAdder("name=StaffContextTests");
            var AllStaff = getStaffListTest();
            newStaffAdder.AddNewPupil(AllStaff);
            MsDbRequester msDbRequester = new MsDbRequester("name=StaffContextTests");
            String FullFIO = msDbRequester.getFullFIOByPupilIdOld(5004);
            Assert.IsTrue(FullFIO == "Новиков Новый Новович");
        }


        public List<object[]> getStaffListTest()
        {
            var AllStaff = new List<object[]>();
            object[] student1 = new object[23];
            student1[0] = 5004;
            student1[1] = "Новиков";
            student1[2] = "Новый";
            student1[3] = "Новович";
            student1[22] = "Новиков Новый Новович";
            student1[21] = "1А";
            student1[20] = 666;
            AllStaff.Add(student1);
            return AllStaff;
        }

    }
}

