using System;
using System.Data.Entity;

namespace MsDbLibraryNS.StaffModel
{
    public class StaffContext : DbContext
    {
        public StaffContext(String NameorConnectionString = "name=StaffContext")
            : base(NameorConnectionString)
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