using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace eljur_notifier
{
    public class Config
    {
        internal protected String ConStrFbDB { get; set; }
        internal protected Double IntervalRequest { get; set; }
        internal protected Double IntervalSleepBeforeReset { get; set; }
        internal protected String EljurApiTocken { get; set; }
        internal protected Double FrenchLeaveInterval { get; set; }
        internal protected String ConStrMsDB { get; set; }
        internal protected TimeSpan timeFromDel { get; set; }
        internal protected TimeSpan timeToDel { get; set; }
        internal protected int ConfigsTreeIdResourceInput1 { get; set; }
        internal protected int ConfigsTreeIdResourceInput2 { get; set; }
        internal protected int ConfigsTreeIdResourceOutput1 { get; set; }
        internal protected int ConfigsTreeIdResourceOutput2 { get; set; }
        

        public Config()
        {
            this.ConStrFbDB = Properties.Settings.Default.ConStrFirebird;
            this.IntervalRequest = Properties.Settings.Default.IntervalRequest;
            this.IntervalSleepBeforeReset = Properties.Settings.Default.IntervalSleepBeforeReset;
            this.EljurApiTocken = Properties.Settings.Default.EljurApiTocken;
            this.FrenchLeaveInterval = Properties.Settings.Default.FrenchLeaveInterval;
            this.ConStrMsDB = ConfigurationManager.ConnectionStrings["StaffContext"].ToString();
            this.timeFromDel = Properties.Settings.Default.timeFromDel;
            this.timeToDel = Properties.Settings.Default.timeToDel;
            this.ConfigsTreeIdResourceInput1 = Properties.Settings.Default.ConfigsTreeIdResourceInput1;
            this.ConfigsTreeIdResourceInput2 = Properties.Settings.Default.ConfigsTreeIdResourceInput2;
            this.ConfigsTreeIdResourceOutput1 = Properties.Settings.Default.ConfigsTreeIdResourceOutput1;
            this.ConfigsTreeIdResourceOutput2 = Properties.Settings.Default.ConfigsTreeIdResourceOutput2;

        }
        
    }
}
