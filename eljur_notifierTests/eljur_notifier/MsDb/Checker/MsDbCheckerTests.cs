using Microsoft.VisualStudio.TestTools.UnitTesting;
using eljur_notifier.MsDbNS.CheckerNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eljur_notifier.MsDbNS.CheckerNS.Tests
{
    [TestClass()]
    public class MsDbCheckerTests
    {
        [TestMethod()]
        public void CheckMsDbTest()
        {
            MsDbChecker msDbChecker = new MsDbChecker("StaffContextTests");
            Boolean IsOk = msDbChecker.CheckMsDb();
            Assert.IsTrue(IsOk == true);
        }
    }
}