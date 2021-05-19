
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokersTavern.Model
{
    public class SupplierArchiveViewModel
    {
        [Key]
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public string name { get; set; }
        public string physicalAddress { get; set; }
        public string emailAddress { get; set; }
        public string phoneNo { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public virtual ProductViewModel Product { get; set; }
    }
}
