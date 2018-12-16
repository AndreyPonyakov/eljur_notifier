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
        public static void DoSomething()
        {
            System.Console.WriteLine("HELLO");

            string connectionString =
                "User=SYSDBA;" +
                "Password=masterkey;" +
                "Database=D:/School/Backup_PERCo_04.10.18/Backup_PERCo_04.10.18/4.10.18_9.23/NEWBASE.FDB;" +
                "Port=3050;" +
                "Dialect=3;" +
                "Charset=WIN1251;" +
                "Role=;" +
                "Connection lifetime=30;" +
                "Pooling=true;" +
                "MinPoolSize=0;" +
                "MaxPoolSize=50;" +
                "Packet Size=8192;" +
                "ServerType=0;";//указываем тип сервера (0 - "полноценный Firebird" (classic или super server), 1 - встроенный (embedded))
            FbConnection fbcon = new FbConnection(connectionString);

            fbcon.Open();

            FbDatabaseInfo fb_inf = new FbDatabaseInfo(fbcon);

            System.Console.WriteLine("Info: " + fb_inf.ServerClass + "; " + fb_inf.ServerVersion);

            //String sSQL = "select * from STAFF";


            //FbCommand fbcmd = new FirebirdSql.Data.FirebirdClient.FbCommand(sSQL, fbcon);

            FbCommand command = fbcon.CreateCommand();

            command.CommandText = "select * from STAFF";

            FbDataReader reader = command.ExecuteReader();

            int count = 0;
            int intValue = 0;

            while (reader.Read())
            {
                if (count == 0) 
                {
                    intValue = reader.GetInt32(0);
                    String param0 = reader[0].ToString();
                    String param1 = reader[1].ToString();
                    String param2 = reader[2].ToString();
                    String param3 = reader[3].ToString();
                    String param4 = reader[4].ToString();
                    String param5 = reader[5].ToString();
                    System.Console.WriteLine("intValue: " + intValue);
                    System.Console.WriteLine("param0: " + param0);
                    System.Console.WriteLine("param1: " + param1);
                    System.Console.WriteLine("param2: " + param2);
                    System.Console.WriteLine("param3: " + param3);
                    System.Console.WriteLine("param4: " + param4);
                    System.Console.WriteLine("param5: " + param5);
                }
                count++;
            }

            System.Console.WriteLine("count: " + count);


            reader.Close();

            command.Dispose();






            System.Console.ReadKey();
        }
    }
}
