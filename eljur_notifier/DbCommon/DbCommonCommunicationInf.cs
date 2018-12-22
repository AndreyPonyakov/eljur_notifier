using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eljur_notifier.DbCommon
{
    interface IDbCommonInf
    {
        Boolean IsDbExist(IDbConnection db);
 
        
    }
}
