using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//Shivani Reddy
namespace SmokersTavern.Model
{
    public class ProductArchiveViewModel
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
        [Range(10, 1000, ErrorMessage = "Please enter a value between R10 and R1000")]
        [RegularExpression("([1-1000000][0-1000000]*)", ErrorMessage = "Count must be a natural number")]
        public int ProductQuantity { get; set; }
        [Required]
        [Display(Name = "Product Price")]
        [DisplayFormat(DataFormatString = "R{0:#,###0.00}")]
        [Range(10, 1000, ErrorMessage = "Please enter a value between R10 and R1000")]
        [RegularExpression("([1-1000000][0-1000000]*)", ErrorMessage = "Count must be a natural number")]
        public decimal ProductPrice { get; set; }
        [Required]
        [Display(Name = "Product Cost Price")]
        [DisplayFormat(DataFormatString = "R{0:#,###0.00}")]
        [Range(10, 1000, ErrorMessage = "Please enter a value between R10 and R1000")]
        [RegularExpression("([1-1000000][0-1000000]*)", ErrorMessage = "Count must be a natural number")]
        public decimal ProductCostPrice { get; set; }
        [Required]
        [Display(Name = "Upload File")]
        public string ImageUrl { get; set; }

        public DateTime DateDeleted { get; set; }


        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public virtual CategoryViewModel Category { get; set; }

        [ForeignKey("Branch")]
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public virtual BranchViewModel Branch { get; set; }
    }
}
