using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MsDbLibraryNS.MsDbNS.SetterNS;
using MsDbLibraryNS.StaffModel;

namespace MsDbLibraryNS.MsDbNS.CatcherNS.Tests
{
    [TestClass()]
    public class MsDbCatcherLastPassTests
    {
        [TestMethod()]
        public void catchLastPassTest() //This Test works only after 00:15:10
        {
            PrepareTestEvent();
            MsDbCatcherLastPass msDbCatcherLastPass = new MsDbCatcherLastPass("name=StaffContextTests");
            var PupilIdOldAndTimeRows = msDbCatcherLastPass.catchLastPass();
            foreach (var PupilIdOldAndTime in PupilIdOldAndTimeRows)
            {
                Assert.IsTrue(DateTime.Now.TimeOfDay > TimeSpan.Parse("00:15:11"));
                Assert.IsTrue(Convert.ToInt32(PupilIdOldAndTime[0]) == 5000);
                Assert.IsTrue(PupilIdOldAndTime[1].ToString() == "00:00:10");
            }
        }


        void PrepareTestEvent()
        {
            MsDbSetter msDbSetter = new MsDbSetter("name=StaffContextTests");
            Event TestEvent1 = new Event();
            TestEvent1.PupilIdOld = 5000;
            TestEvent1.EventTime = TimeSpan.FromMilliseconds(10000);
            TestEvent1.NotifyWasSend = false;
            TestEvent1.EventName = "Прогул";
            Event TestEvent2 = new Event();
            TestEvent2.PupilIdOld = 5001;
            TestEvent2.EventTime = TimeSpan.FromMilliseconds(1000);
            TestEvent2.NotifyWasSend = false;
            TestEvent2.EventName = "Первый проход";
            msDbSetter.SetDelAllEventsForTesting();
            msDbSetter.SetOneFullEventForTesting(TestEvent1);
            msDbSetter.SetOneFullEventForTesting(TestEvent2);
        }
    }
}