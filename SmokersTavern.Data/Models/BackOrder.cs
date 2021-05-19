using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmokersTavern.Controllers
{
    public class BackOrder
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

         [Required]
        public string amntAvailable { get; set; }
         [Required]
        public string amntOutstanding { get; set; }

        //[ForeignKey("Supplier")]
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
    }
}