using System;
using System.Configuration;

namespace eljur_notifier.AppCommonNS
{
    public class Config
    {
        public  String ConStrFbDB { get; set; }
        public  Double IntervalRequest { get; set; }
        public  Double IntervalSleepBeforeReset { get; set; }
        public  String EljurApiTocken { get; set; }
        public  Double FrenchLeaveInterval { get; set; }
        public  String ConStrMsDB { get; set; }
        public  String ConStrMsDBTests { get; set; }
        public  TimeSpan timeFromDel { get; set; }
        public  TimeSpan timeToDel { get; set; }
        public  int ConfigsTreeIdResourceInput1 { get; set; }
        public  int ConfigsTreeIdResourceInput2 { get; set; }
        public  int ConfigsTreeIdResourceOutput1 { get; set; }
        public  int ConfigsTreeIdResourceOutput2 { get; set; }
        

        public Config()
        {
            this.ConStrFbDB = Properties.Settings.Default.ConStrFirebird;
            this.IntervalRequest = Properties.Settings.Default.IntervalRequest;
            this.IntervalSleepBeforeReset = Properties.Settings.Default.IntervalSleepBeforeReset;
            this.EljurApiTocken = Properties.Settings.Default.EljurApiTocken;
            this.FrenchLeaveInterval = Properties.Settings.Default.FrenchLeaveInterval;
            this.ConStrMsDB = ConfigurationManager.ConnectionStrings["StaffContext"].ToString();
            this.ConStrMsDBTests = ConfigurationManager.ConnectionStrings["StaffContextTests"].ToString();
            this.timeFromDel = Properties.Settings.Default.timeFromDel;
            this.timeToDel = Properties.Settings.Default.timeToDel;
            this.ConfigsTreeIdResourceInput1 = Properties.Settings.Default.ConfigsTreeIdResourceInput1;
            this.ConfigsTreeIdResourceInput2 = Properties.Settings.Default.ConfigsTreeIdResourceInput2;
            this.ConfigsTreeIdResourceOutput1 = Properties.Settings.Default.ConfigsTreeIdResourceOutput1;
            this.ConfigsTreeIdResourceOutput2 = Properties.Settings.Default.ConfigsTreeIdResourceOutput2;

        }
        
    }
}
