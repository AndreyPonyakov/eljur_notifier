﻿using System;
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
using eljur_notifier.EljurNS;



namespace eljur_notifier.MsDbNS
{
    class MsDb : DbCommonClass
    {
        internal protected Message message { get; set; }
        internal protected Config config { get; set; }
        internal protected String ConnectStr { get; set; }
        internal protected SqlConnection dbcon { get; set; }
        internal protected StaffContext StaffCtx { get; set; }


        public MsDb(Config Config)
        {
            this.config = Config;
            this.ConnectStr = config.ConStrMsDB;
            this.message = new Message();
            using (this.dbcon = new SqlConnection(ConnectStr))
            {
                if (IsTableExist("Pupils"))
                {
                    message.Display("msDb already exist", "Warn");
                }
                else
                { 
                    this.createCleanMsDb(config.ConStrMsDB);
                }
            }
        }


        public void createCleanMsDb(String conStr)
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
                EljurApiRequester elRequester = new EljurApiRequester(this);
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

        public void UpdateSchedulesDb()
        {
            using (this.StaffCtx = new StaffContext())
            {
                EljurApiRequester elRequester = new EljurApiRequester(this);
                var Clases = elRequester.getClases();
                foreach (String clas in Clases)
                {

                    var ScheduleItem = StaffCtx.Schedules.SingleOrDefault(e => e.Clas == clas);
                    if (ScheduleItem != null)
                    {
                        TimeSpan StartTimeLessons = elRequester.getStartTimeLessonsByClas(clas);
                        ScheduleItem.StartTimeLessons = StartTimeLessons;
                        TimeSpan EndTimeLessons = elRequester.getEndTimeLessonsByClas(clas);
                        ScheduleItem.EndTimeLessons = EndTimeLessons;
                    }

                    StaffCtx.SaveChanges();
                    message.Display("ScheduleItem success saved", "Warn");
                }
                var Schedules = StaffCtx.Schedules;
                message.Display("List of objects: ", "Warn");
                foreach (Schedule s in Schedules)
                {
                    message.Display(s.ScheduleId +"."+ s.Clas +"-"+ s.StartTimeLessons + "-" + s.EndTimeLessons, "Trace");
                }

            }
        }



        public void CheckEventsDb(List<object[]> curEvents)
        {
            using (this.StaffCtx = new StaffContext())
            {
                EljurApiRequester elRequester = new EljurApiRequester(this);
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
                            //register only OUTPUT configs_tree_id_resource
                            if (row[3].ToString() == config.ConfigsTreeIdResourceOutput1.ToString() || row[3].ToString() == config.ConfigsTreeIdResourceOutput2.ToString())
                            {
                                result.EventName = "Вышел";
                                result.EventTime = TimeSpan.Parse(row[0].ToString());
                                result.NotifyWasSend = false;
                                StaffCtx.SaveChanges();
                                message.Display("Школьник с id " + PupilIdOld + " вышел из школы в " + row[0].ToString(), "Trace");
                            }

                        }
                        else if (result.EventName == "Вышел" || result.EventName == "Прогул")
                        {
                            //register only INPUT configs_tree_id_resource
                            if (row[3].ToString() == config.ConfigsTreeIdResourceInput1.ToString() || row[3].ToString() == config.ConfigsTreeIdResourceInput2.ToString())
                            {                   
                                result.EventName = "Вернулся";
                                result.EventTime = TimeSpan.Parse(row[0].ToString());
                                result.NotifyWasSend = false;
                                StaffCtx.SaveChanges();
                                message.Display("Школьник с id " + PupilIdOld + "  вернулся в школу в " + row[0].ToString(), "Trace");
                            }
                        }
                    }
                    else
                    {
                        //register only INPUT configs_tree_id_resource
                        if (row[3].ToString() == config.ConfigsTreeIdResourceInput1.ToString() || row[3].ToString() == config.ConfigsTreeIdResourceInput2.ToString())
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

        public void FillSchedulesDb()
        {
            using (this.StaffCtx = new StaffContext())
            {
                EljurApiRequester elRequester = new EljurApiRequester(this);
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

        public void SetStatusNotifyWasSend(int PupilIdOld)
        {
            using (this.StaffCtx = new StaffContext())
            {
                var result = StaffCtx.Events.SingleOrDefault(e => e.PupilIdOld == PupilIdOld);
                if (result != null)
                {
                    result.NotifyWasSend = true;
                    StaffCtx.SaveChanges();
                    message.Display("Status NotifyWasSend to " + PupilIdOld + " PupilIdOld was set", "Trace");
                }
            }
        }

        public void SetStatusCameTooLate(int PupilIdOld)
        {
            using (this.StaffCtx = new StaffContext())
            {
                var result = StaffCtx.Events.SingleOrDefault(e => e.PupilIdOld == PupilIdOld);
                if (result != null)
                {
                    result.EventName = "Опоздал";
                    StaffCtx.SaveChanges();
                    message.Display("CameTooLate to " + PupilIdOld + " PupilIdOld was set", "Trace");
                }
            }
        }

        public void SetStatusWentHomeTooEarly(int PupilIdOld)
        {
            using (this.StaffCtx = new StaffContext())
            {
                var result = StaffCtx.Events.SingleOrDefault(e => e.PupilIdOld == PupilIdOld);
                if (result != null)
                {
                    result.EventName = "Прогул";
                    StaffCtx.SaveChanges();
                    message.Display("WentHomeTooEarly to " + PupilIdOld + " PupilIdOld was set", "Trace");
                }
            }
        }


        public void SetClasByPupilIdOld(int PupilIdOld, String Clas)
        {
            using (this.StaffCtx = new StaffContext())
            {
                var result = StaffCtx.Pupils.SingleOrDefault(e => e.PupilIdOld == PupilIdOld);
                if (result != null)
                {
                    result.Clas = Clas;
                    StaffCtx.SaveChanges();
                    message.Display("Clas to " + PupilIdOld + " PupilIdOld was updated", "Info");
                }
            }
        }



        public Boolean IsTableExist(String TableName)
        {
            using (this.dbcon = new SqlConnection(config.ConStrMsDB))
            {
                dbcon.Open();
                using (SqlCommand sqlCommand = new SqlCommand("SELECT 'TableExist' FROM (SELECT name FROM sys.tables UNION SELECT name FROM sys.views) T WHERE name = @Name", dbcon))
                {
                    sqlCommand.Parameters.AddWithValue("@name", TableName);
                    if (sqlCommand.ExecuteScalar().ToString() == "TableExist")
                    {
                        message.Display("TableExist " + TableName + " in msDb", "Warn");
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public Boolean IsTableEmpty(String TableName)
        {
            using (this.dbcon = new SqlConnection(config.ConStrMsDB))
            {
                dbcon.Open();
                using (SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM " + TableName, dbcon))
                {
                    int result = int.Parse(sqlCommand.ExecuteScalar().ToString());
                    if (result == 0)
                    {
                        message.Display("Table " + TableName + " is EMPTY in msDb", "Warn");
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

    }
}
