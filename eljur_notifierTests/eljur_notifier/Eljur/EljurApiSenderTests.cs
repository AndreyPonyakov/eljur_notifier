using Microsoft.VisualStudio.TestTools.UnitTesting;
using eljur_notifier.EljurNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MsDbLibraryNS.MsDbNS.RequesterNS;
using MsDbLibraryNS.StaffModel;
using MsDbLibraryNS.MsDbNS.SetterNS;

namespace eljur_notifier.EljurNS.Tests
{
    [TestClass()]
    public class EljurApiSenderTests
    {
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

        void PrepareTestEvent()
        {
            MsDbSetter msDbSetter = new MsDbSetter("name=StaffContextTests");
            Event TestEvent = new Event();
            TestEvent.PupilIdOld = 5000;
            TestEvent.EventTime = DateTime.Now.TimeOfDay;
            TestEvent.NotifyWasSend = false;
            TestEvent.EventName = "Вышел";
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