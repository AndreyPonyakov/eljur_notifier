using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using NLog;
using NLog.Targets;
using NLog.Config;
using NLog.Common;
using System.Net.Mail;


namespace eljur_notifier.AppCommon
{
    //delegate void MessageShowAction(String message, String level, Exception exception);
    class Message
    {
        internal protected Logger logger { get; set; }
        internal protected Exception exception { get; set; }
        //MessageShowAction _mes;
        public Message()
        {
            this.logger = LogManager.GetCurrentClassLogger();
            //this.Rigister_mes(new MessageShowAction(this.Display));
            

        }
        //public void Rigister_mes(MessageShowAction mes)
        //{
        //    _mes = mes;
        //}
        public void Display(String MesStr, String level, Exception ex = null)
        {
          
            if (ex != null)
            {
                this.exception = ex;
            }

            if (level == "Trace")
            {
                logger.Info(MesStr);
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
                logger.Error(ex, MesStr);
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else if (level == "Fatal")
            {
                logger.Fatal(MesStr);
                Console.ForegroundColor = ConsoleColor.DarkRed;
                CloseProgram(new Action(delegate
                {
                    SMTP smtp = new SMTP();
                    smtp.SendEmail(MesStr + exception.Message);
                    //smtp.SendEmail(this.exeption.ToString());
                    Console.WriteLine(MesStr);
                    Thread.Sleep(10000);

                }));

            }
            else if (level == "Debug")
            {
                logger.Debug(MesStr);
                Console.ForegroundColor = ConsoleColor.Green;
                
            }
            Console.WriteLine(MesStr);
            Console.ResetColor();

        }
       


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
        //message.Display("error message without exeption", "Error");
        //message.Display("error message", "Error", ex);
        //Thread.Sleep(10000);


    }
}
