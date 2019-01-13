using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;


namespace MsDbLibraryNS.MsDbNS.CheckerNS.Tests
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


