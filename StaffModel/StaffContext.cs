namespace eljur_notifier.StaffModel
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class StaffContext : DbContext
    {
        // Контекст настроен для использования строки подключения "StaffContext" из файла конфигурации  
        // приложения (App.config или Web.config). По умолчанию эта строка подключения указывает на базу данных 
        // "eljur_notifier.StaffModel.StaffContext" в экземпляре LocalDb. 
        // 
        // Если требуется выбрать другую базу данных или поставщик базы данных, измените строку подключения "StaffContext" 
        // в файле конфигурации приложения.
        public StaffContext()
            : base("name=StaffContext")
        {
        }

        // Добавьте DbSet для каждого типа сущности, который требуется включить в модель. Дополнительные сведения 
        // о настройке и использовании модели Code First см. в статье http://go.microsoft.com/fwlink/?LinkId=390109.

         public  DbSet<Pupil> Pupils { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}