using Microsoft.AspNetCore.Mvc;
using System.Data.SqlTypes;
using System.Data;
using System.Data.SqlClient;
using AddressBookDemo.Models;

namespace AdminPanel.Controllers
{
    public class LOC_CityController : Controller
    {
        private IConfiguration Configuration;
        public LOC_CityController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        
        #region SelectAll
        public IActionResult Index()
        {

            string str = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand sqlCommand = conn.CreateCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "PR_LOC_City_SelectAll";
            DataTable dt = new DataTable();
            SqlDataReader rdr = sqlCommand.ExecuteReader();
            dt.Load(rdr);
            conn.Close();
            return View("LOC_CityList", dt);
        }
        #endregion SelectAll

        #region Delete
        public IActionResult Delete(int CityID)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand sqlCommand = conn.CreateCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "PR_LOC_City_DeleteByPK";
            sqlCommand.Parameters.AddWithValue("@CityID", CityID);
            sqlCommand.ExecuteNonQuery();
            conn.Close();
            return RedirectToAction("Index");
        }
        #endregion Delete
        #region Save
        [HttpPost]

        public IActionResult Save(LOC_CityModel modelLOC_City)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();

            SqlCommand objCmd = conn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;

            if (modelLOC_City.CityID == null)
            {
                objCmd.CommandText = "PR_LOC_City_Insert";
                objCmd.Parameters.Add("@CreationDate", SqlDbType.Date).Value = DBNull.Value;
            }
            else
            {
                objCmd.CommandText = "PR_LOC_City_UpdateByPK";
                objCmd.Parameters.Add("@CityID", SqlDbType.Int).Value = modelLOC_City.CityID;
            }
            objCmd.Parameters.Add("@CityName", SqlDbType.NVarChar).Value = modelLOC_City.CityName;
            objCmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = modelLOC_City.CountryID;
            objCmd.Parameters.Add("@StateID", SqlDbType.Int).Value = modelLOC_City.StateID;
            objCmd.Parameters.Add("@CityPincode", SqlDbType.Int).Value = modelLOC_City.CityPincode;
            objCmd.Parameters.Add("@CityCode", SqlDbType.NVarChar).Value = modelLOC_City.CityCode;
            objCmd.Parameters.Add("@ModificationDate", SqlDbType.Date).Value = DBNull.Value;
            if (Convert.ToBoolean(objCmd.ExecuteNonQuery()))
            {
                if (modelLOC_City.CityID == null)
                    TempData["CityInsertingMsg"] = "Record Inserted Successfully";
                else
                    return RedirectToAction("Index");
            }
            conn.Close();
            return RedirectToAction("Add");

        }
        #endregion Save


        #region Add

        public IActionResult Add(int?CityID)
        {
            #region DropDownForCountry
            string connstr = this.Configuration.GetConnectionString("myConnectionString");

            //Prepare Connection
            SqlConnection sqlconn = new SqlConnection(connstr);
            sqlconn.Open();

            //Prepare Command
            SqlCommand sqlCommand = sqlconn.CreateCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "PR_LOC_Country_SelectForDropDown";
            DataTable dataTable = new DataTable();
            SqlDataReader rdr = sqlCommand.ExecuteReader();
            dataTable.Load(rdr);

            List<LOC_CountryDropDownModel> list = new List<LOC_CountryDropDownModel>();
            foreach (DataRow datarow in dataTable.Rows)
            {
                LOC_CountryDropDownModel vlst = new LOC_CountryDropDownModel();
                vlst.CountryID = Convert.ToInt32(datarow["CountryID"]);
                vlst.CountryName = (string)datarow["CountryName"];
                list.Add(vlst);
            }
            ViewBag.CountryList = list;
            #endregion DropDownForCountry

            #region DropDownForState

            List<LOC_StateDropDownModel> list1 = new List<LOC_StateDropDownModel>();
            ViewBag.StateList = list1;
            #endregion DropDownForState

            #region SelectByPK
            if (CityID != null)
            {
                string str = this.Configuration.GetConnectionString("myConnectionString");
                SqlConnection conn = new SqlConnection(str);
                conn.Open();

                SqlCommand objCmd = conn.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "PR_LOC_City_SelectByPK";
                objCmd.Parameters.Add("@CityID", SqlDbType.Int).Value =CityID;
                DataTable dt = new DataTable();
                SqlDataReader objSDR = objCmd.ExecuteReader();
                dt.Load(objSDR);

                LOC_CityModel modelLOC_City = new LOC_CityModel();


                foreach (DataRow dr in dt.Rows)
                {

                    modelLOC_City.CityName = dr["CityName"].ToString();
                    modelLOC_City.CountryID = Convert.ToInt32(dr["CountryID"]);
                    modelLOC_City.StateID = Convert.ToInt32(dr["StateID"]);
                    modelLOC_City.CityPincode = Convert.ToInt32(dr["CityPincode"]);
                    modelLOC_City.CityCode = dr["CityCode"].ToString();
                    modelLOC_City.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                    modelLOC_City.ModificationDate = Convert.ToDateTime(dr["ModificationDate"]);
                }
                return View("LOC_CityAddEdit", modelLOC_City);
            }
            return View("LOC_CityAddEdit");
        }
        #endregion SelectByPK

        #endregion Add
        public IActionResult DropDownByCountry(int CountryID)
        {
            #region DropDownForState
            string str1 = this.Configuration.GetConnectionString("myConnectionString");

            //Prepare Connection
            SqlConnection connection = new SqlConnection(str1);
            connection.Open();

            //Prepare Command
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_LOC_State_SelectComboBoxByCountryID";
            command.Parameters.AddWithValue("@CountryID", CountryID);
            DataTable table = new DataTable();
            SqlDataReader reader = command.ExecuteReader();
            table.Load(reader);

            List<LOC_StateDropDownModel> list1 = new List<LOC_StateDropDownModel>();
            foreach (DataRow row in table.Rows)
            {
                LOC_StateDropDownModel lst = new LOC_StateDropDownModel();
                lst.StateID = Convert.ToInt32(row["StateID"]);
                lst.StateName = (string)row["StateName"];
                list1.Add(lst);
            }

            var vmodel = list1;
            return Json(vmodel);
            #endregion DropDownForState
        }

    }
}