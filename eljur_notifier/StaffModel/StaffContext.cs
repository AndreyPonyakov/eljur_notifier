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
            // Указывает EF, что если модель изменилась,
            // нужно воссоздать базу данных с новой структурой
            Database.SetInitializer(
                new DropCreateDatabaseIfModelChanges<StaffContext>());


        }  
        public DbSet<Pupil> Pupils { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
    }


}