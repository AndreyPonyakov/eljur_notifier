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
    public class NewStaffAdderTests
    {
        [TestMethod()]
        public void AddNewPupilTest()
        {
            NewStaffAdder newStaffAdder = new NewStaffAdder("name=StaffContextTests");
            var AllStaff = getStaffListTest();
            newStaffAdder.AddNewPupil(AllStaff);
            MsDbRequester msDbRequester = new MsDbRequester("name=StaffContextTests");
            String FullFIO = msDbRequester.getFullFIOByPupilIdOld(5000);
            Assert.IsTrue(FullFIO == "Иванов Иван Иванович");
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
            return AllStaff;
        }

    }
}