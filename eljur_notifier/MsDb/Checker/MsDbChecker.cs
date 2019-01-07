using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Data.SqlClient;
using eljur_notifier.AppCommon;
using eljur_notifier.FirebirdNS;
using eljur_notifier.MsDbNS.CreatorNS;
using eljur_notifier.MsDbNS.FillerNS;
using eljur_notifier.MsDbNS.RequesterNS;
using eljur_notifier.MsDbNS.CleanerNS;

namespace eljur_notifier.MsDbNS.CheckerNS
{
    class MsDbChecker
    {
        internal protected Message message { get; set; }
        internal protected Firebird firebird { get; set; }
        internal protected MsDb msDb { get; set; }
        internal protected CleanCreator cleanCreator { get; set; }
        internal protected MsDbCreator msDbCreator { get; set; }
        internal protected MsDbFiller msDbFiller { get; set; }
        internal protected ScheduleFiller scheduleFiller { get; set; }
        internal protected Config config { get; set; }
        internal protected Requester requester { get; set; }
        internal protected TimeChecker timeChecker { get; set; }
        internal protected ExistChecker existChecker { get; set; }
        internal protected EmptyChecker emptyChecker { get; set; }
        internal protected Cleaner cleaner { get; set; }


        public MsDbChecker(Config Config, MsDb MsDb, Firebird Firebird)
        {
            this.message = new Message();
            this.msDb = MsDb;
            this.config = Config;
            this.firebird = Firebird;            
            this.msDbCreator = new MsDbCreator(config);
            this.msDbFiller = new MsDbFiller(config);
            this.scheduleFiller = new ScheduleFiller(config);
            this.requester = new Requester(config);
            this.timeChecker = new TimeChecker(config, msDb);
            this.existChecker = new ExistChecker(config);
            this.emptyChecker = new EmptyChecker(config);
            this.cleaner = new Cleaner();
            this.CheckSomeIssuesInConstructor();
            
        }

        public void CheckSomeIssuesInConstructor()
        {
            this.CheckMsDb();
            if (existChecker.IsTableExist("Pupils") && existChecker.IsTableExist("Schedules"))
            {
                message.Display("msDb already exist", "Warn");
            }
            else
            {
                msDbCreator.CreateMsDb();
            }
            if (emptyChecker.IsTableEmpty("Schedules"))
            {
                message.Display("Schedules is Empty", "Warn");
                scheduleFiller.FillSchedulesDb();
            }
            else
            {
                message.Display("Schedules is not Empty", "Warn");
                cleaner.clearTableDb("Schedules");
                scheduleFiller.FillSchedulesDb();
            }
            if ((Convert.ToInt32(DateTime.Today.Day) == 1) && (Convert.ToInt32(DateTime.Today.Month) == 9))
            {
                message.Display("Today is 01.09 and we will change all Pupils in Pupils Table", "Info");
                //Now the data is updated every day
                //msDbCreator.CreateMsDb();
            }

        }

        

        public void CheckMsDb()
        {
            using (msDb.dbcon = new SqlConnection(config.ConStrMsDB))
            {
                message.Display("MsSQLDB is exist: " + msDb.IsDbExist(msDb.dbcon, "CheckMsDb func").ToString(), "Warn");
                if (msDb.IsDbExist(msDb.dbcon, "CheckMsDb func"))
                {
                    DateTime ModifyDate = requester.getModifyDate();
                    message.Display("DATABASE was modified: " + ModifyDate.ToString(), "Warn");
                    DateTime dateOnly = ModifyDate.Date;
                    if (dateOnly == DateTime.Today)
                    {
                        message.Display("DATABASE MsDb was modified Today!!!", "Warn");
                    }
                }
                else
                {
                    try
                    {
                        throw new Exception();
                    }
                    catch (Exception ex)
                    {
                        message.Display("Cannot connect to MsDb", "Fatal", ex);
                    }
                }
            }
        }

    }
}
