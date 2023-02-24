using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AddressBookDemo.Models
{
    public class LOC_CountryModel
    {
        public int? CountryID { get; set; }


        [Required(ErrorMessage = "Please enter Country Name")]
        [DisplayName("CountryName")]
        [StringLength(20, MinimumLength = 3)]
        public string? CountryName { get; set; }
        [Required(ErrorMessage = "Please enter Country Code")]
        public string? CountryCode { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
    }
    public class LOC_CountryDropDownModel
    {
        public int CountryID { get; set; }
        public string? CountryName { get; set; }
    }
}
