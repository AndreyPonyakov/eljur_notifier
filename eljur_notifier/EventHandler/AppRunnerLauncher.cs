using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eljur_notifier.MsDbNS.SaverNS;
using eljur_notifier.AppCommon;

namespace eljur_notifier.EventHandlerNS
{
    class AppRunnerLauncher
    {
        internal protected Message message { get; set; }
        internal protected Config config { get; set; }
        internal protected MsDbSaver msDbSaver { get; set; }
        internal protected AppRunner appRunner { get; set; }


        public AppRunnerLauncher()
        {
            this.message = new Message();
            this.config = new Config();
            this.msDbSaver = new MsDbSaver(config);
            this.msDbSaver.SaveFlags();
        }

        public void Launch(string[] args)
        {
            AppRunner appRunner = new AppRunner(msDbSaver);
            appRunner.Run(args);
        }
    }
}
