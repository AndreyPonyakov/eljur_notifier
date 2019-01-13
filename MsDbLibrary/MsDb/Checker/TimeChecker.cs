using System;
using eljur_notifier.AppCommonNS;


namespace MsDbLibraryNS.MsDbNS.CheckerNS
{
    public class TimeChecker : MsDbBaseClass
    {
        public  TimeSpan timeFromDel { get; set; }
        public  TimeSpan timeToDel { get; set; }

        public TimeChecker(TimeSpan TimeFromDel = default(TimeSpan), TimeSpan TimeToDel = default(TimeSpan)) 
            : base(new Message()) {
            if (TimeFromDel == default(TimeSpan) || TimeToDel == default(TimeSpan))
            {
                this.timeFromDel = TimeFromDel;
                this.timeToDel = TimeToDel;
            }         
        }

        public void CheckTime(Action actionAtMidnight)
        {
            var timeNow = DateTime.Now.TimeOfDay;
            if (timeNow > timeFromDel && timeNow < timeToDel)
            {
                message.Display("Time now is between " + timeFromDel.ToString() + " and " + timeToDel.ToString(), "Warn");
                //GOTO AppRunner                          
                actionAtMidnight();                                                         
            }
        }



    }
}
