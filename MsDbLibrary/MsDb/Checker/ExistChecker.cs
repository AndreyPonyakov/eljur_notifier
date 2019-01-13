using System;
using System.Data.SqlClient;
using System.Configuration;
using MsDbLibraryNS;

namespace MsDbLibraryNS.MsDbNS.CheckerNS
{
    public class ExistChecker : MsDbBaseClass
    {
        internal protected String nameOfConnectionString { get; set; }

        public ExistChecker(String NameOfConnectionString = "StaffContext") 
            : base(new Message(), new SqlConnection()) {
            this.nameOfConnectionString = NameOfConnectionString;
        }
  
        public Boolean IsTableExist(String TableName)
        {
            var ConStrMsDBvar = ConfigurationManager.ConnectionStrings[nameOfConnectionString].ToString();
            using (this.dbcon = new SqlConnection(ConStrMsDBvar))
            {
                try
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
                catch
                {
                    return false;
                }
            }
        }





    }
}
