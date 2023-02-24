using Microsoft.AspNetCore.Mvc;
using System.Data.SqlTypes;
using System.Data;
using System.Data.SqlClient;
using AddressBookDemo.Models;
using AddressBookDemo.DAL;

namespace AdminPanel.Controllers
{
    public class LOC_StateController : Controller
    {
        private IConfiguration Configuration;
        public LOC_StateController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        #region SelectAll
        public IActionResult Index()
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            LOC_DAL dalLOC = new LOC_DAL();
            DataTable dt = dalLOC.DBO_PR_LOC_State_SelectAll(str);
            return View("LOC_StateList", dt);

            //SqlConnection conn = new SqlConnection(str);
            //conn.Open();
            //SqlCommand sqlCommand = conn.CreateCommand();
            //sqlCommand.CommandType = CommandType.StoredProcedure;
            //sqlCommand.CommandText = "PR_LOC_State_SelectAll";
            //DataTable dt = new DataTable();
            //SqlDataReader rdr = sqlCommand.ExecuteReader();
            //dt.Load(rdr);
            //conn.Close();

        }
        #endregion SelectAll

        #region Delete

        public IActionResult Delete(int StateID)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand sqlCommand = conn.CreateCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "PR_LOC_State_DeleteByPK";
            sqlCommand.Parameters.AddWithValue("@StateID", StateID);
            sqlCommand.ExecuteNonQuery();
            conn.Close();
            return RedirectToAction("Index");
        }
        #endregion Delete
        #region Save
        [HttpPost]

        public IActionResult Save(LOC_StateModel modelLOC_State)
        {
            if (ModelState.IsValid)

            {
                string str = this.Configuration.GetConnectionString("myConnectionString");
                SqlConnection conn = new SqlConnection(str);
                conn.Open();

                SqlCommand objCmd = conn.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;

                if (modelLOC_State.StateID == null)
                {
                    objCmd.CommandText = "PR_LOC_State_Insert";
                    objCmd.Parameters.Add("@CreationDate", SqlDbType.Date).Value = DBNull.Value;
                }
                else
                {
                    objCmd.CommandText = "PR_LOC_State_UpdateByPK";
                    objCmd.Parameters.Add("@StateID", SqlDbType.Int).Value = modelLOC_State.StateID;
                }
                objCmd.Parameters.Add("@StateName", SqlDbType.NVarChar).Value = modelLOC_State.StateName;
                objCmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = modelLOC_State.CountryID;
                objCmd.Parameters.Add("@StateCode", SqlDbType.NVarChar).Value = modelLOC_State.StateCode;
                objCmd.Parameters.Add("@ModificationDate", SqlDbType.Date).Value = DBNull.Value;
                if (Convert.ToBoolean(objCmd.ExecuteNonQuery()))
                {
                    if (modelLOC_State.StateID == null)
                        TempData["StateInsertingMsg"] = "Record Inserted Successfully";
                    else
                        return RedirectToAction("Index");
                }
                conn.Close();
            }
            return RedirectToAction("Add");

        }
        #endregion Save


        #region Add

        public IActionResult Add(int? StateID)
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
                LOC_CountryDropDownModel vlst = new LOC_CountryDropDownModel
                {
                    CountryID = Convert.ToInt32(datarow["CountryID"]),
                    CountryName = (string)datarow["CountryName"]
                };
                list.Add(vlst);
            }
            ViewBag.CountryList = list;
            #endregion DropDownForCountry

            if (StateID != null)
            {
                string str = this.Configuration.GetConnectionString("myConnectionString");
                SqlConnection conn = new SqlConnection(str);
                conn.Open();

                SqlCommand objCmd = conn.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "PR_LOC_State_SelectByPK";
                objCmd.Parameters.Add("@StateID", SqlDbType.Int).Value = StateID;
                DataTable dt = new DataTable();
                SqlDataReader objSDR = objCmd.ExecuteReader();
                dt.Load(objSDR);

                LOC_StateModel modelLOC_State = new LOC_StateModel();


                foreach (DataRow dr in dt.Rows)
                {
                    
                    modelLOC_State.StateName = dr["StateName"].ToString();
                    modelLOC_State.CountryID = Convert.ToInt32(dr["CountryID"]);
                    modelLOC_State.StateCode = dr["StateCode"].ToString();
                    modelLOC_State.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                    modelLOC_State.ModificationDate = Convert.ToDateTime(dr["ModificationDate"]);
                }
                return View("LOC_StateAddEdit", modelLOC_State);
            }
            return View("LOC_StateAddEdit");
        }
        #endregion Add
    }
}