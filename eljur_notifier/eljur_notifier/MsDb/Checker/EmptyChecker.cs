using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eljur_notifier.AppCommon;
using System.Data.SqlClient;

namespace eljur_notifier.MsDbNS.CheckerNS
{
    public class EmptyChecker
    {
        internal protected Message message { get; set; }
        internal protected Config config { get; set; }
        internal protected SqlConnection dbcon { get; set; }

        public EmptyChecker(Config Config)
        {
            this.message = new Message();
            this.config = Config;
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
