using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using eljur_notifier.AppCommon;

namespace eljur_notifier.MsDbNS.CheckerNS
{
    public class ExistChecker
    {
        internal protected Message message { get; set; }
        internal protected Config config { get; set; }
        internal protected SqlConnection dbcon { get; set; }


        public ExistChecker(Config Config)
        {
            this.message = new Message();
            this.config = Config;

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





    }
}
