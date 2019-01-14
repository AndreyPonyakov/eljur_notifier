using MsDbLibraryNS.MsDbNS.RequesterNS;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.Entity.SqlServer;
using MsDbLibraryNS.StaffModel;
using MsDbLibraryNS.MsDbNS.SetterNS;

namespace MsDbLibraryNS.MsDbNS.RequesterNS.Tests
{
    [TestClass()]
    public class MsDbRequesterTests
    {
        public TestContext TestContext { get; set; }

        public void FixEfProviderServicesProblem()
        {
            //The Entity Framework provider type 'System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer'
            //for the 'System.Data.SqlClient' ADO.NET provider could not be loaded. 
            //Make sure the provider assembly is available to the running application. 
            //See http://go.microsoft.com/fwlink/?LinkId=260882 for more information.

            var instance = SqlProviderServices.Instance;
        }

        [TestMethod()]
        public void getPupilIdOldByFullFioTest()
        {
            PrepareTestPupil();
            MsDbRequester msDbRequester = new MsDbRequester("name=StaffContextTests");
            int PupilIdOld = msDbRequester.getPupilIdOldByFullFio("Тестов Тест Тестович");
            TestContext.WriteLine(PupilIdOld.ToString());
            Assert.IsTrue(PupilIdOld == 5005);
        }

        [TestMethod()]
        public void getClasByPupilIdOldTest()
        {
            PrepareTestPupil();
            MsDbRequester msDbRequester = new MsDbRequester("name=StaffContextTests");
            String Clas = msDbRequester.getClasByPupilIdOld(5005);
            TestContext.WriteLine(Clas);
            Assert.IsTrue(Clas == "1Б");
        }

        [TestMethod()]
        public void getEndTimeLessonsByClasTest()
        {
            PrepareTestSchedule();
            MsDbRequester msDbRequester = new MsDbRequester("name=StaffContextTests");
            TimeSpan EndTimeLessons = msDbRequester.getEndTimeLessonsByClas("1Б");
            TestContext.WriteLine(EndTimeLessons.ToString());
            Assert.IsTrue(EndTimeLessons == TimeSpan.Parse("00:00:20"));
        }

        [TestMethod()]
        public void getStartTimeLessonsByClasTest()
        {
            PrepareTestSchedule();
            MsDbRequester msDbRequester = new MsDbRequester("name=StaffContextTests");
            TimeSpan StartTimeLessons = msDbRequester.getStartTimeLessonsByClas("1Б");
            TestContext.WriteLine(StartTimeLessons.ToString());
            Assert.IsTrue(StartTimeLessons == TimeSpan.Parse("00:00:10"));
        }

        [TestMethod()]
        public void getEljurAccountIdByPupilIdOldTest()
        {
            PrepareTestPupil();
            MsDbRequester msDbRequester = new MsDbRequester("name=StaffContextTests");
            int EljurAccountId = msDbRequester.getEljurAccountIdByPupilIdOld(5005);
            TestContext.WriteLine(EljurAccountId.ToString());
            Assert.IsTrue(EljurAccountId == 666);
        }

        [TestMethod()]
        public void getFullFIOByPupilIdOldTest()
        {
            PrepareTestPupil();
            MsDbRequester msDbRequester = new MsDbRequester("name=StaffContextTests");
            String FullFIO = msDbRequester.getFullFIOByPupilIdOld(5005);
            TestContext.WriteLine(FullFIO);
            Assert.IsTrue(FullFIO == "Тестов Тест Тестович");
        }

        [TestMethod()]
        public void getNotifyEnableByPupilIdOldTest()
        {
            PrepareTestPupil();
            MsDbRequester msDbRequester = new MsDbRequester("name=StaffContextTests");
            Boolean NotifyEnable = msDbRequester.getNotifyEnableByPupilIdOld(5103);
            TestContext.WriteLine(NotifyEnable.ToString());
            Assert.IsTrue(NotifyEnable == false);
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

        void PrepareTestPupil()
        {
            MsDbSetter msDbSetter = new MsDbSetter("name=StaffContextTests");
            Pupil TestPupil = new Pupil();
            TestPupil.PupilIdOld = 5005;
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

        void PrepareTestSchedule()
        {
            MsDbSetter msDbSetter = new MsDbSetter("name=StaffContextTests");
            Schedule TestSchedule = new Schedule();
            TestSchedule.Clas = "1Б";
            TestSchedule.StartTimeLessons = TimeSpan.FromMilliseconds(10000);
            TestSchedule.EndTimeLessons = TimeSpan.FromMilliseconds(20000);
            msDbSetter.SetDelAllSchedulesForTesting();
            msDbSetter.SetTestScheduleForTesting(TestSchedule);
        }

        [TestMethod()]
        public void getEventdByPupilIdOldTest()
        {
            PrepareTestEvent();
            MsDbRequester msDbRequester = new MsDbRequester("name=StaffContextTests");
            Event retEvent = msDbRequester.getEventdByPupilIdOld(5000);
            Assert.IsTrue(retEvent.EventName == "Ушёл слишком рано");
        }
    }
}

