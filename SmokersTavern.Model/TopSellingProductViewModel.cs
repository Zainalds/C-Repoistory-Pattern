using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokersTavern.Model
{
    public class TopSellingProductViewModel
    {

        public int Id { get; set; }
        public string ProductPurchaseName { get; set; }
        public int ProductPurchaseQuantity { get; set; }
        public decimal ProductPurchaseTotal { get; set; }
        public DateTime ProductPurchaseDate { get; set; }
        public decimal GrandTotal { get; set; }

      
    }
}
