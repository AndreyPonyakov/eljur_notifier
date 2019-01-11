using System;
using System.Collections.Generic;
using System.Linq;
using eljur_notifier.StaffModel;
using eljur_notifier.AppCommonNS;


namespace eljur_notifier.MsDbNS
{
    public class MsDb : EljurBaseClass
    {
        public MsDb() : base(new Message(), new Config(), new StaffContext()) { }
     
        public void CheckEventsDb(List<object[]> curEvents)
        {
            using (this.StaffCtx = new StaffContext())
            {
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

          
       

    }
}
