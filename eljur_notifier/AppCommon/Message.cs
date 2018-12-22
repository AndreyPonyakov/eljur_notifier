using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace eljur_notifier.AppCommon
{
    delegate void MessageShowAction(String message, String level);
    class Message
    {
        internal protected Logger logger { get; set; }
        MessageShowAction _mes;
        public Message()
        {
            this.logger = LogManager.GetCurrentClassLogger();
            this.Rigister_mes(new MessageShowAction(this.Display));

        }
        public void Rigister_mes(MessageShowAction mes)
        {
            _mes = mes;
        }
        public void Display(String MesStr, String level)
        {
            
            if (level == "Trace")
            {
                logger.Trace(MesStr);
            }
            else if (level == "Info")
            {
                logger.Info(MesStr);
                Console.ForegroundColor = ConsoleColor.Blue;
            }
            else if (level == "Warn")
            {
                logger.Warn(MesStr);
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else if (level == "Error")
            {
                logger.Error(MesStr);
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else if (level == "Fatal")
            {
                logger.Fatal(MesStr);
                Console.ForegroundColor = ConsoleColor.DarkRed;
            }
            else if (level == "Debug")
            {
                logger.Debug(MesStr);
                Console.ForegroundColor = ConsoleColor.Green;
                
            }
            Console.WriteLine(MesStr);
            Console.ResetColor();

        }
        //    CloseProgram(new Action(delegate
        //    {
        //        Console.WriteLine("Firebird database doesn't exist. Program will be closed!");
        //        Thread.Sleep(2000);

        //    }));


        public static void CloseProgram()
        {
            //Process.GetCurrentProcess().Kill();
            Environment.Exit(1);
        }

        public static void CloseProgram(Action actionBeforeClosing)
        {
            actionBeforeClosing();
            CloseProgram();
        }

        //message.Display("trace message", "Trace");
        //message.Display("debug message", "Debug");
        //message.Display("info message", "Info");
        //message.Display("warn message", "Warn");
        //message.Display("error message", "Error");
        //message.Display("fatal message", "Fatal");
        //Thread.Sleep(10000);


    }
}
