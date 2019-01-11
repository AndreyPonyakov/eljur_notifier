using System;
using eljur_notifier.AppCommonNS;
using System.Data.SqlClient;

namespace eljur_notifier.MsDbNS.CheckerNS
{
    public class EmptyChecker : EljurBaseClass
    {     
        public EmptyChecker() : base(new Message(), new Config(), new SqlConnection()) { }
 
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
