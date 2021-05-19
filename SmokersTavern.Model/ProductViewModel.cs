using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;


//Shivani Reddy
namespace SmokersTavern.Model
{
    public class ProductViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        [DisplayFormat(DataFormatString = "R{0:#,###0.00}")]
        [Range(10, 100000, ErrorMessage = "Please enter a value greater than R0")]
        public decimal ProductPrice { get; set; }
        [Required]
        [Display(Name = "Product Cost Price")]
        [DisplayFormat(DataFormatString = "R{0:#,###0.00}")]
        [Range(10, 100000, ErrorMessage = "Please enter a value greater than R0")]
        public decimal ProductCostPrice { get; set; }
        [Display(Name = "Upload File")]
        public string ImageUrl { get; set; }

        [Required]
        [Display(Name = "Category")]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public virtual CategoryViewModel Category { get; set; }

        [Required]
        [Display(Name = "Branch")]
        [ForeignKey("Branch")]
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public virtual BranchViewModel Branch { get; set; }

    }
}
