using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokersTavern.Data.Models
{
    public class SupplierArchive
    {
        [Key]
        public int Id { get; set; }
        public string ClientId { get; set; }
        [Display(Name = "Supplier Name")]
        public string SupplierName { get; set; }
        [Required]
        [RegularExpression(@"^\(?([0]{1})\)?[-. ]?([1-9]{1})[-. ]?([0-9]{8})$", ErrorMessage = "Invalid Number")]
        [StringLength(10)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Contact Number")]
        public string SupplierContactNumeber { get; set; }
        [EmailAddress]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string SupplierEmail { get; set; }
        [Display(Name = "Supplier Address")]
        public string SupplierAddress { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Numbers and special characters are not allowed.")]
        [Display(Name = "Surname")]
        public string SupplierContactPerson { get; set; }
        [Required]
        [Display(Name = "Payment Terms")]
        public string PaymentTerms { get; set; }
    }
}
