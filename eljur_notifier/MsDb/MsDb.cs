using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using eljur_notifier;
using eljur_notifier.StaffModel;
using eljur_notifier.DbCommon;
using eljur_notifier.AppCommon;
using eljur_notifier.EljurNS;
using eljur_notifier.MsDbNS.CreatorNS;



namespace eljur_notifier.MsDbNS
{
    class MsDb : DbCommonClass
    {
        internal protected Message message { get; set; }
        internal protected Config config { get; set; }
        internal protected CleanCreator cleanCreator { get; set; }
        internal protected String ConnectStr { get; set; }
        internal protected SqlConnection dbcon { get; set; }
        internal protected StaffContext StaffCtx { get; set; }


        public MsDb(Config Config)
        {
            this.config = Config;
            this.cleanCreator = new CleanCreator();
            this.ConnectStr = config.ConStrMsDB;
            this.message = new Message();
            using (this.dbcon = new SqlConnection(ConnectStr))
            {
                if (IsTableExist("Pupils"))
                {
                    message.Display("msDb already exist", "Warn");
                }
                else
                {
                    cleanCreator.createCleanMsDb(config.ConStrMsDB);
                }
            }
        }

            
        



        public void CheckEventsDb(List<object[]> curEvents)
        {
            using (this.StaffCtx = new StaffContext())
            {
                EljurApiRequester elRequester = new EljurApiRequester(config);
                foreach (object[] row in curEvents)
                {
                    if (row[1] == DBNull.Value)
                    {
                        continue;
                    }
                    var PupilIdOld = Convert.ToInt32(row[1]);
                    message.Display("Событие проход школьника с id " + PupilIdOld.ToString() + " в " + row[0].ToString(), "Trace");


                    var result = StaffCtx.Events.SingleOrDefault(e => e.PupilIdOld == PupilIdOld);
                    if (result != null)
                    {
                        if (result.EventName == "Первый проход" || result.EventName == "Вернулся" || result.EventName == "Опоздал")
                        {
                            //register only OUTPUT configs_tree_id_resource
                            if (row[3].ToString() == config.ConfigsTreeIdResourceOutput1.ToString() || row[3].ToString() == config.ConfigsTreeIdResourceOutput2.ToString())
                            {
                                result.EventName = "Вышел";
                                result.EventTime = TimeSpan.Parse(row[0].ToString());
                                result.NotifyWasSend = false;
                                StaffCtx.SaveChanges();
                                message.Display("Школьник с id " + PupilIdOld + " вышел из школы в " + row[0].ToString(), "Trace");
                            }

                        }
                        else if (result.EventName == "Вышел" || result.EventName == "Прогул")
                        {
                            //register only INPUT configs_tree_id_resource
                            if (row[3].ToString() == config.ConfigsTreeIdResourceInput1.ToString() || row[3].ToString() == config.ConfigsTreeIdResourceInput2.ToString())
                            {                   
                                result.EventName = "Вернулся";
                                result.EventTime = TimeSpan.Parse(row[0].ToString());
                                result.NotifyWasSend = false;
                                StaffCtx.SaveChanges();
                                message.Display("Школьник с id " + PupilIdOld + "  вернулся в школу в " + row[0].ToString(), "Trace");
                            }
                        }
                    }
                    else
                    {
                        //register only INPUT configs_tree_id_resource
                        if (row[3].ToString() == config.ConfigsTreeIdResourceInput1.ToString() || row[3].ToString() == config.ConfigsTreeIdResourceInput2.ToString())
                        {
                            Event Event = new Event();
                            Event.PupilIdOld = PupilIdOld;
                            Event.EventTime = TimeSpan.Parse(row[0].ToString());
                            Event.EventName = "Первый проход";
                            StaffCtx.Events.Add(Event);
                            StaffCtx.SaveChanges();
                            message.Display("Школьник с id " + PupilIdOld + "  пришёл в школу в " + row[0].ToString(), "Trace");
                        }
                    }
                }
            }
        }

        

        public void deleteDb(String conStr)
        {

            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();
                String sqlCommandTextSingleUser = @"
                ALTER DATABASE " + "StaffDb" + @" SET SINGLE_USER WITH ROLLBACK IMMEDIATE;";
                SqlCommand sqlCommandSingleUser = new SqlCommand(sqlCommandTextSingleUser, con);
                sqlCommandSingleUser.ExecuteNonQuery();

                String sqlCommandTextDeleteDB = @"
                USE Master;
                DROP DATABASE [" + "StaffDb" + "]";
                SqlCommand sqlCommandDeleteDB = new SqlCommand(sqlCommandTextDeleteDB, con);
                sqlCommandDeleteDB.ExecuteNonQuery();
            }
        }

        public void clearTableDb(String TableName)
        {
            using (this.StaffCtx = new StaffContext())
            {
                this.StaffCtx.Database.ExecuteSqlCommand("TRUNCATE TABLE [" + TableName + "]");
            }

        }

        


        



        public Boolean IsTableExist(String TableName)
        {
            using (this.dbcon = new SqlConnection(config.ConStrMsDB))
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
        }

       

    }
}
