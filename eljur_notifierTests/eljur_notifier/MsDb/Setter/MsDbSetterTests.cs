using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.Entity.SqlServer;

namespace eljur_notifier.MsDbNS.SetterNS.Tests
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
            MsDbSetter msDbSetter = new MsDbSetter("name=StaffContextTests");
            int PupilIdOld = 5000;
            String Clas = "1Б";
            msDbSetter.SetClasByPupilIdOld(PupilIdOld, Clas);

            TestContext.WriteLine(PupilIdOld.ToString());

            Assert.Fail();
        }

        [TestMethod()]
        public void SetStatusWentHomeTooEarlyTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SetStatusCameTooLateTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SetStatusNotifyWasSendTest()
        {
            Assert.Fail();
        }
    }
}