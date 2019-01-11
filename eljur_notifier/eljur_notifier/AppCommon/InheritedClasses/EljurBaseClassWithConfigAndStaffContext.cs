using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eljur_notifier.StaffModel;

namespace eljur_notifier.AppCommonNS
{
    public class EljurBaseClassWithConfigAndStaffContext: EljurBaseClass
    {
        internal protected Config config { get; set; }
        internal protected StaffContext StaffCtx { get; set; }

        public EljurBaseClassWithConfigAndStaffContext()
        {
            this.config = new Config();
        }
    }
}
