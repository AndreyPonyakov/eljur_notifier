using Microsoft.VisualStudio.TestTools.UnitTesting;
using eljur_notifier.MsDbNS.CheckerNS;
using System.Collections.Generic;
using System;

namespace eljur_notifier.MsDbNS.FillerNS.Tests
{
    [TestClass()]
    public class ScheduleFillerTests
    {
        [TestMethod()]
        public void FillSchedulesDbTest()
        {
            ScheduleFiller scheduleFiller = new ScheduleFiller("name=StaffContextTests");
            var AllClasses = getClassesListTest();
            scheduleFiller.FillSchedulesDb(AllClasses);

            EmptyChecker emptyChecker = new EmptyChecker("StaffContextTests");
            Assert.IsTrue(false == emptyChecker.IsTableEmpty("Schedules"));
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