//Vishalen
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokersTavern.Model
{
   public class OrderViewModel
    {
        [Key]
        [Display(Name = "Oder Id")]
        public int OrderId { get; set; }

        [Display(Name = "Username")]
        public string Username { get; set; }

        [Display(Name = "Product(s)")]
        public string Products { get; set; }

        [DisplayFormat(DataFormatString = "R{0:#,###0.00}")]
        [Range(10, 1000, ErrorMessage = "Please enter a value between R10 and R1000")]
        [Display(Name = "Total")]
        public decimal Total { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Time")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Display(Name = "Postal Address")]
        public string OrderPostalAddress { get; set; }
    }
}
