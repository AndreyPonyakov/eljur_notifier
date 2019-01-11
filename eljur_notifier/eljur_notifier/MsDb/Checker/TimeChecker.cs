using System;
using eljur_notifier.AppCommonNS;


namespace eljur_notifier.MsDbNS.CheckerNS
{
    public class TimeChecker : EljurBaseClass
    {
        public TimeChecker() : base(new Message(), new Config()) { }

        public void CheckTime(Action actionAtMidnight)
        {
            var timeNow = DateTime.Now.TimeOfDay;
            if (timeNow > config.timeFromDel && timeNow < config.timeToDel)
            {
                message.Display("Time now is between " + config.timeFromDel.ToString() + " and " + config.timeToDel.ToString(), "Warn");
                //GOTO AppRunner                          
                actionAtMidnight();                                                         
            }
        }



    }
}
