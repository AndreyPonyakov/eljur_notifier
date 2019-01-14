using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using eljur_notifier.FirebirdNS;
using eljur_notifier.MsDbNS;
using MsDbLibraryNS.MsDbNS.CheckerNS;
using eljur_notifier.AppCommonNS;
using MsDbLibraryNS.MsDbNS.CatcherNS;
using eljur_notifier.EljurNS;
using MsDbLibraryNS.MsDbNS.SetterNS;
using MsDbLibraryNS;

namespace eljur_notifier.EventHandlerNS
{
    public class EventHandlerEljur : EljurBaseClass
    {
        public EventHandlerEljur() 
            : base(new Message(), new Config(), new MsDb(), new Firebird(), new TimeChecker(), 
                  new EljurApiSender(), new MsDbCatcherFirstPass(), new MsDbCatcherLastPass(), new MsDbSetter()) { }
 
        public async Task GetDataFb(CancellationToken cancellationToken) 
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    DateTime startTime = DateTime.Now;
                    List<object[]> curEvents = firebird.getStaffByTimeStamp();
                    msDb.CheckEventsDb(curEvents);       
                    TimeSpan deltaTime = DateTime.Now - startTime;
                    TimeSpan IntervalRequest = TimeSpan.FromMilliseconds(config.IntervalRequest);
                    TimeSpan sleepTime = IntervalRequest - deltaTime;
                    message.Display("sleepTime is: " + sleepTime.ToString(), "Trace");
                    await Task.Delay(sleepTime);
                }
            }
            catch (Exception ex)
            {
                message.Display("An unprocessed error has occurred in Task GetDataFb. See log for more information.", "Error", ex);
            }
        }        

      

        public async Task CheckTimekMsDb(CancellationToken cancellationToken, Action actionAtMidnight)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    timeChecker.CheckTime(actionAtMidnight);
                    await Task.Delay(1000);
                }
            }
            catch (Exception ex)
            {
                message.Display("An unprocessed error has occurred in Task CheckTimekMsDb. See log for more information.", "Error", ex);
            }
        }

        public async Task CatchEventFirstPass(CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var PupilIdOldAndTimeRows = msDbCatcherFirstPass.catchFirstPass();
                    foreach (var PupilIdOldAndTime in PupilIdOldAndTimeRows)
                    {
                        Boolean result = eljurApiSender.SendNotifyFirstPass(PupilIdOldAndTime);
                        if (result == true)
                        {
                            msDbSetter.SetStatusNotifyWasSend(Convert.ToInt32(PupilIdOldAndTime[0]));
                        }
                    }
                    await Task.Delay(10000);
                }
            }
            catch (Exception ex)
            {
                message.Display("An unprocessed error has occurred in Task CatchEventFirstPass. See log for more information.", "Error", ex);
            }
        }

        public async Task CatchEventLastPass(CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var PupilIdOldAndTimeRows = msDbCatcherLastPass.catchLastPass();

                    foreach (var PupilIdOldAndTime in PupilIdOldAndTimeRows)
                    {
                        Boolean result = eljurApiSender.SendNotifyLastPass(PupilIdOldAndTime);
                        if (result == true)
                        {
                            msDbSetter.SetStatusNotifyWasSend(Convert.ToInt32(PupilIdOldAndTime[0]));
                        }
                    }
                    await Task.Delay(10000);
                }
            }
            catch (Exception ex)
            {
                message.Display("An unprocessed error has occurred in Task CatchEventLastPass. See log for more information.", "Error", ex);
            }
        }






    }
}
