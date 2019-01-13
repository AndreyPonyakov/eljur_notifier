using Microsoft.VisualStudio.TestTools.UnitTesting;
using MsDbLibraryNS.MsDbNS.UpdaterNS.StaffUpdaterNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MsDbLibraryNS.MsDbNS.RequesterNS;
using MsDbLibraryNS.MsDbNS.CleanerNS;
using MsDbLibraryNS.MsDbNS.FillerNS;

namespace MsDbLibraryNS.MsDbNS.UpdaterNS.StaffUpdaterNS.Tests
{
    [TestClass()]
    public class MsDbStaffUpdaterTests
    {
        [TestMethod()]
        public void UpdateStaffTest()
        {
            PrepareTest();
            MsDbStaffUpdater msDbStaffUpdater = new MsDbStaffUpdater("name=StaffContextTests");
            var AllStaff = getStaffListTestUpdated();
            msDbStaffUpdater.UpdateStaff(AllStaff);
            MsDbRequester msDbRequester = new MsDbRequester("name=StaffContextTests");
            String FullFIO1 = msDbRequester.getFullFIOByPupilIdOld(5000);
            String FullFIO2 = msDbRequester.getFullFIOByPupilIdOld(5001);
            String FullFIO3 = msDbRequester.getFullFIOByPupilIdOld(5002);
            Assert.IsTrue(FullFIO1 == "Иванова Ивана Ивановна");
            Assert.IsTrue(FullFIO2 == "Петрова Петра Петровна");
            Assert.IsTrue(FullFIO3 == "Сидорова Сидора Сидоровна");
        }

        public List<object[]> getStaffListTestUpdated()
        {
            var AllStaff = new List<object[]>();
            object[] student1 = new object[23];
            student1[0] = 5000;
            student1[1] = "Иванова";
            student1[2] = "Ивана";
            student1[3] = "Ивановна";
            student1[22] = "Иванова Ивана Ивановна";
            student1[21] = "1А";
            student1[20] = 666;
            AllStaff.Add(student1);
            object[] student2 = new object[23];
            student2[0] = 5001;
            student2[1] = "Петрова";
            student2[2] = "Петра";
            student2[3] = "Петровна";
            student2[22] = "Петрова Петра Петровна";
            student2[21] = "1А";
            student2[20] = 666;
            AllStaff.Add(student2);
            object[] student3 = new object[23];
            student3[0] = 5002;
            student3[1] = "Сидорова";
            student3[2] = "Сидора";
            student3[3] = "Сидоровна";
            student3[22] = "Сидорова Сидора Сидоровна";
            student3[21] = "1А";
            student3[20] = 666;
            AllStaff.Add(student3);
            return AllStaff;
        }

        public List<object[]> getStaffListTestOld()
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

        void PrepareTest()
        {
            MsDbCleaner msDbCleaner = new MsDbCleaner("name=StaffContextTests");
            msDbCleaner.clearTableDb("Pupils");
            StaffFiller staffFiller = new StaffFiller("name=StaffContextTests");
            var AllStaff = getStaffListTestOld();
            staffFiller.FillStaffDb(AllStaff);
        }

    }
}

