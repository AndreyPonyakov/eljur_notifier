using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.Entity.SqlServer;

namespace eljur_notifier.MsDbNS.RequesterNS.Tests
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
            MsDbRequester msDbRequester = new MsDbRequester("name=StaffContextTests");
            int PupilIdOld = msDbRequester.getPupilIdOldByFullFio("Иванов Иван Иванович");
            TestContext.WriteLine(PupilIdOld.ToString());
            Assert.IsTrue(PupilIdOld == 5000);
        }

        [TestMethod()]
        public void getClasByPupilIdOldTest()
        {
            MsDbRequester msDbRequester = new MsDbRequester("name=StaffContextTests");
            String Clas = msDbRequester.getClasByPupilIdOld(5000);
            TestContext.WriteLine(Clas);
            Assert.IsTrue(Clas == "5Б");
        }

        [TestMethod()]
        public void getEndTimeLessonsByClasTest()
        {
            MsDbRequester msDbRequester = new MsDbRequester("name=StaffContextTests");
            TimeSpan EndTimeLessons = msDbRequester.getEndTimeLessonsByClas("5Б");
            TestContext.WriteLine(EndTimeLessons.ToString());
            Assert.IsTrue(EndTimeLessons == TimeSpan.Parse("12:37:00"));
        }

        [TestMethod()]
        public void getStartTimeLessonsByClasTest()
        {
            MsDbRequester msDbRequester = new MsDbRequester("name=StaffContextTests");
            TimeSpan StartTimeLessons = msDbRequester.getStartTimeLessonsByClas("3А");
            TestContext.WriteLine(StartTimeLessons.ToString());
            Assert.IsTrue(StartTimeLessons == TimeSpan.Parse("07:24:00"));
        }

        [TestMethod()]
        public void getEljurAccountIdByPupilIdOldTest()
        {
            MsDbRequester msDbRequester = new MsDbRequester("name=StaffContextTests");
            int EljurAccountId = msDbRequester.getEljurAccountIdByPupilIdOld(5000);
            TestContext.WriteLine(EljurAccountId.ToString());
            Assert.IsTrue(EljurAccountId == 14370);
        }

        [TestMethod()]
        public void getFullFIOByPupilIdOldTest()
        {
            MsDbRequester msDbRequester = new MsDbRequester("name=StaffContextTests");
            String FullFIO = msDbRequester.getFullFIOByPupilIdOld(5000);
            TestContext.WriteLine(FullFIO);
            Assert.IsTrue(FullFIO == "Иванов Иван Иванович");
        }

        [TestMethod()]
        public void getNotifyEnableByPupilIdOldTest()
        {
            MsDbRequester msDbRequester = new MsDbRequester("name=StaffContextTests");
            Boolean NotifyEnable = msDbRequester.getNotifyEnableByPupilIdOld(5103);
            TestContext.WriteLine(NotifyEnable.ToString());
            Assert.IsTrue(NotifyEnable == false);
        }
    }
}