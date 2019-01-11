using eljur_notifier.AppCommonNS;
using eljur_notifier.MsDbNS.FillerNS;
using eljur_notifier.MsDbNS.CleanerNS;

namespace eljur_notifier.MsDbNS.CheckerNS
{
    public class MsDbChecker : EljurBaseClass
    {           
        public MsDbChecker() : base(new Message(), new MsDbFiller(), new ExistChecker(), new EmptyChecker(), new MsDbCleaner())
        {
            this.CheckMsDb();
        }       

        public void CheckMsDb()
        {
            if (existChecker.IsTableExist("Pupils") && existChecker.IsTableExist("Schedules"))
            {
                message.Display("msDb already exist", "Warn");
            }
            else
            {
                msDbFiller.FillMsDb();
            }

            if (emptyChecker.IsTableEmpty("Schedules"))
            {
                message.Display("Schedules are Empty", "Warn");
                msDbCleaner.clearTableDb("Schedules");
                msDbFiller.FillOnlySchedules();
            }
            else
            {
                message.Display("Schedules are not Empty", "Warn");
            }

            if (emptyChecker.IsTableEmpty("Pupils"))
            {
                message.Display("Pupils are Empty", "Warn");
                msDbCleaner.clearTableDb("Pupils");
                msDbFiller.FillOnlyPupils();
            }
            else
            {
                message.Display("Pupils are not Empty", "Warn");
            }

        }

    }
}
