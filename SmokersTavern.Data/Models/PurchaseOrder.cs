//Shreshtha
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokersTavern.Data.Models
{
    public class PurchaseOrder
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ClientId { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Numbers and special characters are not allowed.")]
        public string ContactPerson { get; set; }
        [Required]
        public string ContactNumber { get; set; }
        [Required]
        public string DeliveryAddress { get; set; }
        public DateTime CreateTime { get; set; }

        //[ForeignKey("Supplier")]
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        //public virtual Supplier Supplier { get; set; }
    }
}
