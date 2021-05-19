using SmokersTavern.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//Shivani Reddy
namespace SmokersTavern.Service
{
    public interface IProductRepository :IDisposable
    {
        Product GetById(Int32 id);
        List<Product> GetAll();
        void Insert(Product model);
        void Update(Product model);
        void Delete(Product model);
        IEnumerable<Product> Find(Func<Product, bool> predicate);
    }
}
