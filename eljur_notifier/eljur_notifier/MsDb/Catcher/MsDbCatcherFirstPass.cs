using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using eljur_notifier.AppCommon;
using eljur_notifier.EljurNS;
using eljur_notifier.MsDbNS.SetterNS;

namespace eljur_notifier.MsDbNS.CatcherNS
{
    public class MsDbCatcherFirstPass
    {
        internal protected Message message { get; set; }
        internal protected MsDb msDb { get; set; }
        internal protected Config config { get; set; }
        internal protected MsDbSetter msDbSetter { get; set; }
        internal protected EljurApiSender eljurApiSender { get; set; }
        internal protected SqlConnection dbcon { get; set; }

        public MsDbCatcherFirstPass(Config Config, MsDb MsDb)
        {
            this.message = new Message();
            this.msDb = MsDb;
            this.config = Config;
            this.msDbSetter = new MsDbSetter();
            this.eljurApiSender = new EljurApiSender(config);
        }

        public void catchFirstPass()
        {
            var rows = new List<object[]>();
            using (this.dbcon = new SqlConnection(config.ConStrMsDB))
            {
                msDb.dbcon.Open();
                using (SqlCommand command = new SqlCommand("SELECT PupilIdOld, EventTime FROM Events WHERE NotifyWasSend = 0 AND EventName = 'Первый проход' ORDER BY EventTime", msDb.dbcon))
                {
                    message.Display("SELECT PupilIdOld, EventTime FROM Events WHERE NotifyWasSend = 0 AND EventName = 'Первый проход' ORDER BY EventTime", "Warn");
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
                }
            }

            foreach (object[] PupilIdOldAndTime in rows)
            {
                Boolean result = eljurApiSender.SendNotifyFirstPass(PupilIdOldAndTime);
                if (result == true)
                {
                    msDbSetter.SetStatusNotifyWasSend(Convert.ToInt32(PupilIdOldAndTime[0]));
                }
            }
            
        }
        
    }
}
