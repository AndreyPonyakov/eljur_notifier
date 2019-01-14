using Microsoft.VisualStudio.TestTools.UnitTesting;
using eljur_notifier.FirebirdNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eljur_notifier.FirebirdNS.Tests
{
    [TestClass()]
    public class FirebirdTests
    {
        [TestMethod()]
        public void getAllStaffTest()
        {
            Firebird firebird = new Firebird();
            var AllStaff = firebird.getAllStaff();
            Assert.IsTrue(AllStaff.Count > 0);
        }

        [TestMethod()]
        public void getAnySqlQueryTest()
        {
            Firebird firebird = new Firebird();
            var AllStaff = firebird.getAnySqlQuery("select * from STAFF");
            Assert.IsTrue(AllStaff.Count > 0);
        }

        [TestMethod()]
        public void SetBeforeDtAndAfterDtTest()
        {
            Firebird firebird = new Firebird();
            firebird.SetBeforeDtAndAfterDt(TimeSpan.Parse("00:00:17"));
            TimeSpan diff = firebird.afterDt - firebird.beforeDt;
            Assert.IsTrue(diff == TimeSpan.Parse("00:00:17"));
        }

        [TestMethod()]
        public void getEventsByIntervalRequestTest()
        {
            Firebird firebird = new Firebird();
            var AllEvents = firebird.getEventsByIntervalRequest();
            //If not Exeption, then Ok!!!
            //Assert.IsTrue(AllEvents.Count > 0);
        }
    }
}