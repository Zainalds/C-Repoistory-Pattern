using SmokersTavern.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//Shivani Reddy
namespace SmokersTavern.Business.Interfaces
{
    public interface IProductBusiness
    {
        IEnumerable<ProductViewModel> GetAll();
        void Insert(ProductViewModel model);
        ProductViewModel GetById(int Id);
        void Update(ProductViewModel model);
        void Delete(int id);
        void DeleteCategory(int id);
        IEnumerable<CategoryViewModel> GetAllCategories();
        void InsertCategory(CategoryViewModel model);
        void UpdateCategory(CategoryViewModel model);
        CategoryViewModel GetByCategoryId(int Id);

        void InsertProductArchive(ProductArchiveViewModel model);

        IEnumerable<ProductArchiveViewModel> GetProductArchive();
        IEnumerable<CategoryArchiveViewModel> GetCategoryArchive();

        ProductArchiveViewModel GetProductArchiveById(int Id);
        void RestoreProduct(int id);

        IEnumerable<ProductArchiveViewModel> GetOutOfStock();

        IEnumerable<ProductViewModel> GetPriceGraph();
        IEnumerable<ProductViewModel> GetStockPerBranchGraph(int id);
    }
}
