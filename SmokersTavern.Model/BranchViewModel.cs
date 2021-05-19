//Shreshtha
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokersTavern.Model
{
    public class BranchViewModel
    {
        [Key]
        public int BranchId { get; set; }
        [Required]
        [Display(Name = "Branch Name")]
        public string BranchName { get; set; }

        [Required]
        [Display(Name = "Branch Manager")]
        public string BranchManager { get; set; }

        [Required]
        [Display(Name = "Branch Address")]
        public string BranchAddress { get; set; }

        [Required]
        [Display(Name = "Branch Contact Number")]
        public string BranchContactNumber { get; set; }

        [Required]
        [Display(Name = "Branch Email")]
        public string BranchEmail { get; set; }
    }
}
