using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using FirebirdSql.Data.FirebirdClient;
using eljur_notifier.DbCommon;
using eljur_notifier.AppCommon;

namespace eljur_notifier.FirebirdNS
{
    class Firebird : DbCommonClass
    {
        internal protected FbConnection dbcon { get; set; }
        internal protected String ConnectStr { get; set; }
        internal protected DateTime beforeDt { get; set; }
        internal protected DateTime afterDt { get; set; }
        internal protected Message message { get; set; }

        public Firebird(String ConnectStr)
        {
            this.message = new Message();
            this.ConnectStr = ConnectStr;
            this.dbcon = new FbConnection(ConnectStr);
          
            if (!this.IsDbExist(dbcon, "Firebird constructor"))
            {
                message.Display("Firebird database doesn't exist. Program will be closed!", "Fatal", new Exception());
            }
            this.beforeDt = Convert.ToDateTime("2000-12-31 23:59:59");
            this.afterDt = Convert.ToDateTime("2000-12-31 23:59:59");
        }



        public List<object[]> getOneStaff(int staffNumber)
        {
            this.dbcon.Open();
            FbCommand command = this.dbcon.CreateCommand();
            command.CommandText = "select * from STAFF";
            int count = 0;
            int count_staff = 0;
            var staff = new List<object[]>();

            using (FbDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (count == staffNumber || staffNumber == -1)
                    {
                        var columns = new object[reader.FieldCount];
                        reader.GetValues(columns);
                        staff.Add(columns);
                        //Console.WriteLine(staff);
                        //Console.WriteLine("staff[" + count + "][id]: " + staff[count_staff][0].ToString());
                        //Console.WriteLine("staff[" + count + "][LastName]: " + staff[count_staff][1].ToString());
                        //Console.WriteLine("staff[" + count + "][FirstName]: " + staff[count_staff][2].ToString());
                        //Console.WriteLine("staff[" + count + "][MiddleName]: " + staff[count_staff][3].ToString());
                        count_staff++;
                    }
                    count++;
                }
            }
            Console.WriteLine("count: " + count);
            command.Dispose();
            this.dbcon.Close();
            return staff;
        }
        public List<object[]> getAllStaff()
        {
            return this.getOneStaff(-1);
        }

        public List<object[]> getAnySqlQuery(String SqlQuery)
        {
            this.dbcon.Open();
            var rows = new List<object[]>();
            FbCommand command = this.dbcon.CreateCommand();
            command.CommandText = SqlQuery;
            using (FbDataReader reader = command.ExecuteReader())
            {

                while (reader.Read())
                {
                    var columns = new object[reader.FieldCount];
                    reader.GetValues(columns);
                    rows.Add(columns);
                }
                command.Dispose();
                this.dbcon.Close();
                return rows;
            }
        }

        public void SetBeforeDtAndAfterDt(TimeSpan IntervalRequest)
        {
            if (this.beforeDt == Convert.ToDateTime("2000-12-31 23:59:59"))
            {
                this.afterDt = DateTime.Now;
                this.afterDt = this.afterDt.Add(new TimeSpan(-8, -25, 0));// NEED COMMENT OUT THIS!!!!!!!!!!!!!!!
                this.beforeDt = this.afterDt.Subtract(IntervalRequest); //Add(new TimeSpan(0, -1, 0));
            }
            else
            {
                //+1 sec because Firebird sql operation Between  is inclusive
                this.beforeDt = this.afterDt.Add(new TimeSpan(0, 0, 1)); // +1 second to IntervalRequest only for beforeDt
                this.afterDt = this.afterDt.Add(IntervalRequest);
            }
            message.Display("Time from new beforeDt: " + this.beforeDt.ToString(), "Trace");
            message.Display("Time from new afterDt: " + this.afterDt.ToString(), "Trace");
        }


        public List<object[]> getStaffByTimeStamp(Config Config)
        {
            TimeSpan IntervalRequest = TimeSpan.FromMilliseconds(Config.IntervalRequest);
            var staff = new List<object[]>();
            SetBeforeDtAndAfterDt(IntervalRequest);
            String dateOnlyStr = DateTime.Now.ToShortDateString();
            dateOnlyStr = "01.10.2018"; //NEED COMMENT OUT THIS!!!!!!!!!!!!!!!
            String beforeStr = this.beforeDt.ToLongTimeString();
            String afterStr = this.afterDt.ToLongTimeString();
            //message.Display("select time_ev, staff_id, configs_tree_id_controller  from REG_EVENTS WHERE  (time_ev BETWEEN '" + beforeStr + "' AND '" + afterStr + "') AND (configs_tree_id_controller = 9190 or configs_tree_id_controller = 8498 or configs_tree_id_controller = 7806)  ORDER BY time_ev", "Trace");
            //staff = getAnySqlQuery("select time_ev, staff_id, configs_tree_id_controller  from REG_EVENTS WHERE  (time_ev BETWEEN '" + beforeStr + "' AND '" + afterStr + "') AND (configs_tree_id_controller = 9190 or configs_tree_id_controller = 8498 or configs_tree_id_controller = 7806)  ORDER BY time_ev");
            //staff = getAnySqlQuery("select time_ev, staff_id, date_ev, configs_tree_id_resource, configs_tree_id_controller  from REG_EVENTS WHERE  (time_ev BETWEEN '07:40:00' AND '07:41:00') AND (date_ev = '01.10.2018') AND (configs_tree_id_controller = 9190 or configs_tree_id_controller = 8498)  AND (type_pass = 1) ORDER BY time_ev");
            message.Display("select time_ev, staff_id, date_ev, configs_tree_id_resource, configs_tree_id_controller  from REG_EVENTS WHERE  (time_ev BETWEEN '" + beforeStr + "' AND '" + afterStr + "') AND (date_ev = '" + dateOnlyStr + "') AND (configs_tree_id_controller = 9190 or configs_tree_id_controller = 8498)  AND (type_pass = 1) ORDER BY time_ev", "Trace");
            staff = getAnySqlQuery("select time_ev, staff_id, date_ev, configs_tree_id_resource, configs_tree_id_controller  from REG_EVENTS WHERE  (time_ev BETWEEN '" + beforeStr + "' AND '" + afterStr + "') AND (date_ev = '" + dateOnlyStr + "') AND (configs_tree_id_controller = 9190 or configs_tree_id_controller = 8498)  AND (type_pass = 1) ORDER BY time_ev");
            return staff;
        }

    }
}
