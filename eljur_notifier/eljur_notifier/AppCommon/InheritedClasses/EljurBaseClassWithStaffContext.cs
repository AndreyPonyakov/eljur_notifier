using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eljur_notifier.StaffModel;

namespace eljur_notifier.AppCommonNS
{
    public class EljurBaseClassWithStaffContext: EljurBaseClass
    {
        internal protected StaffContext StaffCtx { get; set; }

        public EljurBaseClassWithStaffContext()
        {

        }
    }
}
