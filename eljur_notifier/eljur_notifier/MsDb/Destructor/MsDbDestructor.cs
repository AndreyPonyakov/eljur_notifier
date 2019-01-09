using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace eljur_notifier.MsDbNS.DestructorNS
{
    public class MsDbDestructor
    {
        public MsDbDestructor()
        {

        }

        public void deleteDb(String conStr)
        {

            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();
                String sqlCommandTextSingleUser = @"
                ALTER DATABASE " + "StaffDb" + @" SET SINGLE_USER WITH ROLLBACK IMMEDIATE;";
                using (SqlCommand sqlCommandSingleUser = new SqlCommand(sqlCommandTextSingleUser, con))
                {
                    sqlCommandSingleUser.ExecuteNonQuery();
                }
                String sqlCommandTextDeleteDB = @"
                USE Master;
                DROP DATABASE [" + "StaffDb" + "]";
                using (SqlCommand sqlCommandDeleteDB = new SqlCommand(sqlCommandTextDeleteDB, con))
                {
                    sqlCommandDeleteDB.ExecuteNonQuery();
                }
            }
        }

    }
}
