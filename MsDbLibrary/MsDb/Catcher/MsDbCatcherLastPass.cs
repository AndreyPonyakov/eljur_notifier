using System;
using System.Collections.Generic;
using System.Linq;
using eljur_notifier.AppCommonNS;
using MsDbLibraryNS.MsDbNS.SetterNS;
using MsDbLibraryNS.StaffModel;

namespace MsDbLibraryNS.MsDbNS.CatcherNS
{
    public class MsDbCatcherLastPass : MsDbBaseClass
    {
        internal protected String nameorConnectionString { get; set; }

        public MsDbCatcherLastPass(String NameorConnectionString = "name=StaffContext") 
            : base(new StaffContext(NameorConnectionString)) {
            this.nameorConnectionString = NameorConnectionString;
        }

        public List<object[]> catchLastPass()
        {
            using (this.StaffCtx = new StaffContext(nameorConnectionString))
            {
                TimeSpan timeNow = DateTime.Now.TimeOfDay;
                TimeSpan EventTimeNowSubstract15Min = timeNow.Add(new TimeSpan(0, -15, 0));
                var returnedList = new List<object[]>();
                var PupilIdOldAndTimeRows = from e in StaffCtx.Events
                                            where e.NotifyWasSend == false && e.EventName == "Вышел" && e.EventTime < EventTimeNowSubstract15Min
                                            orderby e.EventTime
                                            select new
                                            {
                                                PupilIdOld = e.PupilIdOld,
                                                EventTime = e.EventTime
                                            };
                foreach (var PupilIdOldAndTime in PupilIdOldAndTimeRows)
                {
                    var PupilIdOldAndTimeMassObjects = new object[2];
                    PupilIdOldAndTimeMassObjects[0] = PupilIdOldAndTime.PupilIdOld;
                    PupilIdOldAndTimeMassObjects[1] = PupilIdOldAndTime.EventTime;
                    returnedList.Add(PupilIdOldAndTimeMassObjects);
                }
                return returnedList;               
            }
        }
    }
}
