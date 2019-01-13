using System;
using System.Collections.Generic;
using System.Linq;
using MsDbLibraryNS.StaffModel;

namespace MsDbLibraryNS.MsDbNS.CatcherNS
{
    public class MsDbCatcherFirstPass : MsDbBaseClass
    {
        internal protected String nameorConnectionString { get; set; }

        public MsDbCatcherFirstPass(String NameorConnectionString = "name=StaffContext") 
            : base(new StaffContext(NameorConnectionString)) {
            this.nameorConnectionString = NameorConnectionString;
        }
   
        public List<object[]> catchFirstPass()
        {
            using (this.StaffCtx = new StaffContext(nameorConnectionString))
            {
                var returnedList = new List<object[]>();
                var PupilIdOldAndTimeRows = from e in StaffCtx.Events
                                            where e.NotifyWasSend == false && e.EventName == "Первый проход"
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
