using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using eljur_notifier;
using eljur_notifier.StaffModel;
using eljur_notifier.DbCommon;
using eljur_notifier.AppCommon;

namespace eljur_notifier.MsDbNS
{
    class MsDb: DbCommonClass
    {
        internal protected Message message { get; set; }
        internal protected String ConnectStr { get; set; }
        //internal protected IDbConnection dbcon { get; set; }
        internal protected SqlConnection dbcon { get; set; }
        internal protected Boolean IsDbExistVar { get; set; }
        internal protected StaffContext StaffCtx { get; set; }
        

        public MsDb(String ConnectStr)
        {
            this.message = new Message();
            this.ConnectStr = ConnectStr;
            SqlConnection.ClearAllPools();
            this.dbcon = new SqlConnection(ConnectStr);
            this.IsDbExistVar = this.IsDbExist(dbcon);
            while (this.IsDbExist(dbcon) == false)
            {
                this.createDb(ConnectStr);
            }
        }


        public void createDb(String conStr)
        {
            //using (StaffContext context = new StaffContext())
            using ( this.StaffCtx = new StaffContext())
            {             
                Pupil firstStudent = new Pupil();
                firstStudent.PupilId = 1;
                firstStudent.PupilIdOld = 5001;
                firstStudent.FirstName = "Иван";
                firstStudent.LastName = "Иванов";
                firstStudent.MiddleName = "Иванович";
                firstStudent.FullFIO = "Иван Иванов Иванович";
                firstStudent.Class = "1Б";               
                firstStudent.EljurAccount = "some_string";
                
                Event firstEvent = new Event();
                firstEvent.EventId = 1;
                firstEvent.PupilId = 1;
                firstEvent.EventTime = DateTime.Now.TimeOfDay;
                firstEvent.EventName = "Прогул";
                firstEvent.NotifyEnable = true;
                firstEvent.NotifyEnableDirector = true;
                firstEvent.NotifyWasSend = false;
                firstEvent.NotifyWasSendDirector = false;


                StaffCtx.Pupils.Add(firstStudent);
                StaffCtx.Events.Add(firstEvent);
                StaffCtx.SaveChanges();
                Console.WriteLine("firstStudent success saved");

                var students = StaffCtx.Pupils;
                var evets = StaffCtx.Events;
                Console.WriteLine("List of objects:");
                foreach (Pupil p in students)
                {
                    Console.WriteLine("{0}.{1} - {2} - {3} - {4} - {5}", p.PupilIdOld, p.FirstName, p.LastName, p.MiddleName, p.FullFIO, p.Class);
                }
                foreach (Event e in evets)
                {
                    Console.WriteLine("{0}.{1} - {2} - {3} - {4} - {5} - {6}- {7}", e.EventId, e.PupilId, e.EventTime, e.EventName, e.NotifyEnable, e.NotifyEnableDirector, e.NotifyWasSend, e.NotifyWasSendDirector);
                }
                this.StaffCtx.Database.ExecuteSqlCommand("TRUNCATE TABLE [Pupils]");
                this.StaffCtx.Database.ExecuteSqlCommand("TRUNCATE TABLE [Events]");

            }     
        }

        public void FillStaffDb(List<object[]> AllStaff)
        {
            using (this.StaffCtx = new StaffContext())
            {
                foreach (object[] row in AllStaff)
                {
                    //var columns = row.Count();
                    //Console.WriteLine(columns);
                    //Console.WriteLine("PupilId: " + row[0].ToString());

                    Pupil Student = new Pupil();
                    Student.PupilIdOld = Convert.ToInt32(row[0]);
                    Student.FirstName = row[2].ToString();
                    Student.LastName = row[1].ToString();
                    Student.MiddleName = row[3].ToString();
                    Student.FullFIO = row[22].ToString();
                    //Student.Class = row[21].ToString();
                    //Student.EljurAccount = row[21].ToString();
                    StaffCtx.Pupils.Add(Student);
                    StaffCtx.SaveChanges();
                    Console.WriteLine("Student success saved");
                    //break;
                }
                var students = StaffCtx.Pupils;
                Console.WriteLine("List of objects:");
                foreach (Pupil p in students)
                {
                    Console.WriteLine("{0}.{1} - {2} - {3} - {4} - {5}", p.PupilId, p.FirstName, p.LastName, p.MiddleName, p.FullFIO, p.Class);
                }

            }
        }


