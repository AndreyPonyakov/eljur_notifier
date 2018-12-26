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
    class MsDb : DbCommonClass
    {
        internal protected Message message { get; set; }
        internal protected String ConnectStr { get; set; }
        internal protected SqlConnection dbcon { get; set; }
        internal protected StaffContext StaffCtx { get; set; }


        public MsDb(String ConnectStr)
        {
            this.message = new Message();
            this.ConnectStr = ConnectStr;
            SqlConnection.ClearAllPools();
            this.dbcon = new SqlConnection(ConnectStr);
            this.createDb(ConnectStr);
            while (this.IsDbExist(dbcon) == false) { }
        }


        public void createDb(String conStr)
        {
            using (this.StaffCtx = new StaffContext())
            {
                Pupil firstStudent = new Pupil();
                firstStudent.PupilId = 1;
                firstStudent.PupilIdOld = 5001;
                firstStudent.FirstName = "Иван";
                firstStudent.LastName = "Иванов";
                firstStudent.MiddleName = "Иванович";
                firstStudent.FullFIO = "Иван Иванов Иванович";
                firstStudent.Clas = "1Б";
                firstStudent.EljurAccountId = 666;

                Event firstEvent = new Event();
                firstEvent.EventId = 1;
                firstEvent.PupilIdOld = 5001;
                firstEvent.EventTime = DateTime.Now.TimeOfDay;
                firstEvent.EventName = "Прогул";
                firstEvent.NotifyEnable = true;
                firstEvent.NotifyEnableDirector = true;
                firstEvent.NotifyWasSend = false;
                firstEvent.NotifyWasSendDirector = false;


                Schedule firstScheduleToDayItem = new Schedule();
                firstScheduleToDayItem.ScheduleId = 1;
                firstScheduleToDayItem.Clas = "1A";
                firstScheduleToDayItem.StartTimeLessons = DateTime.Now.TimeOfDay;
                firstScheduleToDayItem.EndTimeLessons = DateTime.Now.TimeOfDay;


                StaffCtx.Pupils.Add(firstStudent);
                StaffCtx.Events.Add(firstEvent);
                StaffCtx.Schedules.Add(firstScheduleToDayItem);
                StaffCtx.SaveChanges();
                message.Display("firstStudent success saved", "Warn");

                var students = StaffCtx.Pupils;
                var evets = StaffCtx.Events;
                var schedules = StaffCtx.Schedules;
                Console.WriteLine("List of objects:");
                foreach (Pupil p in students)
                {
                    Console.WriteLine("{0}.{1} - {2} - {3} - {4} - {5}", p.PupilIdOld, p.FirstName, p.LastName, p.MiddleName, p.FullFIO, p.Clas);
                }
                foreach (Event e in evets)
                {
                    Console.WriteLine("{0}.{1} - {2} - {3} - {4} - {5} - {6}- {7}", e.EventId, e.PupilIdOld, e.EventTime, e.EventName, e.NotifyEnable, e.NotifyEnableDirector, e.NotifyWasSend, e.NotifyWasSendDirector);
                }
                foreach (Schedule s in schedules)
                {
                    Console.WriteLine("{0}.{1} - {2} - {3}", s.ScheduleId, s.Clas, s.StartTimeLessons, s.EndTimeLessons);
                }
                this.StaffCtx.Database.ExecuteSqlCommand("TRUNCATE TABLE [Pupils]");
                message.Display("TABLE Pupils was cleared", "Warn");
                this.StaffCtx.Database.ExecuteSqlCommand("TRUNCATE TABLE [Events]");
                message.Display("TABLE Events was cleared", "Warn");
                this.StaffCtx.Database.ExecuteSqlCommand("TRUNCATE TABLE [Schedules]");
                message.Display("TABLE Schedules was cleared", "Warn");

            }
        }

        public void FillStaffDb(List<object[]> AllStaff)
        {
            using (this.StaffCtx = new StaffContext())
            {
                EljurApiRequester elRequester = new EljurApiRequester();
                foreach (object[] row in AllStaff)
                {

                    Pupil Student = new Pupil();
                    Student.PupilIdOld = Convert.ToInt32(row[0]);
                    Student.FirstName = row[2].ToString();
                    Student.LastName = row[1].ToString();
                    Student.MiddleName = row[3].ToString();
                    Student.FullFIO = row[22].ToString();

                    elRequester.clas = elRequester.getClasByFullFIO(Student.FullFIO);
                    Student.Clas = elRequester.clas;

                    elRequester.eljurAccountId = elRequester.getEljurAccountIdByFullFIO(Student.FullFIO);
                    Student.EljurAccountId = elRequester.eljurAccountId;

                    StaffCtx.Pupils.Add(Student);
                    StaffCtx.SaveChanges();
                    message.Display("Student success saved", "Warn");

                }
                var students = StaffCtx.Pupils;
                Console.WriteLine("List of objects:");
                foreach (Pupil p in students)
                {
                    Console.WriteLine("{0}.{1} - {2} - {3} - {4} - {5}", p.PupilId, p.FirstName, p.LastName, p.MiddleName, p.FullFIO, p.Clas);
                }

            }
        }


        public void CheckEventsDb(List<object[]> curEvents)
        {
            using (this.StaffCtx = new StaffContext())
            {
                EljurApiRequester elRequester = new EljurApiRequester();
                foreach (object[] row in curEvents)
                {
                    if (row[1] == DBNull.Value)
                    {
                        continue;
                    }
                    var PupilIdOld = Convert.ToInt32(row[1]);
                    message.Display("Событие проход школьника с id " + PupilIdOld.ToString() + " в " + row[0].ToString(), "Trace");


                    var result = StaffCtx.Events.SingleOrDefault(e => e.PupilIdOld == PupilIdOld);
                    if (result != null)
                    {
                        if (result.EventName == "Первый проход" || result.EventName == "Вернулся" || result.EventName == "Опоздал")
                        {
                            //register only output configs_tree_id_resource
                            if (row[3].ToString() == "8564" || row[3].ToString() == "9369")
                            {
                                result.EventName = "Вышел";
                                result.EventTime = TimeSpan.Parse(row[0].ToString());


                                String Clas = getClasByPupilIdOld(PupilIdOld);
                                //String FullFIO = getFullFIOByPupilIdOld(PupilIdOld);

                                TimeSpan StartTimeLessons = elRequester.getStartTimeLessonsByClas(Clas);
                                TimeSpan EndTimeLessons = elRequester.getEndTimeLessonsByClas(Clas);


                                StaffCtx.SaveChanges();
                                message.Display("Школьник с id " + PupilIdOld + " вышел из школы в " + row[0].ToString(), "Trace");
                            }

                        }
                        else if (result.EventName == "Вышел" || result.EventName == "Прогул")
                        {
                            if (row[3].ToString() == "8677" || row[3].ToString() == "9256")
                            {
                                //register only input configs_tree_id_resource
                                result.EventName = "Вернулся";
                                result.EventTime = TimeSpan.Parse(row[0].ToString());
                                StaffCtx.SaveChanges();
                                message.Display("Школьник с id " + PupilIdOld + "  вернулся в школу в " + row[0].ToString(), "Trace");
                            }
                        }
                    }
                    else
                    {
                        //register only input configs_tree_id_resource
                        if (row[3].ToString() == "8677" || row[3].ToString() == "9256")
                        {
                            Event Event = new Event();
                            Event.PupilIdOld = PupilIdOld;
                            Event.EventTime = TimeSpan.Parse(row[0].ToString());
                            Event.EventName = "Первый проход";
                            StaffCtx.Events.Add(Event);
                            StaffCtx.SaveChanges();
                            message.Display("Школьник с id " + PupilIdOld + "  пришёл в школу в " + row[0].ToString(), "Trace");
                        }
                    }

                }

            }

        }

        public void FillSchedulesDb(List<object[]> curEvents)
        {
            using (this.StaffCtx = new StaffContext())
            {
                EljurApiRequester elRequester = new EljurApiRequester();
                String[] ClasesArr = elRequester.getClases();
                foreach (String clas in ClasesArr)
                {
                    Schedule ScheduleItem = new Schedule();
                    ScheduleItem.Clas = clas;
                    ScheduleItem.StartTimeLessons = elRequester.getStartTimeLessonsByClas(clas);
                    ScheduleItem.EndTimeLessons = elRequester.getEndTimeLessonsByClas(clas);
                    StaffCtx.Schedules.Add(ScheduleItem);
                    StaffCtx.SaveChanges();
                    message.Display("ScheduleItem success saved", "Warn");
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
            using (this.StaffCtx = new StaffContext())
            {
                this.StaffCtx.Database.ExecuteSqlCommand("TRUNCATE TABLE [" + TableName + "]");
            }

        }

        public DateTime getModifyDate()
        {
            DateTime ModifyDate = new DateTime();
            dbcon.Open();
            SqlCommand command = new SqlCommand("SELECT modify_date FROM sys.tables order by modify_date", dbcon);
            SqlDataReader reader = command.ExecuteReader();
            message.Display("SELECT modify_date FROM sys.tables order by modify_date", "Warn");
            while (reader.Read())
            {
                message.Display(String.Format("{0}", reader[0]), "Trace");
                ModifyDate = Convert.ToDateTime(reader[0].ToString());
                break;
            }
            dbcon.Close();
            return ModifyDate;
        }


        public String getFullFIOByPupilIdOld(int PupilIdOld)
        {
            String FullFIO = "Default FullFIO";
            dbcon.Open();
            SqlCommand command = new SqlCommand("SELECT FullFIO FROM Pupils WHERE PupilIdOld = '" + PupilIdOld + "'", dbcon);
            SqlDataReader reader = command.ExecuteReader();
            message.Display("SELECT FullFIO FROM Pupils WHERE PupilIdOld = '" + PupilIdOld + "'", "Warn");
            while (reader.Read())
            {
                message.Display(String.Format("{0}", reader[0]), "Trace");
                FullFIO = reader[0].ToString();
                break;
            }
            dbcon.Close();
            return FullFIO;
        }

        public String getClasByPupilIdOld(int PupilIdOld)
        {
            String Clas = "Default Clas";
            dbcon.Open();
            SqlCommand command = new SqlCommand("SELECT Clas FROM Pupils WHERE PupilIdOld = '" + PupilIdOld + "'", dbcon);
            SqlDataReader reader = command.ExecuteReader();
            message.Display("SELECT Clas FROM Pupils WHERE PupilIdOld = '" + PupilIdOld + "'", "Warn");
            while (reader.Read())
            {
                message.Display(String.Format("{0}", reader[0]), "Trace");
                Clas = reader[0].ToString();
                break;
            }
            dbcon.Close();
            return Clas;
        }




    }
}
