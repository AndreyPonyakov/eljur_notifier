using System;
using eljur_notifier.AppCommonNS;
using System.Data.SqlClient;
using System.Configuration;

namespace eljur_notifier.MsDbNS.CheckerNS
{
    public class EmptyChecker : EljurBaseClass
    {
        internal protected String nameOfConnectionString { get; set; }

        public EmptyChecker(String NameOfConnectionString = "StaffContext") 
            : base(new Message(), new SqlConnection()) {
            this.nameOfConnectionString = NameOfConnectionString;
        }
 
        public Boolean IsTableEmpty(String TableName)
        {
            var ConStrMsDBvar = ConfigurationManager.ConnectionStrings[nameOfConnectionString].ToString();
            
            using (this.dbcon = new SqlConnection(ConStrMsDBvar))
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
