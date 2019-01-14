using System;
using System.Data;
using MsDbLibraryNS;

namespace eljur_notifier.DbCommonNS
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
