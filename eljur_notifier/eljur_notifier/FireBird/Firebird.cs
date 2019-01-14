using System;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;
using eljur_notifier.AppCommonNS;
using MsDbLibraryNS;

namespace eljur_notifier.FirebirdNS
{
    public class Firebird : EljurBaseClass
    {
        internal protected FbConnection dbcon { get; set; }
        public DateTime beforeDt { get; set; }
        public DateTime afterDt { get; set; }

        public Firebird() : base(new Message(), new Config())
        {
            this.dbcon = new FbConnection(config.ConStrFbDB);
          
            if (!this.IsDbExist(dbcon, "Firebird constructor"))
            {
                message.Display("Firebird database doesn't exist. Program will be closed!", "Fatal", new Exception());
            }
            this.beforeDt = Convert.ToDateTime("2000-12-31 23:59:59");
            this.afterDt = Convert.ToDateTime("2000-12-31 23:59:59");
        }

        public List<object[]> getAllStaff()
        {
            var staff = new List<object[]>();
            String SqlQuery = "select * from STAFF";
            staff = this.getAnySqlQuery(SqlQuery);
            return staff;                      
        }

        public List<object[]> getAnySqlQuery(String SqlQuery)
        {
            using (this.dbcon = new FbConnection(config.ConStrFbDB))
            { 
                this.dbcon.Open();
                var rows = new List<object[]>();
                using (FbCommand command = this.dbcon.CreateCommand())
                { 
                    command.CommandText = SqlQuery;
                    using (FbDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            var columns = new object[reader.FieldCount];
                            reader.GetValues(columns);
                            rows.Add(columns);
                        }
                    }
                }
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
            message.Display("Time from new beforeDt: " + this.beforeDt.ToString(), "Warn");
            message.Display("Time from new afterDt: " + this.afterDt.ToString(), "Warn");
        }


        public List<object[]> getEventsByIntervalRequest(String dateOnlyStr = "today")
        {
            TimeSpan IntervalRequest = TimeSpan.FromMilliseconds(config.IntervalRequest);
            var staff = new List<object[]>();
            SetBeforeDtAndAfterDt(IntervalRequest);
            if (dateOnlyStr == "today")
            {
                dateOnlyStr = DateTime.Now.ToShortDateString();
                dateOnlyStr = "01.10.2018"; //NEED COMMENT OUT THIS!!!!!!!!!!!!!!!
            }
            else
            {
                dateOnlyStr = "01.10.2018"; //NEED COMMENT OUT THIS!!!!!!!!!!!!!!!
            }
              
            String beforeStr = this.beforeDt.ToLongTimeString();
            String afterStr = this.afterDt.ToLongTimeString();

            message.Display("select time_ev, staff_id, date_ev, configs_tree_id_resource, configs_tree_id_controller  from REG_EVENTS WHERE  (time_ev BETWEEN '" + beforeStr + "' AND '" + afterStr + "') AND (date_ev = '" + dateOnlyStr + "') AND (configs_tree_id_controller = 9190 or configs_tree_id_controller = 8498)  AND (type_pass = 1) ORDER BY time_ev", "Info");
            staff = getAnySqlQuery("select time_ev, staff_id, date_ev, configs_tree_id_resource, configs_tree_id_controller  from REG_EVENTS WHERE  (time_ev BETWEEN '" + beforeStr + "' AND '" + afterStr + "') AND (date_ev = '" + dateOnlyStr + "') AND (configs_tree_id_controller = 9190 or configs_tree_id_controller = 8498)  AND (type_pass = 1) ORDER BY time_ev");
            return staff;
        }

    }
}
