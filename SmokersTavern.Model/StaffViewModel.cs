//Romell

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmokersTavern.Data;

namespace SmokersTavern.Model
{
    public class StaffViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Username")]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Numbers and special characters are not allowed.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Numbers and special characters are not allowed.")]
        [Required]
        public string Surname { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Position")]
        public string Position { get; set; }

        [Required]
        [Display(Name = "Type Of User")]
        public string Role { get; set; }

        [Required]
        [RegularExpression(@"^\(?([0]{1})\)?[-. ]?([1-9]{1})[-. ]?([0-9]{8})$", ErrorMessage = "Invalid Number")]
        [StringLength(10)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Cellphone Number")]
        public string Cellphone { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
