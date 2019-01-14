using Microsoft.VisualStudio.TestTools.UnitTesting;
using eljur_notifier.MsDbNS;
using System;
using System.Collections.Generic;
using MsDbLibraryNS.StaffModel;
using MsDbLibraryNS.MsDbNS.SetterNS;
using MsDbLibraryNS.MsDbNS.RequesterNS;
using System.Linq;

namespace eljur_notifier.MsDbNS.Tests
{
    [TestClass()]
    public class MsDbTests
    {
        public TestContext TestContext { get; set; }

        [TestMethod()]
        public void CheckEventsDbTest()
        {
            PrepareTestEvent();
            var TestListEvents = new List<object[]>();
            TestListEvents = PrepareTestListEvents(1);
            MsDb msDb = new MsDb("name=StaffContextTests");
            msDb.CheckEventsDb(TestListEvents);

            MsDbRequester msDbRequester = new MsDbRequester("name=StaffContextTests");
            String EventName = msDbRequester.getEventNameByPupilIdOld(5000);
            Assert.IsTrue(EventName == "Первый проход");

            TestListEvents = PrepareTestListEvents(2);
            msDb.CheckEventsDb(TestListEvents);
            EventName = msDbRequester.getEventNameByPupilIdOld(5000);
            Assert.IsTrue(EventName == "Первый проход");

            TestListEvents = PrepareTestListEvents(3);
            msDb.CheckEventsDb(TestListEvents);
            EventName = msDbRequester.getEventNameByPupilIdOld(5000);
            Assert.IsTrue(EventName == "Вышел");

            TestListEvents = PrepareTestListEvents();
            msDb.CheckEventsDb(TestListEvents);
            EventName = msDbRequester.getEventNameByPupilIdOld(5000);
            Assert.IsTrue(EventName == "Вернулся");
        }

        [TestMethod()]
        public void CheckCurEventTest()
        {
            PrepareTestEvent();
            var TestListEvents = new List<object[]>();
            TestListEvents = PrepareTestListEvents(1);
            MsDb msDb = new MsDb("name=StaffContextTests");
            MsDbRequester msDbRequester = new MsDbRequester("name=StaffContextTests");
            var curEvent = msDbRequester.getEventdByPupilIdOld(5000);
            msDb.CheckCurEvent(curEvent, TestListEvents.First(), 5000);
            String EventName = msDbRequester.getEventNameByPupilIdOld(5000);
            Assert.IsTrue(EventName == "Первый проход");
        }

        [TestMethod()]
        public void RegisterOutputEventTest()
        {
            PrepareTestEvent();
            var TestListEvents = new List<object[]>();
            TestListEvents = PrepareTestListEvents(1);
            MsDb msDb = new MsDb("name=StaffContextTests");
            MsDbRequester msDbRequester = new MsDbRequester("name=StaffContextTests");
            var curEvent = msDbRequester.getEventdByPupilIdOld(5000);
            msDb.RegisterOutputEvent(curEvent, TestListEvents.First(), 5000);
            String EventName = msDbRequester.getEventNameByPupilIdOld(5000);
            Assert.IsTrue(EventName == "Вышел");
        }

        [TestMethod()]
        public void RegisterInputEventTest()
        {
            PrepareTestEvent();
            var TestListEvents = new List<object[]>();
            TestListEvents = PrepareTestListEvents(1);
            MsDb msDb = new MsDb("name=StaffContextTests");
            MsDbRequester msDbRequester = new MsDbRequester("name=StaffContextTests");
            var curEvent = msDbRequester.getEventdByPupilIdOld(5000);
            msDb.RegisterInputEvent(curEvent, TestListEvents.First(), 5000);
            String EventName = msDbRequester.getEventNameByPupilIdOld(5000);
            Assert.IsTrue(EventName == "Вернулся");
        }

        [TestMethod()]
        public void AddNewEventTest()
        {
            PrepareTestEvent();
            MsDbSetter msDbSetter = new MsDbSetter("name=StaffContextTests");
            msDbSetter.SetDelAllEventsForTesting();
            var TestListEvents = new List<object[]>();
            TestListEvents = PrepareTestListEvents(1);
            MsDb msDb = new MsDb("name=StaffContextTests");
            MsDbRequester msDbRequester = new MsDbRequester("name=StaffContextTests");
            msDb.AddNewEvent(TestListEvents.First(), 5000);
            String EventName = msDbRequester.getEventNameByPupilIdOld(5000);
            Assert.IsTrue(EventName == "Первый проход");

            msDbSetter.SetDelAllEventsForTesting();
            TestListEvents = PrepareTestListEvents(3);
            msDb.AddNewEvent(TestListEvents.First(), 5000);
            EventName = msDbRequester.getEventNameByPupilIdOld(5000);
            TestContext.WriteLine(EventName);
            Assert.IsTrue(EventName == null);
        }

