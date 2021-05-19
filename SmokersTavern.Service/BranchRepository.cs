//Shreshtha
using SmokersTavern.Data;
using SmokersTavern.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokersTavern.Service
{
    public class BranchRepository : IBranchRepository
    {
        private ApplicationDbContext _datacontext = null;
        private readonly IRepositoryService<Branch> _productRepository;

        public BranchRepository()
        {
            _datacontext = new ApplicationDbContext();
            _productRepository = new RepositoryService<Branch>(_datacontext);

        }

        public void Dispose()
        {
            _datacontext.Dispose();
            _datacontext = null;
        }

        public Branch GetById(int id)
        {
            return _productRepository.GetById(id);
        }

        public List<Branch> GetAll()
        {
            return _productRepository.GetAll().ToList();
        }

        public void Insert(Branch model)
        {
            _productRepository.Insert(model);
        }

        public void Update(Branch model)
        {
            _productRepository.Update(model);
        }

        public void Delete(Branch model)
        {
            _productRepository.Delete(model);
        }
        public IEnumerable<Branch> Find(Func<Branch, bool> predicate)
        {
            return _productRepository.Find(predicate).ToList();
        }
    }
}
