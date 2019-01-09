using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eljur_notifier.AppCommon;
using eljur_notifier.MsDbNS;
using System.Data.SqlClient;
using eljur_notifier.MsDbNS.RequesterNS;
using eljur_notifier.MsDbNS.SetterNS;

namespace eljur_notifier.EljurNS
{
    public class EljurApiSender
    {
        internal protected Message message { get; set; }
        internal protected Config config { get; set; }
        internal protected MsDbRequester msDbRequester { get; set; }
        internal protected MsDbSetter msDbSetter { get; set; }

        public EljurApiSender(Config Config)
        {
            this.message = new Message();
            this.config = Config;
            this.msDbRequester = new MsDbRequester(config);
            this.msDbSetter = new MsDbSetter(config);
        }

        public Boolean SendNotifyFirstPass(object[] PupilIdOldAndTime)
        {           
            int EljurAccountId = msDbRequester.getEljurAccountIdByPupilIdOld(Convert.ToInt32(PupilIdOldAndTime[0]));          
            String FullFIO = msDbRequester.getFullFIOByPupilIdOld(Convert.ToInt32(PupilIdOldAndTime[0]));

            String Clas = msDbRequester.getClasByPupilIdOld(Convert.ToInt32(PupilIdOldAndTime[0]));

            if ((Convert.ToInt32(DateTime.Today.Day) ==1) && (Convert.ToInt32(DateTime.Today.Month) == 9))
            {
                message.Display("Today is 01.09 and we will change all classes in Pupils Table", "Info");
                EljurApiRequester eljurApiRequester = new EljurApiRequester(config);
                Clas = eljurApiRequester.getClasByFullFIO(FullFIO);
            }

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

            var EventTime = TimeSpan.Parse(PupilIdOldAndTime[1].ToString());
            var EventTimePlus15Min = EventTime.Add(new TimeSpan(0, 15, 0));
            var timeNow = DateTime.Now.TimeOfDay;

            if (timeNow > EventTimePlus15Min)
            {
                if (EventTime > EndTimeLessons)
                {
                    message.Display("Notify about student " + FullFIO + " who went home was sent to " + EljurAccountId + " EljurAccountId", "Warn");
                    return true;
                }
                else
                {
                    message.Display("Notify about student " + FullFIO + " who went home too early was sent to " + EljurAccountId + " EljurAccountId", "Warn");
                    msDbSetter.SetStatusWentHomeTooEarly(Convert.ToInt32(PupilIdOldAndTime[0]));
                    return true;
                }          
            }
            else
            {
                message.Display("Too early to make a decision. 15 minutes have not passed yet.", "Warn");
                return false;
            }
        }      
    }
}
