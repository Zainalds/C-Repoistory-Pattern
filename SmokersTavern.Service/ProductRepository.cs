using SmokersTavern.Data;
using SmokersTavern.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//Zain
namespace SmokersTavern.Service
{
    public class ProductRepository : IProductRepository
    {
        private ApplicationDbContext _datacontext = null;
        private readonly IRepositoryService<Product> _productRepository;

        public ProductRepository()
        {
            _datacontext = new ApplicationDbContext();
            _productRepository = new RepositoryService<Product>(_datacontext);

        }

        public void Dispose()
        {
            _datacontext.Dispose();
            _datacontext = null;
        }

        public Product GetById(int id)
        {
            return _productRepository.GetById(id);
        }

        public List<Product> GetAll()
        {
            return _productRepository.GetAll().ToList();
        }

        public void Insert(Product model)
        {
            _productRepository.Insert(model);
        }

        public void Update(Product model)
        {
            _productRepository.Update(model);
        }

        public void Delete(Product model)
        {
            _productRepository.Delete(model);
        }
        public IEnumerable<Product> Find(Func<Product, bool> predicate)
        {
            return _productRepository.Find(predicate).ToList();
        }
    }
}
