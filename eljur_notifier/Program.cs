using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eljur_notifier;

namespace eljur_notifier
{
    class Program
    {
        static void Main(string[] args)
        {
            var Config = new Config();
            var Firebird = new Firebird(Config.ConnectStr);
            var staff = new List<object[]>();
            //staff = Firebird.getOneStaff(1040);
            //staff = Firebird.getAllStaff();
            DateTime date1 = DateTime.Now;
            Console.WriteLine(date1);
            Console.ReadKey();
        }
    }
}
