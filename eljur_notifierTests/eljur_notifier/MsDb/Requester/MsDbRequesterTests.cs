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

        [TestMethod()]
        public void MsDbRequesterTest()
        {
            Assert.Fail();
        }

        public void FixEfProviderServicesProblem()
        {
            //The Entity Framework provider type 'System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer'
            //for the 'System.Data.SqlClient' ADO.NET provider could not be loaded. 
            //Make sure the provider assembly is available to the running application. 
            //See http://go.microsoft.com/fwlink/?LinkId=260882 for more information.

            var instance = SqlProviderServices.Instance;
        }


        [TestMethod()]
        public void RandomStringTest()
        {
            Assert.Fail();
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
            Assert.Fail();
        }

        [TestMethod()]
        public void getEndTimeLessonsByClasTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void getStartTimeLessonsByClasTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void getEljurAccountIdByPupilIdOldTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void getFullFIOByPupilIdOldTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void getModifyDateTest()
        {
            Assert.Fail();
        }
    }
}