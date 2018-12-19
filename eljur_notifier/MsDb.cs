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

        public static void createDb(String conStr)
        {
            using (StaffContext context = new StaffContext())
            {
                Console.WriteLine("Inside using");

                Pupil firstStudent = new Pupil();
                firstStudent.PupilId = 1;
                firstStudent.FirstName = "Иван";
                firstStudent.LastName = "Иванов";
                firstStudent.MiddleName = "Иванович";
                firstStudent.FullFIO = "Иван Иванов Иванович";
                firstStudent.Class = "1Б";
                firstStudent.Event = "Прогул";
                firstStudent.EljurAccount = "some_string";
                firstStudent.NotifyEnable = true;
                firstStudent.NotifyEnableDirector = true;
                firstStudent.NotifyWasSend = false;
                firstStudent.NotifyWasSendDirector = false;


                context.Pupils.Add(firstStudent);
                context.SaveChanges();
                Console.WriteLine("firstStudent success saved");

                var students = context.Pupils;
                Console.WriteLine("List of objects:");
                foreach (Pupil p in students)
                {
                    Console.WriteLine("{0}.{1} - {2}", p.PupilId, p.FirstName, p.LastName, p.MiddleName, p.FullFIO, p.Class);
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







    }    
}
