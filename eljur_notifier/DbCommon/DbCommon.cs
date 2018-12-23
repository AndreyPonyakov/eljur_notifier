using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eljur_notifier.AppCommon;

namespace eljur_notifier.DbCommon
{
    class DbCommonClass : IDbCommonInf
    {  
        public Boolean IsDbExist(IDbConnection db)
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
                message.Display("Cannot connect to database", "Error", ex);
                return false;
            }
        }
    }
}
