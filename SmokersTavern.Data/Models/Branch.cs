//Shreshtha
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokersTavern.Data.Models
{
    public class Branch
    {
        [Key]
        public int BranchId { get; set; }
        [Required]
        [Display(Name = "Branch Name")]
        public string BranchName { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Numbers and special characters are not allowed.")]
        [Display(Name = "Bracnh Manager")]
        public string BranchManager { get; set; }

        [Required]
        [Display(Name = "Branch Address")]
        public string BranchAddress { get; set; }

        [Required]
        [RegularExpression(@"^\(?([0]{1})\)?[-. ]?([1-9]{1})[-. ]?([0-9]{8})$", ErrorMessage = "Invalid Number")]
        [StringLength(10)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Branch Contact Number")]
        public string BranchContactNumber { get; set; }

        [Required]
        [Display(Name = "Branch Email")]
        [EmailAddress]
        public string BranchEmail { get; set; }
    }
}
