//Shreshtha
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokersTavern.Model
{
    public class SupplierViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string ClientId { get; set; }

        [Required]
        [Display(Name = "Supplier Name")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Numbers and special characters are not allowed.")]
        public string name { get; set; }


        [Required]
        [Display(Name = "Physical Address")]
        [DataType(DataType.MultilineText)]
        public string physicalAddress { get; set; }


        [Required]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string emailAddress { get; set; }


        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Only 10 digits allowed.")]
        [StringLength(10)]
        [Display(Name = "Contact Number")]
        [DataType(DataType.PhoneNumber)]
        public string phoneNo { get; set; }

    }
}
