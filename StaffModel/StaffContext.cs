namespace eljur_notifier.StaffModel
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class StaffContext : DbContext
    {
        public StaffContext()
            : base("name=StaffContext")
        {
        }  
         public  DbSet<Pupil> Pupils { get; set; }
    }


}