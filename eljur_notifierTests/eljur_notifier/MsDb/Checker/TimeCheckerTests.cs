using Microsoft.VisualStudio.TestTools.UnitTesting;
using eljur_notifier.MsDbNS.CheckerNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eljur_notifier.MsDbNS.CleanerNS;
using eljur_notifier.EventHandlerNS;

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
                msDbCleaner.clearAllTablesBesidesPupils();

                //var AllStaff = new List<object[]>();
                //AllStaff = firebird.getAllStaff();
                //msDbUpdater.UpdateMsDb(AllStaff);
                ////restart 
                //AppRunner appRunner = new AppRunner();
                //appRunner.Run(args);
            }));
            Assert.Fail();
        }
    }
}