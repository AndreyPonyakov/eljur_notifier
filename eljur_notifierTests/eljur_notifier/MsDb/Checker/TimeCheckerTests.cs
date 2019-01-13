using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using eljur_notifier.MsDbNS.CleanerNS;
using eljur_notifier.MsDbNS.UpdaterNS;
using eljur_notifier.MsDbNS.RequesterNS;

namespace eljur_notifier.MsDbNS.CheckerNS.Tests
{
    [TestClass()]
    public class TimeCheckerTests
    {
        [TestMethod()]
        public void TimeCheckerTest()
        {
            TimeChecker timeChecker = new TimeChecker();
            Assert.IsTrue(timeChecker.timeFromDel == timeChecker.config.timeFromDel);
            Assert.IsTrue(timeChecker.timeToDel == timeChecker.config.timeToDel);

            TimeSpan now = DateTime.Now.TimeOfDay;
            TimeSpan nowPlusMinute = now.Add(new TimeSpan(0, 1, 0));
            TimeChecker timeCheckerWithParam = new TimeChecker(now, nowPlusMinute);
            Assert.IsTrue(timeCheckerWithParam.timeFromDel == now);
            Assert.IsTrue(timeCheckerWithParam.timeToDel == nowPlusMinute);

        }

        [TestMethod()]
        public void CheckTimeTest()
        {
            TimeSpan now = DateTime.Now.TimeOfDay;
            TimeSpan nowPlusMinute = now.Add(new TimeSpan(0, 1, 0));
            TimeChecker timeCheckerWithParam = new TimeChecker(now, nowPlusMinute);
            timeCheckerWithParam.CheckTime(new Action(delegate {

                MsDbCleaner msDbCleaner = new MsDbCleaner("name=StaffContextTests");
                MsDbUpdater msDbUpdater = new MsDbUpdater("name=StaffContextTests");
                msDbCleaner.clearAllTablesBesidesPupils();

                var AllStaff = new List<object[]>();
                AllStaff = getStaffListTest();
                msDbUpdater.UpdateMsDb(AllStaff);
    
            }));

            MsDbRequester msDbRequester = new MsDbRequester("name=StaffContextTests");
            String FullFIO1 = msDbRequester.getFullFIOByPupilIdOld(5000);
            String FullFIO2 = msDbRequester.getFullFIOByPupilIdOld(5001);
            String FullFIO3 = msDbRequester.getFullFIOByPupilIdOld(5002);
            Assert.IsTrue(FullFIO1 == "Обновляев Обновляй Обновляевич");
            Assert.IsTrue(FullFIO2 == "Петров Петр Петрович");
            Assert.IsTrue(FullFIO3 == "Сидоров Сидор Сидорович");
        }

        public List<object[]> getStaffListTest()
        {
            var AllStaff = new List<object[]>();
            object[] student1 = new object[23];
            student1[0] = 5000;
            student1[1] = "Обновляев";
            student1[2] = "Обновляй";
            student1[3] = "Обновляевич";
            student1[22] = "Обновляев Обновляй Обновляевич";
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