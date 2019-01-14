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

        public MsDb(int TreeIdResourceOutput1 = 8564, int TreeIdResourceOutput2 = 9369, int TreeIdResourceInput1 = 8677, int TreeIdResourceInput2 = 9256) 
            : base(new Message(), new MsDbRequester(), new MsDbSetter()) {
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
                    continue;
                }
                var PupilIdOld = Convert.ToInt32(row[1]);
                message.Display("Событие проход школьника с id " + PupilIdOld.ToString() + " в " + row[0].ToString(), "Trace");

                var result = msDbRequester.getEventdByPupilIdOld(PupilIdOld);      
                if (result != null)
                {
                    if (result.EventName == "Первый проход" || result.EventName == "Вернулся" || result.EventName == "Опоздал")
                    {
                        //register only OUTPUT configs_tree_id_resource
                        if (row[3].ToString() == treeIdResourceOutput1.ToString() || row[3].ToString() == treeIdResourceOutput2.ToString())
                        {
                            result.EventName = "Вышел";
                            result.EventTime = TimeSpan.Parse(row[0].ToString());
                            result.NotifyWasSend = false;
                            msDbSetter.SetUpdatedEvent(result);
                            message.Display("Школьник с id " + PupilIdOld + " вышел из школы в " + row[0].ToString(), "Trace");
                        }

                    }
                    else if (result.EventName == "Вышел" || result.EventName == "Прогул")
                    {
                        //register only INPUT configs_tree_id_resource
                        if (row[3].ToString() == treeIdResourceInput1.ToString() || row[3].ToString() == treeIdResourceInput2.ToString())
                        {                   
                            result.EventName = "Вернулся";
                            result.EventTime = TimeSpan.Parse(row[0].ToString());
                            result.NotifyWasSend = false;
                            msDbSetter.SetUpdatedEvent(result);
                            message.Display("Школьник с id " + PupilIdOld + "  вернулся в школу в " + row[0].ToString(), "Trace");
                        }
                    }
                }
                else
                {
                    //register only INPUT configs_tree_id_resource
                    if (row[3].ToString() == treeIdResourceInput1.ToString() || row[3].ToString() == treeIdResourceInput2.ToString())
                    {
                        Event Event = new Event();
                        Event.PupilIdOld = PupilIdOld;
                        Event.EventTime = TimeSpan.Parse(row[0].ToString());
                        Event.EventName = "Первый проход";
                        msDbSetter.SetOneFullEventForTesting(Event);
                        message.Display("Школьник с id " + PupilIdOld + "  пришёл в школу в " + row[0].ToString(), "Trace");
                    }
                }
            }
            
        }

          
       

    }
}
