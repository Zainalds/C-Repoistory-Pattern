//Shreshtha
using SmokersTavern.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SmokersTavern.Business.Interfaces
{
    public interface ISupplierBusiness
    {
        IEnumerable<SupplierViewModel> GetAllSuppliers();
        void Insert(SupplierViewModel model);
        void Update(SupplierViewModel model);
        SupplierViewModel GetById(Int32 id);

        void Delete(int id);
        IEnumerable<SupplierViewModel> GetAllProducts(int id);
        ProductViewModel GetByPoductId(int id);
        List<ProductViewModel> GetProductTypes();
        void RestoreSupplier(int id);
    }
}
