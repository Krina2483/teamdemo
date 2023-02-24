using AddressBookDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlTypes;
using System.Data;
using System.Data.SqlClient;


namespace AdminPanel.Controllers
{
    public class CON_ContactController : Controller
    {
        private IConfiguration Configuration;
        public CON_ContactController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        #region Index
        public IActionResult Index()
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand sqlCommand = conn.CreateCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "PR_CON_Contact_SelectAll";
            DataTable dt = new DataTable();
            SqlDataReader rdr = sqlCommand.ExecuteReader();
            dt.Load(rdr);
            conn.Close();
            return View("CON_ContactList", dt);
        }
        #endregion Index

        #region Delete
        public IActionResult Delete(int PersonID)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand sqlCommand = conn.CreateCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "PR_CON_Contact_DeleteByPK";
            sqlCommand.Parameters.AddWithValue("@PersonID", PersonID);
            sqlCommand.ExecuteNonQuery();
            conn.Close();

            return RedirectToAction("Index");
        }
        #endregion Delete

        #region Save
        [HttpPost]

        public IActionResult Save(CON_ContactModel modelCON_Contact)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");

            //Prepare Connection
            SqlConnection conn = new SqlConnection(str);
            conn.Open();

            //Prepare Command
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            if (modelCON_Contact.PersonID == null)
            {
                cmd.CommandText = "PR_CON_Contact_Insert";
                cmd.Parameters.Add("@CreationDate", SqlDbType.Date).Value = DBNull.Value;
            }
            else
            {
                cmd.CommandText = "PR_CON_Contact_UpdateByPK";
                cmd.Parameters.Add("@PersonID", SqlDbType.Int).Value = modelCON_Contact.PersonID;
            }
            cmd.Parameters.Add("@PersonName", SqlDbType.NVarChar).Value = modelCON_Contact.PersonName;
            cmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = modelCON_Contact.CountryID;
            cmd.Parameters.Add("@StateID", SqlDbType.Int).Value = modelCON_Contact.StateID;
            cmd.Parameters.Add("@CityID", SqlDbType.Int).Value = modelCON_Contact.CityID;
            cmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = modelCON_Contact.Address;
            cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = modelCON_Contact.Email;
            cmd.Parameters.Add("@Mobile", SqlDbType.NVarChar).Value = modelCON_Contact.Mobile;
            cmd.Parameters.Add("@AlternetContact", SqlDbType.NVarChar).Value = modelCON_Contact.AlternetContact;
            cmd.Parameters.Add("@Gender", SqlDbType.NVarChar).Value = modelCON_Contact.Gender;
            cmd.Parameters.Add("@Date_Of_Birth", SqlDbType.DateTime).Value = modelCON_Contact.Date_Of_Birth;
            cmd.Parameters.Add("@Linkedln", SqlDbType.VarChar).Value = modelCON_Contact.Linkedln;
            cmd.Parameters.Add("@Twiiter", SqlDbType.VarChar).Value = modelCON_Contact.Twiiter;
            cmd.Parameters.Add("@Insta", SqlDbType.VarChar).Value = modelCON_Contact.Insta;
            cmd.Parameters.Add("@GitHub", SqlDbType.VarChar).Value = modelCON_Contact.GitHub;
            cmd.Parameters.Add("@TypeOfProfessio", SqlDbType.NVarChar).Value = modelCON_Contact.TypeOfProfessio;
            cmd.Parameters.Add("@CompanyName", SqlDbType.NVarChar).Value = modelCON_Contact.CompanyName;
            cmd.Parameters.Add("@Designation", SqlDbType.NVarChar).Value = modelCON_Contact.Designation;
            cmd.Parameters.Add("@ContactCategoryID", SqlDbType.Int).Value = modelCON_Contact.ContactCategoryID;
            cmd.Parameters.Add("@ModificationDate", SqlDbType.Date).Value = DBNull.Value;

            if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                if (modelCON_Contact.PersonID == null)
                {
                    TempData["PersonInsertingMsg"] = "Record Inserted Successfully.";
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            conn.Close();
            return RedirectToAction("Add");
        }

        #endregion Save


        #region Add

        public IActionResult Add(int? PersonID)
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

            #region DropDownForState

            List<LOC_CityDropDownModel> list2 = new List<LOC_CityDropDownModel>();
            ViewBag.CityList = list2;
            #endregion DropDownForCity

            #region DropDownForContactCategory
            string str3 = this.Configuration.GetConnectionString("myConnectionString");

            //Prepare Connection
            SqlConnection connection3 = new SqlConnection(str3);
            connection3.Open();

            //Prepare Command
            SqlCommand command3 = connection3.CreateCommand();
            command3.CommandType = CommandType.StoredProcedure;
            command3.CommandText = "PR_MST_ContactCategory_SelectForDropDown";
            DataTable table3 = new DataTable();
            SqlDataReader reader3 = command3.ExecuteReader();
            table3.Load(reader3);

            List<MST_ContactCategoryDropDownModel> list3 = new List<MST_ContactCategoryDropDownModel>();
            foreach (DataRow row in table3.Rows)
            {
                MST_ContactCategoryDropDownModel lst = new MST_ContactCategoryDropDownModel();
                lst.ContactCategoryID = Convert.ToInt32(row["ContactCategoryID"]);
                lst.ContactCategoryName = (string)row["ContactCategoryName"];
                list3.Add(lst);
            }
            ViewBag.ContactCategoryList = list3;
            #endregion DropDownForContactCategory

            if (PersonID != null)
            {
                string str = this.Configuration.GetConnectionString("myConnectionString");
                SqlConnection conn = new SqlConnection(str);
                conn.Open();

                SqlCommand objCmd = conn.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "PR_CON_Contact_SelectByPK";
                objCmd.Parameters.Add("@PersonID", SqlDbType.Int).Value = PersonID;
                DataTable dt = new DataTable();
                SqlDataReader objSDR = objCmd.ExecuteReader();
                dt.Load(objSDR);

                CON_ContactModel modelCON_Contact = new CON_ContactModel();


                foreach (DataRow dr in dt.Rows)
                {

                    modelCON_Contact.PersonName = dr["PersonName"].ToString();
                    modelCON_Contact.CountryID = Convert.ToInt32(dr["CountryID"]);
                    modelCON_Contact.CityID = Convert.ToInt32(dr["CityID"]);
                    modelCON_Contact.StateID = Convert.ToInt32(dr["StateID"]);
                    modelCON_Contact.Address = dr["Address"].ToString();
                    modelCON_Contact.Mobile = dr["Mobile"].ToString();
                    modelCON_Contact.AlternetContact = dr["AlternetContact"].ToString();
                    modelCON_Contact.Email = dr["Email"].ToString();
                    modelCON_Contact.Date_Of_Birth = Convert.ToDateTime(dr["Date_Of_Birth"]);
                    modelCON_Contact.Linkedln = dr["Linkedln"].ToString();
                    modelCON_Contact.Twiiter = dr["Twiiter"].ToString();
                    modelCON_Contact.Insta = dr["Insta"].ToString();
                    modelCON_Contact.GitHub = dr["GitHub"].ToString();
                    modelCON_Contact.Gender = dr["Gender"].ToString();
                    modelCON_Contact.TypeOfProfessio = dr["TypeOfProfessio"].ToString();
                    modelCON_Contact.CompanyName = dr["CompanyName"].ToString();
                    modelCON_Contact.Designation = dr["Designation"].ToString();
                    modelCON_Contact.ContactCategoryID = Convert.ToInt32(dr["ContactCategoryID"]);
                    modelCON_Contact.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                    modelCON_Contact.ModificationDate = Convert.ToDateTime(dr["ModificationDate"]);
                }
                return View("CON_ContactAddEdit", modelCON_Contact);
            }
            return View("CON_ContactAddEdit");
        }
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

        public IActionResult DropDownByState(int StateID)
        {
            #region DropDownForState
            string str2 = this.Configuration.GetConnectionString("myConnectionString");

            //Prepare Connection
            SqlConnection connection = new SqlConnection(str2);
            connection.Open();

            //Prepare Command
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_LOC_City_SelectComboBoxByStateID";
            command.Parameters.AddWithValue("@StateID", StateID);
            DataTable table = new DataTable();
            SqlDataReader reader = command.ExecuteReader();
            table.Load(reader);

            List<LOC_CityDropDownModel> list2 = new List<LOC_CityDropDownModel>();
            foreach (DataRow row in table.Rows)
            {
                LOC_CityDropDownModel lst = new LOC_CityDropDownModel();
                lst.CityID = Convert.ToInt32(row["CityID"]);
                lst.CityName = (string)row["CityName"];
                list2.Add(lst);
            }

            var vmodel = list2;
            return Json(vmodel);
            #endregion DropDownForCity
        }
    }
}