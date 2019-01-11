using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eljur_notifier.AppCommonNS
{
    public class EljurBaseClassWithConfig: EljurBaseClass
    {
        internal protected Config config { get; set; }

        public EljurBaseClassWithConfig()
        {
            this.config = new Config();
        }
    }
}
