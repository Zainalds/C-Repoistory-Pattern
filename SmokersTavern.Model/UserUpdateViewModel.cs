using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//Zain
namespace SmokersTavern.Model
{
    public class UserUpdateViewModel
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Numbers and special characters are not allowed.")]
        [Display(Name = "First Name")]
        public string FirstMidName { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Numbers and special characters are not allowed.")]
        [Display(Name = "Surname")]
        public string Surname { get; set; }
        [Required]
        [Display(Name = "Postal Address")]
        public string Address { get; set; }
        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Only 10 digits allowed.")]
        [Display(Name = "Cellphone Number")]
        public string CellNo { get; set; }
    }
}
