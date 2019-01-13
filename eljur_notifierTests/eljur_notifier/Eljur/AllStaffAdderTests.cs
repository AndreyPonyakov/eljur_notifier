using Microsoft.VisualStudio.TestTools.UnitTesting;
using eljur_notifier.EljurNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eljur_notifier.EljurNS.Tests
{
    [TestClass()]
    public class AllStaffAdderTests
    {
        [TestMethod()]
        public void AddClassAndEljurIdTest()
        {
            AllStaffAdder allStaffAdder = new AllStaffAdder();
            var AllStaff = getStaffListTest();
            AllStaff = allStaffAdder.AddClassAndEljurId(AllStaff);
            foreach (object[] row in AllStaff)
            {
                Assert.IsTrue( false == String.IsNullOrEmpty(row[21].ToString()));
                Assert.IsTrue(Int32.TryParse(row[20].ToString(), out int number));
            }
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