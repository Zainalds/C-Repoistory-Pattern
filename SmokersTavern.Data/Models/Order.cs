using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokersTavern.Data.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Oder Id")]
        public int OrderId { get; set; }

        [Display(Name = "Username")]
        public string Username { get; set; }

        [Display(Name = "Your Product(s)")]
        public string Products { get; set; }

        [Display(Name = "Total")]
        [DataType(DataType.Currency)]
        public decimal Total { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Time")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Display(Name ="Postal Address")]
        public string OrderPostalAddress { get; set; }
    }
}
