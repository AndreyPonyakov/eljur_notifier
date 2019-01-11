using System;
using System.Data.SqlClient;
using eljur_notifier.AppCommonNS;

namespace eljur_notifier.MsDbNS.CheckerNS
{
    public class ExistChecker : EljurBaseClass
    {
        public ExistChecker() : base(new Message(), new Config(), new SqlConnection()) { }
  
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
