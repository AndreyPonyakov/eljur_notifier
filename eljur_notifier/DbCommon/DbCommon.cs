using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eljur_notifier.DbCommon
{
    class DbCommonClass : IDbCommonInf
    {
        public Boolean IsDbExist(IDbConnection db)
        {
            try
            {
                db.Open();
                db.Close();
                return true;
            }
            catch (Exception e)
            {
                // Cannot connect to database
                Console.WriteLine("Cannot connect to database");
                return false;
            }
        }
    }
}
