using Microsoft.VisualStudio.TestTools.UnitTesting;
using eljur_notifier.MsDbNS.RequesterNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            MsDbRequester msDbRequester = new MsDbRequester();
            int PupilIdOld = msDbRequester.getPupilIdOldByFullFio("Лапшина Ксения Михайловна");
            TestContext.WriteLine(PupilIdOld.ToString());
            Assert.IsTrue(PupilIdOld == 5103);
        }

        [TestMethod()]
        public void getClasByPupilIdOldTest()
        {
            MsDbRequester msDbRequester = new MsDbRequester();
            String Clas = msDbRequester.getClasByPupilIdOld(5103);
            TestContext.WriteLine(Clas);
            Assert.IsTrue(Clas == "3А");
        }

        [TestMethod()]
        public void getEndTimeLessonsByClasTest()
        {
            MsDbRequester msDbRequester = new MsDbRequester();
            TimeSpan EndTimeLessons = msDbRequester.getEndTimeLessonsByClas("3А");
            TestContext.WriteLine(EndTimeLessons.ToString());
            Assert.IsTrue(EndTimeLessons == TimeSpan.Parse("12:37:00"));
        }

        [TestMethod()]
        public void getStartTimeLessonsByClasTest()
        {
            MsDbRequester msDbRequester = new MsDbRequester();
            TimeSpan StartTimeLessons = msDbRequester.getStartTimeLessonsByClas("3А");
            TestContext.WriteLine(StartTimeLessons.ToString());
            Assert.IsTrue(StartTimeLessons == TimeSpan.Parse("07:24:00"));
        }

        [TestMethod()]
        public void getEljurAccountIdByPupilIdOldTest()
        {
            MsDbRequester msDbRequester = new MsDbRequester();
            int EljurAccountId = msDbRequester.getEljurAccountIdByPupilIdOld(5103);
            TestContext.WriteLine(EljurAccountId.ToString());
            Assert.IsTrue(EljurAccountId == 9415);
        }

        [TestMethod()]
        public void getFullFIOByPupilIdOldTest()
        {
            MsDbRequester msDbRequester = new MsDbRequester();
            String FullFIO = msDbRequester.getFullFIOByPupilIdOld(5103);
            TestContext.WriteLine(FullFIO);
            Assert.IsTrue(FullFIO == "Лапшина Ксения Михайловна");
        }

    }
}