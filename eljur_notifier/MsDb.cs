using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using eljur_notifier;

namespace eljur_notifier
{
    class MsDb
    {
        internal protected IDbConnection db { get; set; }
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



    }    
}
