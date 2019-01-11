using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eljur_notifier.AppCommonNS
{
    public class EljurBaseClass
    {
        internal protected Message message { get; set; }

        public EljurBaseClass()
        {
            this.message = new Message();

        }
    }
}
