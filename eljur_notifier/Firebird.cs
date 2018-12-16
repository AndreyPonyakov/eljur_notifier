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

            string connectionString =
                "User=SYSDBA;" +
                "Password=masterkey;" +
                "Database=D:/School/Backup_PERCo_04.10.18/Backup_PERCo_04.10.18/4.10.18_9.23/NEWBASE.FDB;" +
                //"DataSource=roman-book;" +
                "Port=3050;" +
                "Dialect=3;" +
                "Charset=WIN1251;" +
                "Role=;" +
                "Connection lifetime=30;" +
                "Pooling=true;" +
                "MinPoolSize=0;" +
                "MaxPoolSize=50;" +
                "Packet Size=8192;" +
                "ServerType=0;";
            FbConnection fbcon = new FbConnection(connectionString);

            //FbConnectionStringBuilder fb_con = new FbConnectionStringBuilder();
            //fb_con.Charset = "UTF-8";
            //fb_con.UserID = "sysdba";
            //fb_con.Password = "masterkey";
            ////fb_con.Database = "D:/School/Backup_PERCo_04.10.18/Backup_PERCo_04.10.18/4.10.18_9.23/NEWBASE.FDB";
            //fb_con.Database = @"D:\School\Backup_PERCo_04.10.18\Backup_PERCo_04.10.18\4.10.18_9.23\NEWBASE.FDB";
            //fb_con.ServerType = 0;

            //FbConnection fb = new FbConnection(fb_con.ToString());

            fbcon.Open();

            FbDatabaseInfo fb_inf = new FbDatabaseInfo(fbcon);

            System.Console.WriteLine("Info: " + fb_inf.ServerClass + "; " + fb_inf.ServerVersion);






            System.Console.ReadKey();
        }
    }
}
