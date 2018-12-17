using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebirdSql.Data.FirebirdClient;

namespace eljur_notifier
{
    class Firebird
    {
        internal protected FbConnection FbCon { get; set; }
        internal protected String ConnectStr { get; set; }
        public Firebird(String ConnectStr)
        {
            this.ConnectStr = ConnectStr;
            this.FbCon = Firebird.getConnection(this.ConnectStr);
        }

        public static FbConnection getConnection(String ConnectStr)
        {
            FbConnection fbcon = new FbConnection(ConnectStr);
            return fbcon;
        }

        public List<object[]> getOneStaff(int staffNumber)
        {
            this.FbCon.Open();
            //FbDatabaseInfo fb_inf = new FbDatabaseInfo(this.FbCon);
            //Console.WriteLine("Info: " + fb_inf.ServerClass + "; " + fb_inf.ServerVersion);
            FbCommand command = this.FbCon.CreateCommand();
            command.CommandText = "select * from STAFF";
            FbDataReader reader = command.ExecuteReader();
            int count = 0;
            int count_staff = 0;
            var staff = new List<object[]>();

            while (reader.Read())
            {
                if (count == staffNumber || staffNumber == -1)
                {
                    var columns = new object[reader.FieldCount];
                    reader.GetValues(columns);
                    staff.Add(columns);
                    //Console.WriteLine(staff);
                    Console.WriteLine("staff[" + count + "][id]: " + staff[count_staff][0].ToString());
                    Console.WriteLine("staff[" + count + "][LastName]: " + staff[count_staff][1].ToString());
                    Console.WriteLine("staff[" + count + "][FirstName]: " + staff[count_staff][2].ToString());
                    Console.WriteLine("staff[" + count + "][MiddleName]: " + staff[count_staff][3].ToString());
                    count_staff++;
                }
                count++;
            }
            Console.WriteLine("count: " + count);
            reader.Close();
            command.Dispose();
            this.FbCon.Close();
            return staff;
        }
        public List<object[]> getAllStaff()
        {
            return this.getOneStaff(-1);
        }

        public List<object[]> getAnySqlQuery(String SqlQuery)
        {
            this.FbCon.Open();
            var rows = new List<object[]>();
            FbCommand command = this.FbCon.CreateCommand();
            command.CommandText = SqlQuery;
            FbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var columns = new object[reader.FieldCount];
                reader.GetValues(columns);
                rows.Add(columns);
            }
            this.FbCon.Close();
            return rows;
        }

        public List<object[]> getStaffByTimeStamp(DateTime TimeStamp)
        {
            var staff = new List<object[]>();
            staff = getAnySqlQuery("select time_ev, staff_id  from REG_EVENTS ORDER BY time_ev");
            Console.WriteLine(staff);
            foreach (object[] row in staff)
            {
                //Console.WriteLine(row);
                //Console.WriteLine(row.GetType());
                //Console.WriteLine(row.GetType().GetProperties());
                //Console.WriteLine(row[0].ToString());
                //break;
                foreach (object element in row)
                {
                    Console.WriteLine(element.ToString());
                }
                break;
            }
            return staff;

        }

    }
}
