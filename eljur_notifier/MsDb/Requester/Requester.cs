using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using eljur_notifier.MsDbNS;
using eljur_notifier.AppCommon;
using eljur_notifier.MsDbNS.SetterNS;

namespace eljur_notifier.MsDbNS.RequesterNS
{
    class Requester
    {
        internal protected Message message { get; set; }
        internal protected Config config { get; set; }
        internal protected Setter setter { get; set; }
        internal protected SqlConnection dbcon { get; set; }

        public Requester(Config Config)
        {
            this.message = new Message();
            this.config = Config;
            this.setter = new Setter(config);

        }

        public static string RandomString(int length)
        {
            Random random = new Random(); 
            const string chars = "АБВ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }


        


        public int getPupilIdOldByFullFio(String FullFIO)
        {
            int PupilIdOld = 0;
            using (this.dbcon = new SqlConnection(config.ConStrMsDB))
            {
                dbcon.Open();
                using (SqlCommand command = new SqlCommand("SELECT PupilIdOld FROM Pupils WHERE FullFIO = '" + FullFIO + "'", dbcon))
                {
                    message.Display("SELECT PupilIdOld FROM Pupils WHERE FullFIO = '" + FullFIO + "'", "Warn");
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            message.Display(String.Format("{0}", reader[0]), "Trace");
                            PupilIdOld = Convert.ToInt32(reader[0]);
                            break;
                        }
                    }
                }
            }
            return PupilIdOld;
        }

        public String getClasByPupilIdOld(int PupilIdOld)
        {
            String Clas = "Default Clas";
            using (this.dbcon = new SqlConnection(config.ConStrMsDB))
            {
                dbcon.Open();
                using (SqlCommand command = new SqlCommand("SELECT Clas FROM Pupils WHERE PupilIdOld = '" + PupilIdOld + "'", dbcon))
                {
                    message.Display("SELECT Clas FROM Pupils WHERE PupilIdOld = '" + PupilIdOld + "'", "Warn");
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            message.Display(String.Format("{0}", reader[0]), "Trace");
                            Clas = reader[0].ToString();
                            break;
                        }
                    }
                }
            }
            return Clas;
        }



        public TimeSpan getEndTimeLessonsByClas(String Clas)
        {
            TimeSpan EndTimeLessons = TimeSpan.Parse("23:59:59");
            using (this.dbcon = new SqlConnection(config.ConStrMsDB))
            {
                dbcon.Open();
                using (SqlCommand command = new SqlCommand("SELECT EndTimeLessons FROM Schedules where Clas = '" + Clas + "'", dbcon))
                {
                    message.Display("SELECT EndTimeLessons FROM Schedules where Clas = '" + Clas + "'", "Warn");
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var colums = new object[reader.FieldCount];
                            reader.GetValues(colums);
                            message.Display(string.Format("{0}", colums[0].ToString()), "Trace");
                            EndTimeLessons = TimeSpan.Parse(colums[0].ToString());
                            //break;
                        }
                    }
                }
            }
            return EndTimeLessons;
        }


        public TimeSpan getStartTimeLessonsByClas(String Clas)
        {
            TimeSpan StartTimeLessons = TimeSpan.Parse("00:00:01");
            using (this.dbcon = new SqlConnection(config.ConStrMsDB))
            {
                dbcon.Open();
                using (SqlCommand command = new SqlCommand("SELECT StartTimeLessons FROM Schedules where Clas = '" + Clas + "'", dbcon))
                {
                    message.Display("SELECT StartTimeLessons FROM Schedules where Clas = '" + Clas + "'", "Warn");
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var colums = new object[reader.FieldCount];
                            reader.GetValues(colums);
                            message.Display(string.Format("{0}", colums[0].ToString()), "Trace");
                            StartTimeLessons = TimeSpan.Parse(colums[0].ToString());
                            //break;
                        }
                    }
                }
            }
            return StartTimeLessons;
        }


        public int getEljurAccountIdByPupilIdOld(int PupilIdOld)
        {
            int EljurAccountId = 0;
            using (this.dbcon = new SqlConnection(config.ConStrMsDB))
            {
                dbcon.Open();
                using (SqlCommand command = new SqlCommand("SELECT EljurAccountId FROM Pupils where PupilIdOld = '" + PupilIdOld + "'", dbcon))
                {
                    message.Display("SELECT eljuraccountid FROM Pupils where PupilIdOld = '" + PupilIdOld + "'", "Warn");
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var colums = new object[reader.FieldCount];
                            reader.GetValues(colums);
                            message.Display(string.Format("{0}", colums[0].ToString()), "Trace");
                            EljurAccountId = Convert.ToInt32(colums[0]);
                            //break;
                        }
                    }
                }
            }
            return EljurAccountId;
        }


        public String getFullFIOByPupilIdOld(int PupilIdOld)
        {
            String FullFIO = "Default FullFIO";
            using (this.dbcon = new SqlConnection(config.ConStrMsDB))
            {
                dbcon.Open();
                using (SqlCommand command = new SqlCommand("SELECT FullFIO FROM Pupils WHERE PupilIdOld = '" + PupilIdOld + "'", dbcon))
                {
                    message.Display("SELECT FullFIO FROM Pupils WHERE PupilIdOld = '" + PupilIdOld + "'", "Warn");
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            message.Display(String.Format("{0}", reader[0]), "Trace");
                            FullFIO = reader[0].ToString();
                            break;
                        }
                    }
                }
            }
            return FullFIO;
        }


        public DateTime getModifyDate()
        {
            DateTime ModifyDate = new DateTime();
            using (this.dbcon = new SqlConnection(config.ConStrMsDB))
            {
                dbcon.Open();
                using (SqlCommand command = new SqlCommand("SELECT modify_date FROM sys.tables order by modify_date", dbcon))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        message.Display("SELECT modify_date FROM sys.tables order by modify_date", "Warn");
                        while (reader.Read())
                        {
                            message.Display(String.Format("{0}", reader[0]), "Trace");
                            ModifyDate = Convert.ToDateTime(reader[0].ToString());
                            break;
                        }
                    }
                }
            }
            return ModifyDate;
        }



    }
}
