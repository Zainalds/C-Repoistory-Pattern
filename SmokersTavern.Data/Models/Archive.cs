using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//Zain
namespace SmokersTavern.Data.Models
{
    public class Archive
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
        [Required]
        [Display(Name = "Product Description")]
        public string ProductDescription { get; set; }
        [Required]
        [Display(Name = "Brand Name")]
        public string BrandName { get; set; }
        [Required]
        [Display(Name = "Product Color")]
        public string ProductColor { get; set; }
        [Required]
        [Display(Name = "Quantity")]
        public int ProductQuantity { get; set; }
        [Required]
        [Display(Name = "Product Price")]
        public decimal ProductPrice { get; set; }
        [Required]
        [Display(Name = "Product Price")]
        public decimal ProductCostPrice { get; set; }

        [Required]
        [Display(Name = "Upload File")]
        public string ImageUrl { get; set; }

        public DateTime DateDeleted { get; set;}

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public virtual Category Category { get; set; }

        [ForeignKey("Branch")]
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public virtual Branch Branch { get; set; }
    }
}
