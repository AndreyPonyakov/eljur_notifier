using Microsoft.VisualStudio.TestTools.UnitTesting;
using eljur_notifier.MsDbNS.CheckerNS;
using System;
using eljur_notifier.StaffModel;
using eljur_notifier.MsDbNS.SetterNS;

namespace eljur_notifier.MsDbNS.CheckerNS.Tests
{
    [TestClass()]
    public class EmptyCheckerTests
    {
        [TestMethod()]
        public void IsTableEmptyTest()
        {
            PrepareTestEvent();
            EmptyChecker EmptyChecker = new EmptyChecker("StaffContextTests");
            Boolean IsTableEmpty = EmptyChecker.IsTableEmpty("Events");
            Assert.IsTrue(IsTableEmpty == false);
            MsDbSetter msDbSetter = new MsDbSetter("name=StaffContextTests");
            msDbSetter.SetDelAllEventsForTesting();
            IsTableEmpty = EmptyChecker.IsTableEmpty("Events");
            Assert.IsTrue(IsTableEmpty == true);
        }


        void PrepareTestEvent()
        {
            MsDbSetter msDbSetter = new MsDbSetter("name=StaffContextTests");
            Event TestEvent = new Event();
            TestEvent.PupilIdOld = 5000;
            TestEvent.EventTime = TimeSpan.FromMilliseconds(1000);
            TestEvent.NotifyWasSend = false;
            TestEvent.EventName = "Ушёл слишком рано";
            msDbSetter.SetDelAllEventsForTesting();
            msDbSetter.SetOneFullEventForTesting(TestEvent);
        }

    }
}