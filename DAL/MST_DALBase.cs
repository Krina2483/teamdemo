using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace AddressBookDemo.DAL
{
    public class MST_DALBase
    {
        #region dbo.PR_MST_ContactCategory_SelectAll

        public DataTable DBO_PR_MST_ContactCategory_SelectAll(string conn)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_ContactCategory_SelectAll");
                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        #endregion dbo.PR_MST_ContactCategory_SelectAll
    }
}
