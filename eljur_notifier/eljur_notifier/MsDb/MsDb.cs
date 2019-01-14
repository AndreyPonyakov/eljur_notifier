using System;
using System.Collections.Generic;
using MsDbLibraryNS.StaffModel;
using eljur_notifier.AppCommonNS;
using MsDbLibraryNS;
using MsDbLibraryNS.MsDbNS.RequesterNS;
using MsDbLibraryNS.MsDbNS.SetterNS;



namespace eljur_notifier.MsDbNS
{
    public class MsDb : EljurBaseClass
    {
        internal protected int treeIdResourceOutput1 { get; set; }
        internal protected int treeIdResourceOutput2 { get; set; }
        internal protected int treeIdResourceInput1 { get; set; }
        internal protected int treeIdResourceInput2 { get; set; }

        public MsDb(String NameorConnectionString = "name=StaffContext", int TreeIdResourceOutput1 = 8564, int TreeIdResourceOutput2 = 9369, int TreeIdResourceInput1 = 8677, int TreeIdResourceInput2 = 9256) 
            : base(new Message(), new MsDbRequester(NameorConnectionString), new MsDbSetter(NameorConnectionString)) {
            this.treeIdResourceOutput1 = TreeIdResourceOutput1;
            this.treeIdResourceOutput2 = TreeIdResourceOutput2;
            this.treeIdResourceInput1 = TreeIdResourceInput1;
            this.treeIdResourceInput2 = TreeIdResourceInput2;
        }
     
        public void CheckEventsDb(List<object[]> curEvents)
        {
                      
            foreach (object[] row in curEvents)
            {
                if (row[1] == DBNull.Value)
                {
                    message.Display("DBNULL value was skipped", "Warn");
                    continue;
                }

                var PupilIdOld = Convert.ToInt32(row[1]);
                message.Display("Событие проход школьника с id " + PupilIdOld.ToString() + " в " + row[0].ToString(), "Trace");

                var result = msDbRequester.getEventdByPupilIdOld(PupilIdOld);      
                if (result != null)
                {
                    CheckCurEvent(result, row, PupilIdOld);
                }
                else
                {
                    AddNewEvent(row, PupilIdOld);
                }
            }
            
        }


        public void CheckCurEvent(Event curEvent, object[] row, int PupilIdOld)
        {
            if (IsInputEventName(curEvent.EventName))
            {
                //register only OUTPUT configs_tree_id_resource
                if (IsOutputPass(row[3]))
                {
                    RegisterOutputEvent(curEvent, row, PupilIdOld);
                }

            }
            else if (IsOutPutEventName(curEvent.EventName))
            {
                //register only INPUT configs_tree_id_resource
                if (IsInputPass(row[3]))
                {
                    RegisterInputEvent(curEvent, row, PupilIdOld);
                }
            }

        }

        public void RegisterOutputEvent(Event CurEvent, object[] row, int PupilIdOld)
        {
            CurEvent.EventName = "Вышел";
            CurEvent.EventTime = TimeSpan.Parse(row[0].ToString());
            CurEvent.NotifyWasSend = false;
            msDbSetter.SetUpdatedEvent(CurEvent);
            message.Display("Школьник с id " + PupilIdOld + " вышел из школы в " + row[0].ToString(), "Trace");
        }

        public void RegisterInputEvent(Event CurEvent, object[] row, int PupilIdOld)
        {
            CurEvent.EventName = "Вернулся";
            CurEvent.EventTime = TimeSpan.Parse(row[0].ToString());
            CurEvent.NotifyWasSend = false;
            msDbSetter.SetUpdatedEvent(CurEvent);
            message.Display("Школьник с id " + PupilIdOld + "  вернулся в школу в " + row[0].ToString(), "Trace");
        }


        public void AddNewEvent(object[] row, int PupilIdOld)
        {
            //register only INPUT configs_tree_id_resource
            if (IsInputPass(row[3]))
            {
                Event Event = new Event();
                Event.PupilIdOld = PupilIdOld;
                Event.EventTime = TimeSpan.Parse(row[0].ToString());
                Event.EventName = "Первый проход";
                msDbSetter.SetOneFullEventForTesting(Event);
                message.Display("Школьник с id " + PupilIdOld + "  пришёл в школу в " + row[0].ToString(), "Trace");
            }

        }

        public Boolean IsInputPass(object CurTreeIdResource)
        {
            if (CurTreeIdResource.ToString() == treeIdResourceInput1.ToString() || CurTreeIdResource.ToString() == treeIdResourceInput2.ToString())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean IsOutputPass(object CurTreeIdResource)
        {
            if (CurTreeIdResource.ToString() == treeIdResourceOutput1.ToString() || CurTreeIdResource.ToString() == treeIdResourceOutput2.ToString())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean IsInputEventName(String CurEventName)
        {
            if (CurEventName == "Первый проход" || CurEventName == "Вернулся" || CurEventName == "Опоздал")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean IsOutPutEventName(String CurEventName)
        {
            if (CurEventName == "Вышел" || CurEventName == "Прогул")
            {
                return true;
            }
            else
            {
                return false;
            }
        }








    }
}
