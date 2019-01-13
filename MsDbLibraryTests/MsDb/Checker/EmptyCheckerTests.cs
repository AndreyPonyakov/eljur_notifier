using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MsDbLibraryNS.StaffModel;
using MsDbLibraryNS.MsDbNS.SetterNS;


namespace MsDbLibraryNS.MsDbNS.CheckerNS.Tests
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