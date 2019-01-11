using System;
using System.Data;

namespace eljur_notifier.DbCommonNS
{
    interface IDbCommonInf
    {
        Boolean IsDbExist(IDbConnection db, String FromWhere);      
    }
}
