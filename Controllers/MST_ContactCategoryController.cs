using Microsoft.AspNetCore.Mvc;
using System.Data.SqlTypes;
using System.Data;
using System.Data.SqlClient;
using AddressBookDemo.Models;
using AddressBookDemo.DAL;

namespace AdminPanel.Controllers
{
    public class MST_ContactCategoryController : Controller
    {
        private IConfiguration Configuration;
        public MST_ContactCategoryController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        #region SelectAll
        public IActionResult Index()
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            MST_DAL dalLOC = new MST_DAL();
            DataTable dt = dalLOC.DBO_PR_MST_ContactCategory_SelectAll(str);
            return View("MST_ContactCategoryList", dt);
            //SqlConnection conn = new SqlConnection(str);
            //conn.Open();
            //SqlCommand sqlCommand = conn.CreateCommand();
            //sqlCommand.CommandType = CommandType.StoredProcedure;
            //sqlCommand.CommandText = "PR_MST_ContactCategory_SelectAll";
            //DataTable dt = new DataTable();
            //SqlDataReader rdr = sqlCommand.ExecuteReader();
            //dt.Load(rdr);
            //conn.Close();
            //return View("MST_ContactCategoryList", dt);
        }
        #endregion SelectAll
        #region Delete
        public IActionResult Delete(int ContactCategoryID)
        {

            {
                string str = this.Configuration.GetConnectionString("myConnectionString");
                SqlConnection conn = new SqlConnection(str);
                conn.Open();
                SqlCommand sqlCommand = conn.CreateCommand();
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = "PR_MST_ContactCategory_DeleteByPK";
                sqlCommand.Parameters.AddWithValue("@ContactCategoryID", ContactCategoryID);
                sqlCommand.ExecuteNonQuery();
                conn.Close();
                return RedirectToAction("Index");
            }
        }
        #endregion Delete
        #region Save
        [HttpPost]

        public IActionResult Save(MST_ContactCategoryModel modelMST_ContactCategory)
        {
            if (ModelState.IsValid)
            {
                string str = this.Configuration.GetConnectionString("myConnectionString");
                SqlConnection conn = new SqlConnection(str);
                conn.Open();

                SqlCommand objCmd = conn.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;

                if (modelMST_ContactCategory.ContactCategoryID == null)
                {
                    objCmd.CommandText = "PR_MST_ContactCategory_Insert";
                    objCmd.Parameters.Add("@CreationDate", SqlDbType.Date).Value = DBNull.Value;
                }
                else
                {
                    objCmd.CommandText = "PR_MST_ContactCategory_UpdateByPK";
                    objCmd.Parameters.Add("@ContactCategoryID", SqlDbType.Int).Value = modelMST_ContactCategory.ContactCategoryID;
                }
                objCmd.Parameters.Add("@ContactCategoryName", SqlDbType.NVarChar).Value = modelMST_ContactCategory.ContactCategoryName;
                objCmd.Parameters.Add("@ModificationDate", SqlDbType.Date).Value = DBNull.Value;
                if (Convert.ToBoolean(objCmd.ExecuteNonQuery()))
                {
                    if (modelMST_ContactCategory.ContactCategoryID == null)
                        TempData["ContactCategoryInsertingMsg"] = "Record Inserted Successfully";
                    else
                        return RedirectToAction("Index");
                }
                conn.Close();
            }
            return RedirectToAction("Add");

        }
        #endregion Save


        #region Add

        public IActionResult Add(int? ContactCategoryID)
        {
            
            if (ContactCategoryID != null)
            {
                string str = this.Configuration.GetConnectionString("myConnectionString");
                SqlConnection conn = new SqlConnection(str);
                conn.Open();

                SqlCommand objCmd = conn.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "PR_MST_ContactCategory_SelectByPK";
                objCmd.Parameters.Add("@ContactCategoryID", SqlDbType.Int).Value = ContactCategoryID;
                DataTable dt = new DataTable();
                SqlDataReader objSDR = objCmd.ExecuteReader();
                dt.Load(objSDR);

                MST_ContactCategoryModel modelMST_ContactCategory = new MST_ContactCategoryModel();


                foreach (DataRow dr in dt.Rows)
                {
                    modelMST_ContactCategory.ContactCategoryID = Convert.ToInt32(dr["ContactCategoryID"]);
                    modelMST_ContactCategory.ContactCategoryName = dr["ContactCategoryName"].ToString();
                    modelMST_ContactCategory.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                    modelMST_ContactCategory.ModificationDate = Convert.ToDateTime(dr["ModificationDate"]);
                }
                return View("MST_ContactCategoryAddEdit", modelMST_ContactCategory);
            }
            return View("MST_ContactCategoryAddEdit");
        }
        #endregion Add
    }
}