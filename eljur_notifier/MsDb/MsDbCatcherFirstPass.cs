﻿using System;
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
            message.Display("SELECT PupilIdOld, EventTime FROM Events WHERE NotifyWasSend = 0 AND EventName = 'Первый проход' ORDER BY EventTime", "Warn");
            var rows = new List<object[]>();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var PupilIdOldAndTime = new object[reader.FieldCount];
                    //message.Display("reader.FieldCount " + reader.FieldCount.ToString(), "Trace");
                    reader.GetValues(PupilIdOldAndTime);
                    //message.Display("PupilIdOldAndTime[0] " + PupilIdOldAndTime[0].ToString(), "Trace");
                    //message.Display("PupilIdOldAndTime[1] " + PupilIdOldAndTime[1].ToString(), "Trace");
                    message.Display(String.Format("{0} - {1}", PupilIdOldAndTime[0].ToString(), PupilIdOldAndTime[1].ToString()), "Trace");
                    int PupilIdOld = Convert.ToInt32(PupilIdOldAndTime[0]);
                    TimeSpan EventTime = TimeSpan.Parse(PupilIdOldAndTime[1].ToString());
                    //object[] PupilIdOldAndTime = new object[2];
                    //PupilIdOldAndTime[0] = PupilIdOld;
                    //PupilIdOldAndTime[1] = EventTime;                 
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
                    msDb.SetStatusNotifyWasSend(Convert.ToInt32(PupilIdOldAndTime[0]));
                }
            }
        }
    }
}
