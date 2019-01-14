using System;
using eljur_notifier.AppCommonNS;
using MsDbLibraryNS.MsDbNS.RequesterNS;
using MsDbLibraryNS.MsDbNS.SetterNS;
using MsDbLibraryNS;

namespace eljur_notifier.EljurNS
{
    public class EljurApiSender : EljurBaseClass
    {
        public EljurApiSender(String NameorConnectionString = "name=StaffContext") 
            : base(new Message(), new MsDbRequester(NameorConnectionString), new MsDbSetter(NameorConnectionString)) { }

        //NEED REALISATION OF THIS METHOD
        public Boolean SendNotifyFirstPass(object[] PupilIdOldAndTime)
        {           
            int EljurAccountId = msDbRequester.getEljurAccountIdByPupilIdOld(Convert.ToInt32(PupilIdOldAndTime[0]));          
            String FullFIO = msDbRequester.getFullFIOByPupilIdOld(Convert.ToInt32(PupilIdOldAndTime[0]));
            String Clas = msDbRequester.getClasByPupilIdOld(Convert.ToInt32(PupilIdOldAndTime[0]));
            TimeSpan StartTimeLessons = msDbRequester.getStartTimeLessonsByClas(Clas);

            var EventTime = TimeSpan.Parse(PupilIdOldAndTime[1].ToString());
            if (EventTime > StartTimeLessons)
            {
                msDbSetter.SetStatusCameTooLate(Convert.ToInt32(PupilIdOldAndTime[0]));
                message.Display("Notify about student " + FullFIO + " who came too late was sent to " + EljurAccountId + " EljurAccountId", "Warn");
            }
            else
            {
                message.Display("Notify about FirstPass by student " + FullFIO + " was sent to " + EljurAccountId + " EljurAccountId", "Warn");
            }             
            return true;
        }

        
        public Boolean SendNotifyLastPass(object[] PupilIdOldAndTime)
        {
            int EljurAccountId = msDbRequester.getEljurAccountIdByPupilIdOld(Convert.ToInt32(PupilIdOldAndTime[0]));
            String FullFIO = msDbRequester.getFullFIOByPupilIdOld(Convert.ToInt32(PupilIdOldAndTime[0]));
            String Clas = msDbRequester.getClasByPupilIdOld(Convert.ToInt32(PupilIdOldAndTime[0]));
            TimeSpan EndTimeLessons = msDbRequester.getEndTimeLessonsByClas(Clas);
            Boolean NotifyEnable = msDbRequester.getNotifyEnableByPupilIdOld(Convert.ToInt32(PupilIdOldAndTime[0]));

            var EventTime = TimeSpan.Parse(PupilIdOldAndTime[1].ToString());
            var EventTimePlus15Min = EventTime.Add(new TimeSpan(0, 15, 0));
            var timeNow = DateTime.Now.TimeOfDay;

            if (timeNow > EventTimePlus15Min)
            {
                if (EventTime > EndTimeLessons)
                {
                    String NotifyString = "Notify about student " + FullFIO + " who went home was sent to " + EljurAccountId + " EljurAccountId";
                    message.Display(NotifyString, "Warn");
                    return SendNotify(NotifyString, NotifyEnable);
                }
                else
                {
                    String NotifyString = "Notify about student " + FullFIO + " who went home too early was sent to " + EljurAccountId + " EljurAccountId";
                    message.Display(NotifyString, "Warn");
                    msDbSetter.SetStatusWentHomeTooEarly(Convert.ToInt32(PupilIdOldAndTime[0]));
                    return SendNotify(NotifyString, NotifyEnable);
                }          
            }
            else
            {
                message.Display("Too early to make a decision. 15 minutes have not passed yet.", "Warn");
                return false;
            }

        }

        //NEED REALISATION OF THIS METHOD
        public Boolean SendNotify(String NotifyString, Boolean NotifyEnable)
        {
            if (NotifyEnable)
            {
                return true;
            }
            else
            {   
                //In this case need always return true!!!
                return true;
            }
                     
        }





    }
}
