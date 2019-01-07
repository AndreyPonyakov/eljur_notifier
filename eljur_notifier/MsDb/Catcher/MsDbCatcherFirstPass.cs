using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eljur_notifier.AppCommon;
using System.Data.SqlClient;
using eljur_notifier.EljurNS;
using eljur_notifier.MsDbNS.SetterNS;

namespace eljur_notifier.MsDbNS.CatcherNS
{
    class MsDbCatcherFirstPass
    {
        internal protected Message message { get; set; }
        internal protected MsDb msDb { get; set; }
        internal protected Config config { get; set; }
        internal protected Setter setter { get; set; }
        internal protected EljurApiSender eljurApiSender { get; set; }

        public MsDbCatcherFirstPass(Config Config, MsDb MsDb)
        {
            this.message = new Message();
            this.msDb = MsDb;
            this.config = Config;
            this.setter = new Setter(config);
            this.eljurApiSender = new EljurApiSender(config);
        }

        public void catchFirstPass()
        {

            msDb.dbcon.Open();
            SqlCommand command = new SqlCommand("SELECT PupilIdOld, EventTime FROM Events WHERE NotifyWasSend = 0 AND EventName = 'Первый проход' ORDER BY EventTime", msDb.dbcon);        
            message.Display("SELECT PupilIdOld, EventTime FROM Events WHERE NotifyWasSend = 0 AND EventName = 'Первый проход' ORDER BY EventTime", "Warn");
            var rows = new List<object[]>();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var PupilIdOldAndTime = new object[reader.FieldCount];
                    reader.GetValues(PupilIdOldAndTime);  
                    message.Display(String.Format("{0} - {1}", PupilIdOldAndTime[0].ToString(), PupilIdOldAndTime[1].ToString()), "Trace");
                    int PupilIdOld = Convert.ToInt32(PupilIdOldAndTime[0]);
                    TimeSpan EventTime = TimeSpan.Parse(PupilIdOldAndTime[1].ToString());             
                    rows.Add(PupilIdOldAndTime);
                }
            }
            command.Dispose();
            msDb.dbcon.Close();

            foreach (object[] PupilIdOldAndTime in rows)
            {
                Boolean result = eljurApiSender.SendNotifyFirstPass(PupilIdOldAndTime);
                if (result == true)
                {
                    setter.SetStatusNotifyWasSend(Convert.ToInt32(PupilIdOldAndTime[0]));
                }
            }
        }
    }
}
