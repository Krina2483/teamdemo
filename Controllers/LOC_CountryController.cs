using AddressBookDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;



namespace AddressBookDemo.Controllers
{
    public class LOC_CountryController : Controller
    {

        private IConfiguration Configuration;


        public LOC_CountryController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        #region SelectAll


        public IActionResult Index()
        {
            string str = this.Configuration.GetConnectionString("myconnectionstring");
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            objcmd.CommandText = "PR_LOC_Country_SelectAll";


            SqlDataReader objSDR = objcmd.ExecuteReader();
            dt.Load(objSDR);

            return View("LOC_CountryList", dt);
        }
        #endregion



        public IActionResult Delete(int CountryID)
        {
            string str = this.Configuration.GetConnectionString("myconnectionstring");
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand sqlCommand = conn.CreateCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "PR_LOC_Country_DeleteByPK ";
            sqlCommand.Parameters.AddWithValue("@CountryID", CountryID);
            sqlCommand.ExecuteNonQuery();

            conn.Close();
            return RedirectToAction("Index");
        }
        #region Insert, Update => Save Method
        [HttpPost]
        public IActionResult Save(LOC_CountryModel modelLOC_Country)
        {
            if (ModelState.IsValid)
            {
                string connectionstr = this.Configuration.GetConnectionString("myConnectionString");

                //prepare a connection
                SqlConnection conn = new SqlConnection(connectionstr);
                conn.Open();

                //prepare a command
                SqlCommand objcmd = conn.CreateCommand();
                objcmd.CommandType = CommandType.StoredProcedure;

                if (modelLOC_Country.CountryID == null)
                {
                    objcmd.CommandText = "PR_LOC_Country_Insert";
                    objcmd.Parameters.Add("@CreationDate", SqlDbType.Date).Value = DBNull.Value;
                }
                else
                {
                    objcmd.CommandText = "PR_LOC_Country_UpdateByPK";
                    objcmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = modelLOC_Country.CountryID;
                }

                //get the parameters
                objcmd.Parameters.Add("@CountryName", SqlDbType.VarChar).Value = modelLOC_Country.CountryName;
                objcmd.Parameters.Add("@CountryCode", SqlDbType.VarChar).Value = modelLOC_Country.CountryCode;
                objcmd.Parameters.Add("@ModificationDate", SqlDbType.Date).Value = DBNull.Value;

                //execute a command

                if (Convert.ToBoolean(objcmd.ExecuteNonQuery()))
                {
                    if (modelLOC_Country.CountryID == null)

                        TempData["CountryInsertingMsg"] = "Record Insert Successfully";

                    else

                        return RedirectToAction("Index");

                }

                //close connection
                conn.Close();
            }

            return RedirectToAction("Add");
        }
        #endregion


        #region SelectByPK
        public IActionResult Add(int? CountryID)

        {

            if (CountryID != null)
            {
                string connectionstr = this.Configuration.GetConnectionString("myConnectionString");

                SqlConnection conn = new SqlConnection(connectionstr);
                conn.Open();
                SqlCommand objcmd = conn.CreateCommand();
                objcmd.CommandType = CommandType.StoredProcedure;
                objcmd.CommandText = "PR_LOC_Country_SelectByPK";
                objcmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = CountryID;
                DataTable dt = new DataTable();
                SqlDataReader objSDR = objcmd.ExecuteReader();
                dt.Load(objSDR);

                LOC_CountryModel modelLOC_Country = new LOC_CountryModel();

                foreach (DataRow dr in dt.Rows)
                {
                    modelLOC_Country.CountryID = Convert.ToInt32(dr["CountryID"]);
                    modelLOC_Country.CountryName = dr["CountryName"].ToString();
                    modelLOC_Country.CountryCode = dr["CountryCode"].ToString();
                    modelLOC_Country.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                    modelLOC_Country.ModificationDate = Convert.ToDateTime(dr["ModificationDate"]);
                }

                return View("LOC_CountryAddEdit", modelLOC_Country);
            }
            return View("LOC_CountryAddEdit");
        }
        #endregion
    }
}