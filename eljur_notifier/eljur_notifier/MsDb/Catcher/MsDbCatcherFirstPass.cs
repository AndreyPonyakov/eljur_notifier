using System;
using System.Linq;
using eljur_notifier.AppCommonNS;
using eljur_notifier.EljurNS;
using eljur_notifier.MsDbNS.SetterNS;
using eljur_notifier.StaffModel;

namespace eljur_notifier.MsDbNS.CatcherNS
{
    public class MsDbCatcherFirstPass : EljurBaseClass
    {
        public MsDbCatcherFirstPass() : base(new Message(), new StaffContext(), new MsDbSetter(), new EljurApiSender()) { }
   
        public void catchFirstPass()
        {
            using (this.StaffCtx = new StaffContext())
            {
                var PupilIdOldAndTimeRows = from e in StaffCtx.Events
                                            where e.NotifyWasSend == false && e.EventName == "Первый проход"
                                            orderby e.EventTime
                                            select new 
                                            {
                                                PupilIdOld = e.PupilIdOld,
                                                EventTime = e.EventTime
                                            };
                foreach (var PupilIdOldAndTime in PupilIdOldAndTimeRows)
                {
                    var PupilIdOldAndTimeMassObjects = new object[2];
                    PupilIdOldAndTimeMassObjects[0] = PupilIdOldAndTime.PupilIdOld;
                    PupilIdOldAndTimeMassObjects[1] = PupilIdOldAndTime.EventTime;
                    Boolean result = eljurApiSender.SendNotifyFirstPass(PupilIdOldAndTimeMassObjects);
                    if (result == true)
                    {
                        msDbSetter.SetStatusNotifyWasSend(Convert.ToInt32(PupilIdOldAndTime.PupilIdOld));
                    }
                }

            }

            
        }
        
    }
}
