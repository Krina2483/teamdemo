using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AddressBookDemo.Models
{
    public class LOC_StateModel
    {
        public int? StateID { get; set; }


        [Required(ErrorMessage = "Please enter State Name")]
        [DisplayName("StateName")]
        [StringLength(20, MinimumLength = 3)]
        public string? StateName { get; set; }
        [Required(ErrorMessage = "Please enter State Code")]
        public string? StateCode { get; set; }
        [Required(ErrorMessage = "Select Country Name")]
        [DisplayName("Country")]
        public int CountryID { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
    }
    public class LOC_StateDropDownModel
    {
        public int StateID { get; set; }
        public string? StateName { get; set; }
    }
}