        [TestMethod()]
        public void IsInputPassTest()
        {
            MsDb msDb = new MsDb("name=StaffContextTests");
            Boolean pass = msDb.IsInputPass(8677);
            Assert.IsTrue(pass);
            pass = msDb.IsInputPass(9256);
            Assert.IsTrue(pass);
            pass = msDb.IsInputPass(8564);
            Assert.IsFalse(pass);
        }

        [TestMethod()]
        public void IsOutputPassTest()
        {
            MsDb msDb = new MsDb("name=StaffContextTests");
            Boolean pass = msDb.IsOutputPass(8564);
            Assert.IsTrue(pass);
            pass = msDb.IsOutputPass(9369);
            Assert.IsTrue(pass);
            pass = msDb.IsOutputPass(8677);
            Assert.IsFalse(pass);
        }

        [TestMethod()]
        public void IsInputEventNameTest()
        {
            MsDb msDb = new MsDb("name=StaffContextTests");
            Boolean res = msDb.IsInputEventName("Первый проход");
            Assert.IsTrue(res);
            res = msDb.IsInputEventName("Вернулся");
            Assert.IsTrue(res);
            res = msDb.IsInputEventName("Опоздал");
            Assert.IsTrue(res);
            res = msDb.IsInputEventName("ПРОСТО СТРОКА");
            Assert.IsFalse(res);
        }

        [TestMethod()]
        public void IsOutPutEventNameTest()
        {
            MsDb msDb = new MsDb("name=StaffContextTests");
            Boolean res = msDb.IsOutPutEventName("Вышел");
            Assert.IsTrue(res);
            res = msDb.IsOutPutEventName("Прогул");
            Assert.IsTrue(res);
            res = msDb.IsOutPutEventName("ПРОСТО СТРОКА");
            Assert.IsFalse(res);
        }


        void PrepareTestSchedule()
        {
            MsDbSetter msDbSetter = new MsDbSetter("name=StaffContextTests");
            Schedule TestSchedule = new Schedule();
            TestSchedule.Clas = "1Б";
            TestSchedule.StartTimeLessons = DateTime.Now.TimeOfDay;
            TestSchedule.EndTimeLessons = DateTime.Now.TimeOfDay;

            msDbSetter.SetDelAllSchedulesForTesting();
            msDbSetter.SetTestScheduleForTesting(TestSchedule);

        }

        void PrepareTestEvent()
        {
            MsDbSetter msDbSetter = new MsDbSetter("name=StaffContextTests");
            Event TestEvent = new Event();
            TestEvent.PupilIdOld = 5000;
            TestEvent.EventTime = DateTime.Now.TimeOfDay;
            TestEvent.NotifyWasSend = false;
            TestEvent.EventName = "Первый проход";
            msDbSetter.SetDelAllEventsForTesting();
            msDbSetter.SetOneFullEventForTesting(TestEvent);

        }

        List<object[]> PrepareTestListEvents(int number=0)
        {
            var TestListEvents = new List<object[]>();
            var ObjectEvent1 = new object[5];
            ObjectEvent1[0] = DateTime.Now.TimeOfDay;
            ObjectEvent1[1] = 5000;
            ObjectEvent1[2] = DateTime.Now.ToShortDateString();
            ObjectEvent1[3] = 8677; //input1
            ObjectEvent1[4] = 8498; //inputCommonValue
            var ObjectEvent2 = new object[5];
            ObjectEvent2[0] = DateTime.Now.TimeOfDay;
            ObjectEvent2[1] = DBNull.Value;
            ObjectEvent2[2] = DateTime.Now.ToShortDateString();
            ObjectEvent2[3] = 8564; //output1
            ObjectEvent2[4] = 8498; //outputCommonValue
            var ObjectEvent3 = new object[5];
            ObjectEvent3[0] = DateTime.Now.TimeOfDay;
            ObjectEvent3[1] = 5000;
            ObjectEvent3[2] = DateTime.Now.ToShortDateString();
            ObjectEvent3[3] = 8564; //output1
            ObjectEvent3[4] = 8498; //outputCommonValue

            if (number == 0)
            {
                TestListEvents.Add(ObjectEvent1);
                TestListEvents.Add(ObjectEvent2);               
            }
            else if (number == 1)
            {
                TestListEvents.Add(ObjectEvent1);
            }
            else if (number == 2)
            {
                TestListEvents.Add(ObjectEvent2);
            }
            else if (number == 3)
            {
                TestListEvents.Add(ObjectEvent3);
            }
            return TestListEvents;
        }

        void PrepareTestPupil()
        {
            MsDbSetter msDbSetter = new MsDbSetter("name=StaffContextTests");
            Pupil TestPupil = new Pupil();
            TestPupil.PupilIdOld = 5000;
            TestPupil.FirstName = "Тест";
            TestPupil.LastName = "Тестов";
            TestPupil.MiddleName = "Тестович";
            TestPupil.FullFIO = "Тестов Тест Тестович";
            TestPupil.Clas = "1Б";
            TestPupil.EljurAccountId = 666;
            TestPupil.NotifyEnable = true;
            msDbSetter.SetDelAllPupilsForTesting();
            msDbSetter.SetOneTestPupilForTesting(TestPupil);
        }



    }
}