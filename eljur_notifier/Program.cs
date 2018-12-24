using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Data;
using System.Data.SqlClient;
using eljur_notifier;
using eljur_notifier.StaffModel;
using eljur_notifier.AppCommon;
using eljur_notifier.FirebirdNS;
using eljur_notifier.MsDbNS;
using eljur_notifier.EventHandlerNS;
using NLog;


namespace eljur_notifier
{
    class Program
    {
        static void Main(string[] args)
        { 
            Message message = new Message();
            Config Config = new Config();         
            Firebird Firebird = new Firebird(Config.ConStrFbDB);
            MsDb MsDb = new MsDb(Config.ConStrMsDB);
            MsDbChecker MsDbChecker = new MsDbChecker(MsDb, Config, Firebird);
            EventHandlerEljur EventHandler = new EventHandlerEljur(Config, Firebird, MsDb, MsDbChecker);
         
            Task taskGetDataFb = new Task(() => EventHandler.GetDataFb());
            //Task taskSendNotifyParents = new Task(() => EventHandler.SendNotifyParents(Config.EljurApiTocken, Config.FrenchLeaveInterval));
            taskGetDataFb.Start();
            //taskSendNotifyParents.Start();

            Console.ReadKey();
        }
   
    }
}

