using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eljur_notifier.StaffModel;
using eljur_notifier.MsDbNS.SetterNS;
using eljur_notifier.EljurNS;

namespace eljur_notifier.AppCommonNS
{
    public class EljurBaseClass
    {
        internal protected Message message { get; set; }
        internal protected Config config { get; set; }
        internal protected StaffContext StaffCtx { get; set; }
        internal protected MsDbSetter msDbSetter { get; set; }
        internal protected EljurApiSender eljurApiSender { get; set; }

        public EljurBaseClass(Message Message)
        {
            this.message = Message;
        }

        public EljurBaseClass(Message Message, Config Config)
        {
            this.message = Message;
            this.config = Config;
        }

        public EljurBaseClass(Message Message, StaffContext StaffContext)
        {
            this.message = Message;
            this.StaffCtx = StaffContext;
        }

        public EljurBaseClass(Message Message, Config Config, StaffContext StaffContext)
        {
            this.message = Message;
            this.config = Config;
            this.StaffCtx = StaffContext;
        }

        public EljurBaseClass(Message Message, StaffContext StaffContext, MsDbSetter MsDbSetter, EljurApiSender EljurApiSender)
        {
            this.message = Message;
            this.StaffCtx = StaffContext;
            this.msDbSetter = MsDbSetter;
            this.eljurApiSender = EljurApiSender;
        }

    }
}
