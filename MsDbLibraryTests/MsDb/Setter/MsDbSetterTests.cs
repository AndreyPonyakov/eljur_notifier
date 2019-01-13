using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.Entity.SqlServer;
using MsDbLibraryNS.MsDbNS.RequesterNS;
using MsDbLibraryNS.StaffModel;

namespace MsDbLibraryNS.MsDbNS.SetterNS.Tests
{
    [TestClass()]
    public class MsDbSetterTests
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
        public void SetClasByPupilIdOldTest()
        {
            PrepareTestPupil();
            MsDbSetter msDbSetter = new MsDbSetter("name=StaffContextTests");
            int PupilIdOld = 5005;
            String Clas = "1А";
            msDbSetter.SetClasByPupilIdOld(PupilIdOld, Clas);

            MsDbRequester msDbRequester = new MsDbRequester("name=StaffContextTests");
            String ReqClas = msDbRequester.getClasByPupilIdOld(5005);
            msDbSetter.SetDelAllPupilsForTesting();

            Assert.IsTrue(Clas == ReqClas);

        }

        [TestMethod()]
        public void SetStatusWentHomeTooEarlyTest()
        {
            PrepareTestEvent();
            MsDbSetter msDbSetter = new MsDbSetter("name=StaffContextTests"); ;
            msDbSetter.SetStatusWentHomeTooEarly(5000);

            MsDbRequester msDbRequester = new MsDbRequester("name=StaffContextTests");
            String EventName = msDbRequester.getEventNameByPupilIdOld(5000);
            msDbSetter.SetDelAllEventsForTesting();
            Assert.IsTrue(EventName == "Прогул");
        }

        [TestMethod()]
        public void SetStatusCameTooLateTest()
        {
            PrepareTestEvent();
            MsDbSetter msDbSetter = new MsDbSetter("name=StaffContextTests"); ;
            msDbSetter.SetStatusCameTooLate(5000);

            MsDbRequester msDbRequester = new MsDbRequester("name=StaffContextTests");
            String EventName = msDbRequester.getEventNameByPupilIdOld(5000);
            msDbSetter.SetDelAllEventsForTesting();
            Assert.IsTrue(EventName == "Опоздал");
        }

        [TestMethod()]
        public void SetStatusNotifyWasSendTest()
        {
            PrepareTestEvent();
            MsDbSetter msDbSetter = new MsDbSetter("name=StaffContextTests"); ;
            msDbSetter.SetStatusNotifyWasSend(5000);

            MsDbRequester msDbRequester = new MsDbRequester("name=StaffContextTests");
            Boolean NotifyWasSend = msDbRequester.getStatusNotifyWasSendByPupilIdOld(5000);
            msDbSetter.SetDelAllEventsForTesting();
            Assert.IsTrue(NotifyWasSend == true);
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
            msDbSetter.SetDelAllPupilsForTesting();
            msDbSetter.SetOneTestPupilForTesting(TestPupil);
        }

    }
}

