using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


//Shivani Manickum
namespace SmokersTavern.Model
{
    public class CategoryViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name="Category Name")]
        public string CategoryName { get; set; }
    }
}
