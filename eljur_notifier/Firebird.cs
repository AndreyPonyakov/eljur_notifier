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
        internal protected FbConnection FbCon { get; set; }

        public static FbConnection getConnection()
        {
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
            return fbcon;
        }

        public List<object[]> getOneStaff(int staffNumber)
        {
            this.FbCon = Firebird.getConnection();
            this.FbCon.Open();
            //FbDatabaseInfo fb_inf = new FbDatabaseInfo(this.FbCon);
            //Console.WriteLine("Info: " + fb_inf.ServerClass + "; " + fb_inf.ServerVersion);
            FbCommand command = this.FbCon.CreateCommand();
            command.CommandText = "select * from STAFF";
            FbDataReader reader = command.ExecuteReader();
            int count = 0;
            var staffs = new List<object[]>();

            while (reader.Read())
            {
                if (count == staffNumber || staffNumber == -1)
                {
                    var columns = new object[reader.FieldCount];
                    reader.GetValues(columns);
                    staffs.Add(columns);
                    //Console.WriteLine(staffs);
                    Console.WriteLine("staffs[" + count + "][id]: " + staffs[count][0].ToString());
                    Console.WriteLine("staffs["+count+"][LastName]: " + staffs[count][1].ToString());
                    Console.WriteLine("staffs[" + count + "][FirstName]: " + staffs[count][2].ToString());
                    Console.WriteLine("staffs[" + count + "][MiddleName]: " + staffs[count][3].ToString());

                }
                count++;
            }
            Console.WriteLine("count: " + count);
            reader.Close();
            command.Dispose();
            return staffs;
        }
        public List<object[]> getAllStaffs()
        {
            return this.getOneStaff(-1);
        }
    }
}
