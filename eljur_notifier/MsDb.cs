using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using eljur_notifier;
using eljur_notifier.StaffModel;

namespace eljur_notifier
{
    class MsDb
    {
        internal protected IDbConnection dbcon { get; set; }
        internal protected String ConnectStr { get; set; }
        internal protected Boolean IsDbExistVar { get; set; }
        internal protected StaffContext StaffCtx { get; set; }

        public MsDb()
        {

        }
        public static Boolean IsDbExist(String conStr)
        {
            IDbConnection db = new SqlConnection(conStr);
            try
            {
                db.Open();
                db.Close();
                return true;
            }
            catch (SqlException e)
            {
                // Cannot connect to database
                Console.WriteLine("Cannot connect to database");
                return false;
            }
        }

        public static IDbConnection getConnection(String conStr)
        {
            IDbConnection dbcon = new SqlConnection(conStr);
            return dbcon;
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
                    var PupilId = Convert.ToInt32(row[1]);//PipilIdOld



                    var result = StaffCtx.Events.SingleOrDefault(e => e.PupilId == PupilId);
                    if (result != null)
                    {
                        if (result.EventName == "Первый проход" || result.EventName == "Вернулся")
                        {
                            result.EventName = "Вышел";
                            result.EventTime = Convert.ToDateTime(row[0]).TimeOfDay;
                            StaffCtx.SaveChanges();
                            Console.WriteLine("Школьник с id " + PupilId + " вышел из школы в " + row[0].ToString());
                        }
                        else if (result.EventName == "Вышел" || result.EventName == "Прогул")
                        {
                            result.EventName = "Вернулся";
                            result.EventTime = Convert.ToDateTime(row[0]).TimeOfDay;
                            StaffCtx.SaveChanges();
                            Console.WriteLine("Школьник с id " + PupilId + "  вернулся в школу в " + row[0].ToString());

                        }
                    }
                    else
                    {
                        result.EventName = "Первый проход";
                        result.EventTime = Convert.ToDateTime(row[0]).TimeOfDay;
                        StaffCtx.SaveChanges();
                        Console.WriteLine("Школьник с id " + PupilId + "  пришёл в школу в " + row[0].ToString());
                    }



                    //var students = StaffCtx.Pupils;
                    //foreach (Pupil p in students)
                    //{
                    //    Console.WriteLine("TEST{0}.{1} - {2} - {3} - {4} - {5}", p.PupilIdOld, p.FirstName, p.LastName, p.MiddleName, p.FullFIO, p.Class);
                    //}




                    //var evets = StaffCtx.Events;
                    //foreach (Event e in evets)
                    //{
                    //    if (e.PupilId == PupilId)
                    //    {
                    //        if (e.EventName == "Первый проход")
                    //        {
                    //            e.EventName = 
                    //            StaffCtx.SaveChanges();
                    //        }                                         
                    //    }                    
                    //    Console.WriteLine("{0}.{1} - {2} - {3} - {4} - {5} - {6}", e.EventId, e.PupilId, e.EventName, e.NotifyEnable, e.NotifyEnableDirector, e.NotifyWasSend, e.NotifyWasSendDirector);
                    //}




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


        







    }    
}
