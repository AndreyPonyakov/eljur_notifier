using eljur_notifier.AppCommonNS;
using System;

namespace eljur_notifier.MsDbNS.CheckerNS
{
    public class MsDbChecker : EljurBaseClass
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
