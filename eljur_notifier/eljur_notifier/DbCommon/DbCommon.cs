using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eljur_notifier.AppCommonNS;

namespace eljur_notifier.DbCommon
{
    public class DbCommonClass : IDbCommonInf
    {  
        public Boolean IsDbExist(IDbConnection db, String FromWhere)
        {
            try
            {
                db.Open();
                db.Close();
                return true;
            }
            catch (Exception ex)
            {
                // Cannot connect to database
                Message message = new Message();
                message.Display("Cannot connect to database from " + FromWhere + " code string", "Error", ex);
                return false;
            }
        }
    }
}
