using System;
using System.Data.SqlClient;
using System.Configuration;

namespace MsDbLibraryNS.MsDbNS.DestructorNS
{
    public class MsDbDestructor : MsDbBaseClass
    {
        internal protected String nameOfConnectionString { get; set; }

        public MsDbDestructor(String NameOfConnectionString = "StaffContext")
            : base(new Message(), new SqlConnection()){
            this.nameOfConnectionString = NameOfConnectionString;
        }

        public void deleteDb(String nameOfConnectionString)
        {
            var ConStrMsDBvar = ConfigurationManager.ConnectionStrings[nameOfConnectionString].ToString();
            String DbName = "StaffDbTests";
            if (nameOfConnectionString == "StaffContext")
            {
                DbName = "StaffDb";
            }
            using (SqlConnection con = new SqlConnection(ConStrMsDBvar))
            {              
                con.Open();
                String sqlCommandTextSingleUser = @"
                ALTER DATABASE " + DbName + @" SET SINGLE_USER WITH ROLLBACK IMMEDIATE;";
                using (SqlCommand sqlCommandSingleUser = new SqlCommand(sqlCommandTextSingleUser, con))
                {
                    sqlCommandSingleUser.ExecuteNonQuery();
                }
                String sqlCommandTextDeleteDB = @"
                USE Master;
                DROP DATABASE [" + DbName + "]";
                using (SqlCommand sqlCommandDeleteDB = new SqlCommand(sqlCommandTextDeleteDB, con))
                {
                    sqlCommandDeleteDB.ExecuteNonQuery();
                }
            }
        }

    }
}
