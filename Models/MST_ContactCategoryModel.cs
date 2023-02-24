using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AddressBookDemo.Models
{
    public class MST_ContactCategoryModel
    {
        public int? ContactCategoryID { get; set; }
        [Required(ErrorMessage = "Please enter ContactCategory Name")]
        [DisplayName("ContactCategoryName")]
        [StringLength(20, MinimumLength = 3)]
        public string? ContactCategoryName { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
    }
    public class MST_ContactCategoryDropDownModel
    {
        public int ContactCategoryID { get; set; }
        public string ContactCategoryName { get; set; }
    }
}
