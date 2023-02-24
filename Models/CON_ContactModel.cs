using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System;

namespace AddressBookDemo.Models
{
    public class CON_ContactModel
    {
        public int? PersonID { get; set; }
        [Required(ErrorMessage = "Please enter Persone Name")]
        [DisplayName("PersoneName")]
        [StringLength(20, MinimumLength = 3)]
        public string? PersonName { get; set; }
        [Required(ErrorMessage = "Select City Name")]
        [DisplayName("City")]
        public int? CityID { get; set; }
        [Required(ErrorMessage = "Select Country Name")]
        [DisplayName("Country")]
        public int? CountryID { get; set; }
        [Required(ErrorMessage = "Select State Name")]
        [DisplayName("State")]
        public int? StateID { get; set; }
        
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public string StateName { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        public string? Mobile { get; set; }
        public string AlternetContact { get; set; }
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Your Email is not valid.")]
        public string? Email { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM.DD.YYYY}")]

        public DateTime? Date_Of_Birth { get; set; }
        [Required]
        public string Linkedln { get; set; }
        [Required]
        public string Twiiter { get; set; }
        [Required]
        public string Insta { get; set; }
        [Required]
        public string GitHub { get; set; }

        [Required(ErrorMessage = "Please enter Gender Name")]
        [DisplayName("Gender")]
        [StringLength(20, MinimumLength =3)]
        public string? Gender { get; set; }
        [Required(ErrorMessage = "Please enter  Type Of Profession")]
        [DisplayName(" TypeOfProfessio")]
        [StringLength(20, MinimumLength = 3)]
        public string? TypeOfProfessio { get; set; }
        [Required(ErrorMessage = "Please enter Company Name")]
        [DisplayName("CompanyName")]
        [StringLength(20, MinimumLength = 3)]
        public string? CompanyName { get; set; }
        [Required(ErrorMessage = "Please enter Designation")]
        [DisplayName("Designation")]
        [StringLength(20, MinimumLength = 3)]
        public string? Designation { get; set; }
        public string ContactCategoryName { get; set; }
        [Required(ErrorMessage = "Select ContactCategory Name")]
        [DisplayName("ContactCategory")]
        public int? ContactCategoryID { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
    }
  
}
