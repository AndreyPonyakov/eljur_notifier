using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using eljur_notifier.AppCommon;
using eljur_notifier.MsDbNS.RequesterNS;
using eljur_notifier.StaffModel;

namespace eljur_notifier.MsDbNS.SaverNS
{
    class MsDbSaver
    {
        internal protected Message message { get; set; }
        internal protected Config config { get; set; }
        internal protected MsDbRequester msDbRequester { get; set; }
        internal protected SqlConnection dbcon { get; set; }
        internal protected object[] PupilIdOldAndEnable { get; set; }
        internal protected StaffContext StaffCtx { get; set; }

        public MsDbSaver(Config Config)
        {
            this.message = new Message();
            this.config = Config;
            this.msDbRequester = new MsDbRequester(config);
        }

        public void SaveFlags()
        {
            var rows = new List<object[]>();
            using (this.dbcon = new SqlConnection(config.ConStrMsDB))
            {
                this.dbcon.Open();
                using (SqlCommand command = new SqlCommand("SELECT PupilIdOld, NotifyEnable FROM Pupils ORDER BY PupilIdOld", this.dbcon))
                {
                    message.Display("SELECT PupilIdOld, NotifyEnable FROM Pupils ORDER BY PupilIdOld", "Warn");
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PupilIdOldAndEnable = new object[reader.FieldCount];
                            reader.GetValues(PupilIdOldAndEnable);
                            message.Display(String.Format("{0} - {1}", PupilIdOldAndEnable[0].ToString(), PupilIdOldAndEnable[1].ToString()), "Trace");
                            int PupilIdOld = Convert.ToInt32(PupilIdOldAndEnable[0]);
                            Boolean NotifyEnable = (bool)PupilIdOldAndEnable[1];
                            rows.Add(PupilIdOldAndEnable);
                        }
                    }
                }
            }
        }

        public void RestoreFlags()
        {
            using (this.StaffCtx = new StaffContext())
            {
                foreach (object[] Item in PupilIdOldAndEnable)
                {
                    int PupilIdOld = Convert.ToInt32(PupilIdOldAndEnable[0]);
                    Boolean NotifyEnable = (bool)PupilIdOldAndEnable[1];
                    var result = StaffCtx.Pupils.SingleOrDefault(e => e.PupilIdOld == PupilIdOld);
                    if (result != null)
                    {
                        result.NotifyEnable = NotifyEnable;
                        StaffCtx.SaveChanges();
                        message.Display("NotifyEnable to " + PupilIdOld + " PupilIdOld was set", "Trace");
                    }
                }
            }

        }  






    }
}
