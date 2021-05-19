using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokersTavern.Model
{
    public class TopSellers
    {
        public string ProductPurchaseName { get; set; }
        public int ProductQuantity { get; set; }
        public string ImageUrl { get; set; }
        public decimal ProductPrice { get; set; }
    }
}
