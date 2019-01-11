using System;
using System.Threading;
using NLog;



namespace eljur_notifier.AppCommonNS
{
    public class Message
    {
        internal protected Logger logger { get; set; }
        internal protected Exception exception { get; set; }
        public Message()
        {
            this.logger = LogManager.GetCurrentClassLogger();      
        }
        public void Display(String MesStr, String level, Exception ex = null, Action act = null)
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
                Console.WriteLine("Exception information: {0}", exception);
            }
            else if (level == "Fatal")
            {
                logger.Fatal(MesStr);
                Console.ForegroundColor = ConsoleColor.DarkRed;
                CloseProgram(new Action(delegate
                {
                    act?.Invoke();
                    SMTP smtp = new SMTP();
                    smtp.SendEmail(MesStr + exception.Message);
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

        public void CheckEmailSending()
        {
            try
            {
                throw new Exception();
            }
            catch (Exception ex)
            {
                Display("fatal message", "Fatal", ex);
            }
        }

        public static void CloseProgram()
        {
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
