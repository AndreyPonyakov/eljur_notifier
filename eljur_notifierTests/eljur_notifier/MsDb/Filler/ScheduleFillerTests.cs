using Microsoft.VisualStudio.TestTools.UnitTesting;
using eljur_notifier.MsDbNS.CheckerNS;

namespace eljur_notifier.MsDbNS.FillerNS.Tests
{
    [TestClass()]
    public class ScheduleFillerTests
    {


        [TestMethod()]
        public void FillSchedulesDbTest()
        {
            ScheduleFiller scheduleFiller = new ScheduleFiller("name=StaffContextTests");
            scheduleFiller.FillSchedulesDb();

            EmptyChecker emptyChecker = new EmptyChecker("StaffContextTests");
            Assert.IsTrue(false == emptyChecker.IsTableEmpty("Schedules"));
        }
    }
}