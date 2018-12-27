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
    class MsDbCatcherLastPass
    {
        internal protected Message message { get; set; }
        internal protected MsDb msDb { get; set; }
        internal protected Config config { get; set; }
        internal protected EljurApiSender eljurApiSender { get; set; }

        public MsDbCatcherLastPass(Config Config, MsDb MsDb)
        {
            this.message = new Message();
            this.msDb = MsDb;
            this.config = Config;
            this.eljurApiSender = new EljurApiSender(config, msDb);
        }

        public void catchLastPass() 
        {
            msDb.dbcon.Open();
            TimeSpan timeNow = DateTime.Now.TimeOfDay;
            TimeSpan EventTimeNowSubstract15Min = timeNow.Add(new TimeSpan(0, -15, 0));
            SqlCommand command = new SqlCommand("SELECT PupilIdOld, EventTime FROM Events WHERE NotifyWasSend = 0 AND EventName = 'Вышел' AND EventTime < '" + EventTimeNowSubstract15Min + "' ORDER BY EventTime", msDb.dbcon);
            message.Display("SELECT PupilIdOld, EventTime FROM Events WHERE NotifyWasSend = 0 AND EventName = 'Вышел' ORDER BY EventTime", "Warn");
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
                Boolean result = eljurApiSender.SendNotifyLastPass(PupilIdOldAndTime);
                if (result == true)
                {
                    msDb.SetStatusNotifyWasSend(Convert.ToInt32(PupilIdOldAndTime[0]));
                }
            }
        }




    }
}
