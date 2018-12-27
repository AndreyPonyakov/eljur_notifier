using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eljur_notifier.AppCommon;
using eljur_notifier.MsDbNS;

namespace eljur_notifier.EljurNS
{
    class EljurApiSender
    {
        internal protected Message message { get; set; }
        internal protected MsDb msDb { get; set; }
        internal protected Config config { get; set; }

        public EljurApiSender(Config Config, MsDb MsDb)
        {
            this.message = new Message();
            this.msDb = MsDb;
            this.config = Config;
        }

        public Boolean SendNotifyFirstPass(object[] PupilIdOldAndTime)
        {
            int EljurAccountId = msDb.getEljurAccountIdByPupilIdOld(Convert.ToInt32(PupilIdOldAndTime[0]));
            String FullFIO = msDb.getFullFIOByPupilIdOld(Convert.ToInt32(PupilIdOldAndTime[0]));

            message.Display("Notify about FirstPass by student " + FullFIO + " was send to " + EljurAccountId + " EljurAccountId", "Warn");
            return true;
        } 


    }
}
