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
    public class MsDbChecker
    {
        internal protected Message message { get; set; }
        internal protected MsDb msDb { get; set; }
        internal protected CleanCreator cleanCreator { get; set; }
        internal protected MsDbCreator msDbCreator { get; set; }
        internal protected MsDbFiller msDbFiller { get; set; }
        internal protected Config config { get; set; }
        internal protected MsDbRequester msDbRequester { get; set; }
        internal protected TimeChecker timeChecker { get; set; }
        internal protected ExistChecker existChecker { get; set; }
        internal protected EmptyChecker emptyChecker { get; set; }
        internal protected MsDbCleaner msDbCleaner { get; set; }


        public MsDbChecker(Config Config)
        {
            this.message = new Message();      
            this.config = Config;
            this.msDb = new MsDb(config);
            this.msDbCreator = new MsDbCreator(config);
            this.msDbFiller = new MsDbFiller(config);
            this.msDbRequester = new MsDbRequester();
            this.timeChecker = new TimeChecker(config);
            this.existChecker = new ExistChecker(config);
            this.emptyChecker = new EmptyChecker(config);
            this.msDbCleaner = new MsDbCleaner();
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
                message.Display("Schedules are Empty", "Warn");
                msDbCleaner.clearTableDb("Schedules");
                msDbFiller.FillOnlySchedules();
            }
            else
            {
                message.Display("Schedules are not Empty", "Warn");
            }

            if (emptyChecker.IsTableEmpty("Pupils"))
            {
                message.Display("Pupils are Empty", "Warn");
                msDbCleaner.clearTableDb("Pupils");
                msDbFiller.FillOnlyPupils();
            }
            else
            {
                message.Display("Pupils are not Empty", "Warn");
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
                    message.Display("DATABASE MsDb already exist", "Warn");                   
                }
                else
                {
                    try
                    {
                        throw new Exception();
                    }
                    catch (Exception ex)
                    {
                        message.Display("Cannot connect to MsDb from CheckMsDb Func", "Fatal", ex);
                    }
                }
            }
        }

    }
}
