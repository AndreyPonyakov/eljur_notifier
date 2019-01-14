using eljur_notifier.EljurNS;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MsDbLibraryNS.StaffModel;
using MsDbLibraryNS.MsDbNS.SetterNS;
using MsDbLibraryNS.MsDbNS.RequesterNS;


namespace eljur_notifier.EljurNS.Tests
{
    [TestClass()]
    public class EljurApiSenderTests
    {
        [TestMethod()]
        public void SendNotifyFirstPassTest()
        {
            PrepareTestPupil();
            PrepareTestEvent();
            PrepareTestSchedule();
            var TestObject1 = PrepareTestObject5MinPast();
            var TestObject2 = PrepareTestObject5MinFuture();
            MsDbRequester msDbRequester = new MsDbRequester("name=StaffContextTests");
            MsDbSetter msDbSetter = new MsDbSetter("name=StaffContextTests");
            EljurApiSender EljurApiSender = new EljurApiSender("name=StaffContextTests");

            var result = EljurApiSender.SendNotifyFirstPass(TestObject1);
            Assert.IsTrue(result);
            String EventName = msDbRequester.getEventNameByPupilIdOld(5000);      
            Assert.IsTrue(EventName == "Первый проход");
       
            result = EljurApiSender.SendNotifyFirstPass(TestObject2);
            Assert.IsTrue(result);
      
            EventName = msDbRequester.getEventNameByPupilIdOld(5000);
            msDbSetter.SetDelAllEventsForTesting();
            Assert.IsTrue(EventName == "Опоздал");          
        }


        [TestMethod()]
        public void SendNotifyLastPassTest()
        {
            PrepareTestPupil();
            PrepareTestEvent();
            var TestObject1 = PrepareTestObject16MinPast();
            var TestObject2 = PrepareTestObject5MinPast();
            EljurApiSender EljurApiSender = new EljurApiSender("name=StaffContextTests");
            var result = EljurApiSender.SendNotifyLastPass(TestObject1);
            Assert.IsTrue(result);
            result = EljurApiSender.SendNotifyLastPass(TestObject2);
            Assert.IsFalse(result);
            PrepareChangedTestPupil();
            result = EljurApiSender.SendNotifyLastPass(TestObject1);
            //Assert.IsFalse(result);
            //In this case need always return true!!!
            Assert.IsTrue(result);
        }

        object[] PrepareTestObject16MinPast()
        {
            var TestObject = new object[2];
            TestObject[0] = 5000;
            TimeSpan TestTime = DateTime.Now.TimeOfDay;
            TestTime = TestTime.Add(new TimeSpan(0, -16, 0));
            TestObject[1] = TestTime;
            return TestObject;
        }

        object[] PrepareTestObject5MinPast()
        {
            var TestObject = new object[2];
            TestObject[0] = 5000;
            TimeSpan TestTime = DateTime.Now.TimeOfDay;
            TestTime = TestTime.Add(new TimeSpan(0, -5, 0));
            TestObject[1] = TestTime;
            return TestObject;
        }

        object[] PrepareTestObject5MinFuture()
        {
            var TestObject = new object[2];
            TestObject[0] = 5000;
            TimeSpan TestTime = DateTime.Now.TimeOfDay;
            TestTime = TestTime.Add(new TimeSpan(0, 5, 0));
            TestObject[1] = TestTime;
            return TestObject;
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

        void PrepareChangedTestPupil()
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
            TestPupil.NotifyEnable = false;
            msDbSetter.SetDelAllPupilsForTesting();
            msDbSetter.SetOneTestPupilForTesting(TestPupil);
        }


    }
}