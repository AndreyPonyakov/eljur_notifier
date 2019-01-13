using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using eljur_notifier.MsDbNS.SetterNS;
using eljur_notifier.StaffModel;

namespace eljur_notifier.MsDbNS.CatcherNS.Tests
{
    [TestClass()]
    public class MsDbCatcherFirstPassTests
    {

        [TestMethod()]
        public void catchFirstPassTest()
        {
            PrepareTestEvent();
            MsDbCatcherFirstPass msDbCatcherFirstPass = new MsDbCatcherFirstPass("name=StaffContextTests");
            var PupilIdOldAndTimeRows = msDbCatcherFirstPass.catchFirstPass();
            foreach (var PupilIdOldAndTime in PupilIdOldAndTimeRows)
            {
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
            TestEvent1.EventName = "Первый проход";
            Event TestEvent2 = new Event();
            TestEvent2.PupilIdOld = 5001;
            TestEvent2.EventTime = TimeSpan.FromMilliseconds(1000);
            TestEvent2.NotifyWasSend = false;
            TestEvent2.EventName = "Не первый проход";
            msDbSetter.SetDelAllEventsForTesting();
            msDbSetter.SetOneFullEventForTesting(TestEvent1);
            msDbSetter.SetOneFullEventForTesting(TestEvent2);
        }


    }
}