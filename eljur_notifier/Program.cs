using System.Collections.Generic;
using System.Linq;
using eljur_notifier.EventHandlerNS;



namespace eljur_notifier
{
    class Program
    {
        internal protected AppRunner appRunner { get; set; }
        public static List<string> MainMethodArgs = new List<string>();
        static void Main(string[] args)
        {
            MainMethodArgs = args.ToList();
            AppRunner appRunner = new AppRunner();
            appRunner.Run(MainMethodArgs.ToArray());
        }     
   
    }
}

