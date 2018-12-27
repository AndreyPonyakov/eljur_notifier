using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eljur_notifier.AppCommon;
using System.Data.SqlClient;
using eljur_notifier.EljurNS;

namespace eljur_notifier.MsDbNS
{
    class MsDbCatcherFirstPass
    {
        internal protected Message message { get; set; }
        internal protected MsDb msDb { get; set; }
        internal protected Config config { get; set; }
        internal protected EljurApiSender eljurApiSender { get; set; }

        public MsDbCatcherFirstPass(Config Config, MsDb MsDb)
        {
            this.message = new Message();
            this.msDb = MsDb;
            this.config = Config;
            this.eljurApiSender = new EljurApiSender(config, msDb);
        }

        public void catchFirstPass()
        {

            msDb.dbcon.Open();
            SqlCommand command = new SqlCommand("SELECT PupilIdOld, EventTime FROM Events WHERE NotifyWasSend = 0 AND EventName = 'Первый проход' ORDER BY EventTime", msDb.dbcon);
            SqlDataReader reader = command.ExecuteReader();
            message.Display("SELECT PupilIdOld, EventTime FROM Events WHERE NotifyWasSend = 0 AND EventName = 'Первый проход' ORDER BY EventTime", "Warn");
            while (reader.Read())
            {
                message.Display(String.Format("{0} - {0}", reader[0].ToString(), reader[1].ToString()), "Trace");
                int PupilIdOld = Convert.ToInt32(reader[0]);
                TimeSpan EventTime = TimeSpan.Parse(reader[1].ToString());
                object[] PupilIdOldAndTime = new object[2];
                PupilIdOldAndTime[0] = PupilIdOld;
                PupilIdOldAndTime[1] = EventTime;
                Boolean result = eljurApiSender.SendNotifyFirstPass(PupilIdOldAndTime);
                if (result == true)
                {
                    msDb.SetStatusNotifyWasSend(PupilIdOld);
                }
                //break;
            }
            msDb.dbcon.Close();
           
        }


    }
}
