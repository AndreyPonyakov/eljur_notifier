using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eljur_notifier.StaffModel;

namespace eljur_notifier.AppCommonNS
{
    public class EljurBaseClass
    {
        internal protected Message message { get; set; }
        internal protected Config config { get; set; }
        internal protected StaffContext StaffCtx { get; set; }

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

    }
}
