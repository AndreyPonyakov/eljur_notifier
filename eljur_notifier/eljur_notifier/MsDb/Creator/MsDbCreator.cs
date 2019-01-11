using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eljur_notifier.AppCommonNS;
using System.Data.SqlClient;
using eljur_notifier.MsDbNS.FillerNS;

namespace eljur_notifier.MsDbNS.CreatorNS
{
    public class MsDbCreator
    {
        internal protected Message message { get; set; }
        internal protected Config config { get; set; }
        internal protected CleanCreator cleanCreator { get; set; }
        internal protected MsDbFiller msDbFiller { get; set; }

        public MsDbCreator(Config Config)
        {
            this.message = new Message();
            this.config = Config;
            this.cleanCreator = new CleanCreator();
            this.msDbFiller = new MsDbFiller(config);
        }


        public void CreateMsDb()
        {        
            cleanCreator.createCleanMsDb(config.ConStrMsDB);
            msDbFiller.FillMsDb();
        }

    }
}
