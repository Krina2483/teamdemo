using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace AddressBookDemo.DAL
{
    public class LOC_DALBase
    {
        #region dbo.PR_LOC_State_SelectAll

        public DataTable? DBO_PR_LOC_State_SelectAll(string conn)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_LOC_State_SelectAll");
                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch(Exception) 
            {
                return null;
            }

        }

        #endregion dbo.PR_LOC_State_SelectAll

    }
}
