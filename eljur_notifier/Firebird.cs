using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebirdSql.Data.FirebirdClient;

namespace Eljur
{
    class Firebird
    {
        public static void Do_something()
        {
            System.Console.WriteLine("HELLO");

            //string connectionString = 
            //    "User=SYSDBA;" +
            //    "Password=masterkey;" +
            //    "Database=D:/SMDK/DBase/SmarketFood.fdb;" +
            //    "DataSource=roman-book;" +
            //    "Port=3050;Dialect=3;" +
            //    "Charset=WIN1251;" +
            //    "Role=;Connection lifetime=30;" +
            //    "Pooling=true;" +
            //    "MinPoolSize=0;" +
            //    "MaxPoolSize=50;" +
            //    "Packet Size=8192;" +
            //    "ServerType=0;";
            //FbConnection con = new FbConnection(connectionString);




            System.Console.ReadKey();
        }
    }
}
