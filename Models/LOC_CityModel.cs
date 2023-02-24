using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AddressBookDemo.Models
{
    public class LOC_CityModel
    {
        public int? CityID { get; set; }
        [Required(ErrorMessage = "Please enter City Name")]
        [DisplayName("CityName")]
        [StringLength(20, MinimumLength = 3)]
        public string? CityName { get; set; }

        [Required(ErrorMessage = "Please enter City Code")]
        public string? CityCode { get; set; }
        [Required(ErrorMessage = "Please enter City Pincode")]
        public int? CityPincode { get; set; }
        [Required(ErrorMessage = "Select Country Name")]
        [DisplayName("Country")]
        public int? CountryID { get; set; }
        [Required(ErrorMessage = "Select State Name")]
        [DisplayName("State")]
        public int? StateID { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public string CountryName { get; internal set; }
        public string CountryCode { get; internal set; }
    }
    public class LOC_CityDropDownModel
    {
        public int CityID { get; set; }
        public string CityName { get; set; }
    }
}
