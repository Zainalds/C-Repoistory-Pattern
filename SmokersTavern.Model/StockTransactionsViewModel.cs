//Tryvynne
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokersTavern.Model
{
    public class StockTransactionsViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StockId { get; set; }
        public virtual ProductViewModel Product { get; set; }
        [ForeignKey("Product")]
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Reason { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
    }
}
