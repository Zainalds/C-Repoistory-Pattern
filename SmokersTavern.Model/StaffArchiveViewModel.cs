//Romell

using SmokersTavern.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokersTavern.Model
{
    public class StaffArchiveViewModel
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Username")]
        [DataType(DataType.EmailAddress)]
        [Required]
        public string UserName { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Numbers and special characters are not allowed.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Numbers and special characters are not allowed.")]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Display(Name = "Position")]
        public string Position { get; set; }
        [EmailAddress]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^\(?([0]{1})\)?[-. ]?([1-9]{1})[-. ]?([0-9]{8})$", ErrorMessage = "Invalid Number")]
        [StringLength(10)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Cellphone Number")]
        public string CellPhone { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }


        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
