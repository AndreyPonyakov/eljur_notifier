using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace eljur_notifier
{
    class Config
    {
        internal protected String ConStrFbDB { get; set; }
        internal protected Double IntervalRequest { get; set; }
        internal protected String EljurApiTocken { get; set; }
        internal protected Double FrenchLeaveInterval { get; set; }
        internal protected String ConStrMsDB { get; set; }
        public Config()
        {
            this.ConStrFbDB = Properties.Settings.Default.ConStrFirebird;
            this.IntervalRequest = Properties.Settings.Default.IntervalRequest;
            this.EljurApiTocken = Properties.Settings.Default.EljurApiTocken;
            this.FrenchLeaveInterval = Properties.Settings.Default.FrenchLeaveInterval;
            this.ConStrMsDB = ConfigurationManager.ConnectionStrings["StaffContext"].ToString();
     
        }
        
    }
}
