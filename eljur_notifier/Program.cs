using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eljur;

namespace Eljur
{
    class Program
    {
        static void Main(string[] args)
        {
            var Firebird = new Firebird();
            var staffs = new List<object[]>();
            //staffs = Firebird.getOneStaff(1040);
            staffs = Firebird.getAllStaffs();
            Console.ReadKey();
        }
    }
}
