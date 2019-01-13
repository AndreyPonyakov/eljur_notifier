using System;

namespace MsDbLibraryNS.MsDbNS.CheckerNS
{
    public class MsDbChecker : MsDbBaseClass
    {
        public MsDbChecker(String NameOfConnectionString = "StaffContext")
            : base(new Message(), new ExistChecker(NameOfConnectionString)) { }
     
        public Boolean CheckMsDb()
        {
            if (existChecker.IsTableExist("Pupils") && existChecker.IsTableExist("Schedules"))
            {
                message.Display("msDb already exist", "Warn");
                return true;
            }
            else
            {
                message.Display("msDb NOT exist", "Warn");
                return false;
            }

        }

    }
}
