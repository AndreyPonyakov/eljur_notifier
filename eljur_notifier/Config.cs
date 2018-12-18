using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eljur_notifier
{
    class Config
    {
        internal protected String ConnectStr { get; set; }
        internal protected Double IntervalRequest { get; set; }
        public Config()
        {
            this.ConnectStr = Properties.Settings.Default.ConStrFirebird;
            this.IntervalRequest = Properties.Settings.Default.IntervalRequest;
        }
        
    }
}
