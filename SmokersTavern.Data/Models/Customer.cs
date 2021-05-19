using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


//Zain
namespace SmokersTavern.Data
{
    public class Customer
    {
        [Key]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Numbers and special characters are not allowed.")]
        [Display(Name = "First Name")]
        public string FirstMidName { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Numbers and special characters are not allowed.")]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [RegularExpression(@"^\d{10}$", ErrorMessage = "Only 10 digits allowed.")]
        [Display(Name = "Cellphone Number")]
        public string CellNo { get; set; }

    }
}
