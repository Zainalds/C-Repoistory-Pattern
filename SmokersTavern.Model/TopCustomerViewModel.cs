using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokersTavern.Model
{
    public class TopCustomerViewModel
    {
        public string CustomerName { get; set; }
        public string CustomerEmailAddress { get; set; }
        public string CustomerContactNumber { get; set; }
        public int OrderCount { get; set; }
        public decimal OrderAmount { get; set; }
    }
}
