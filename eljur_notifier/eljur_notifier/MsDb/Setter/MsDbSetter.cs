using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eljur_notifier.AppCommonNS;
using eljur_notifier.StaffModel;

namespace eljur_notifier.MsDbNS.SetterNS
{
    public class MsDbSetter
    {
        internal protected Message message { get; set; }
        internal protected StaffContext StaffCtx { get; set; }

        public MsDbSetter()
        {
            this.message = new Message();
        }

        public void SetClasByPupilIdOld(int PupilIdOld, String Clas)
        {
            using (this.StaffCtx = new StaffContext())
            {
                var result = StaffCtx.Pupils.SingleOrDefault(e => e.PupilIdOld == PupilIdOld);
                if (result != null)
                {
                    result.Clas = Clas;
                    StaffCtx.SaveChanges();
                    message.Display("Clas to " + PupilIdOld + " PupilIdOld was updated", "Info");
                }
            }
        }


        public void SetStatusWentHomeTooEarly(int PupilIdOld)
        {
            using (this.StaffCtx = new StaffContext())
            {
                var result = StaffCtx.Events.SingleOrDefault(e => e.PupilIdOld == PupilIdOld);
                if (result != null)
                {
                    result.EventName = "Прогул";
                    StaffCtx.SaveChanges();
                    message.Display("WentHomeTooEarly to " + PupilIdOld + " PupilIdOld was set", "Trace");
                }
            }
        }


        public void SetStatusCameTooLate(int PupilIdOld)
        {
            using (this.StaffCtx = new StaffContext())
            {
                var result = StaffCtx.Events.SingleOrDefault(e => e.PupilIdOld == PupilIdOld);
                if (result != null)
                {
                    result.EventName = "Опоздал";
                    StaffCtx.SaveChanges();
                    message.Display("CameTooLate to " + PupilIdOld + " PupilIdOld was set", "Trace");
                }
            }
        }


        public void SetStatusNotifyWasSend(int PupilIdOld)
        {
            using (this.StaffCtx = new StaffContext())
            {
                var result = StaffCtx.Events.SingleOrDefault(e => e.PupilIdOld == PupilIdOld);
                if (result != null)
                {
                    result.NotifyWasSend = true;
                    StaffCtx.SaveChanges();
                    message.Display("Status NotifyWasSend to " + PupilIdOld + " PupilIdOld was set", "Trace");
                }
            }
        }


    }
}