        public void CheckEventsDb(List<object[]> curEvents)
        {
            using (this.StaffCtx = new StaffContext())
            {
                foreach (object[] row in curEvents)
                {                  
                    if (row[1] == DBNull.Value)
                    {
                        continue;
                    }
                    var PupilId = Convert.ToInt32(row[1]);//PipilIdOld
                    Console.WriteLine("Событие проход школьника с id " + PupilId.ToString() + " в " + row[0].ToString());


                    var result = StaffCtx.Events.SingleOrDefault(e => e.PupilId == PupilId);
                    if (result != null)
                    {
                        if (result.EventName == "Первый проход" || result.EventName == "Вернулся")
                        {
                            result.EventName = "Вышел";
                            //result.EventTime = Convert.ToDateTime(row[0]).TimeOfDay;
                            result.EventTime = TimeSpan.Parse(row[0].ToString());
                            StaffCtx.SaveChanges();
                            Console.WriteLine("Школьник с id " + PupilId + " вышел из школы в " + row[0].ToString());
                        }
                        else if (result.EventName == "Вышел" || result.EventName == "Прогул")
                        {
                            result.EventName = "Вернулся";
                            //result.EventTime = Convert.ToDateTime(row[0]).TimeOfDay;
                            result.EventTime = TimeSpan.Parse(row[0].ToString());
                            StaffCtx.SaveChanges();
                            Console.WriteLine("Школьник с id " + PupilId + "  вернулся в школу в " + row[0].ToString());
                        }
                    }
                    else
                    {
                        Event Event = new Event();
                        Event.PupilId = PupilId;
                        //Event.EventTime = Convert.ToDateTime(row[0]).TimeOfDay;
                        Event.EventTime = TimeSpan.Parse(row[0].ToString());
                        Event.EventName = "Первый проход";
                        StaffCtx.Events.Add(Event);
                        StaffCtx.SaveChanges();
                        Console.WriteLine("Школьник с id " + PupilId + "  пришёл в школу в " + row[0].ToString());
                    }

                }

            }

        }

        public void deleteDb(String conStr)
        {

            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();
                String sqlCommandTextSingleUser = @"
                ALTER DATABASE " + "StaffDb" + @" SET SINGLE_USER WITH ROLLBACK IMMEDIATE;";
                SqlCommand sqlCommandSingleUser = new SqlCommand(sqlCommandTextSingleUser, con);
                sqlCommandSingleUser.ExecuteNonQuery();

                String sqlCommandTextDeleteDB = @"
                USE Master;
                DROP DATABASE [" + "StaffDb" + "]";
                SqlCommand sqlCommandDeleteDB = new SqlCommand(sqlCommandTextDeleteDB, con);
                sqlCommandDeleteDB.ExecuteNonQuery();
            }
        }

        public void clearTableDb(String TableName)
        {
            //this.StaffCtx.Database.ExecuteSqlCommand("TRUNCATE TABLE [" + TableName + "]");
        }

        public DateTime getCreationDate()
        {
            message.Display("dbcon.Open(); ", "Warn");
            DateTime CreationDate = new DateTime();
            message.Display("dbcon.Open(); ", "Warn");
            dbcon.Open();
            message.Display("AFTER dbcon.Open(); ", "Warn");
            SqlCommand command = new SqlCommand("SELECT create_date FROM sys.tables order by create_date", dbcon);
            SqlDataReader reader = command.ExecuteReader();
            Console.WriteLine("SELECT create_date FROM sys.tables order by create_date");
            while (reader.Read())
            {
                Console.WriteLine(String.Format("{0}", reader[0]));
                CreationDate = Convert.ToDateTime(reader[0].ToString());
                break;
            }
            dbcon.Close();
            return CreationDate;
        }











    }    
}
