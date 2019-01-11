using System;
using System.Net.Mail;
using System.Net;

namespace eljur_notifier.AppCommonNS
{
    public class SMTP
    {
        internal protected String SmtpLogin { get; set; }
        internal protected String SmtpPassword { get; set; }
        internal protected String SmtpTo { get; set; }

        public SMTP()
        {
            this.SmtpTo = Environment.GetEnvironmentVariable("SmtpLogin", EnvironmentVariableTarget.User);
            this.SmtpLogin = Environment.GetEnvironmentVariable("SmtpLogin", EnvironmentVariableTarget.User);
            this.SmtpPassword = Environment.GetEnvironmentVariable("SmtpPassword", EnvironmentVariableTarget.User);
            //Console.WriteLine("SmtpLogin: " + SmtpLogin);
            //Console.WriteLine("SmtpPassword: " + SmtpPassword);
        }

        public void SendEmail(String Message)
        {
            // отправитель - устанавливаем адрес и отображаемое в письме имя
            MailAddress from = new MailAddress(SmtpLogin, "Admin");
            // кому отправляем
            MailAddress to = new MailAddress(SmtpLogin);
            // создаем объект сообщения
            MailMessage m = new MailMessage(from, to);
            // тема письма
            m.Subject = "Fatal error eljur_notifier";
            // текст письма
            //m.Body = "Письмо-тест работы smtp-клиента";
            m.Body = "<h2>"+ Message + "</h2>";
            // письмо представляет код html
            m.IsBodyHtml = true;
            // адрес smtp-сервера и порт, с которого будем отправлять письмо
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            // логин и пароль
            smtp.Credentials = new NetworkCredential(SmtpLogin, SmtpPassword);
            smtp.EnableSsl = true;
            smtp.Send(m);

        }
       

    }
}
