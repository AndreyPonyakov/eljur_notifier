using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using eljur_notifier.AppCommon;

namespace eljur_notifier.MsDbNS.CheckerNS
{
    class ExistChecker
    {
        internal protected Message message { get; set; }
        internal protected Config config { get; set; }
        internal protected SqlConnection dbcon { get; set; }


        public ExistChecker(Config Config)
        {
            this.config = Config;

        }


       


    }
}
