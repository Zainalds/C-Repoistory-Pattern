using SmokersTavern.Data;
using SmokersTavern.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokersTavern.Service
{
    public class SupplierRepository : ISupplierRepository
    {
        private ApplicationDbContext _datacontext = null;
        private readonly IRepositoryService<Supplier> _supplierRepository;

        public SupplierRepository()
        {
            _datacontext = new ApplicationDbContext();
            _supplierRepository = new RepositoryService<Supplier>(_datacontext);

        }

        public void Dispose()
        {
            _datacontext.Dispose();
            _datacontext = null;
        }

        public Supplier GetById(int id)
        {
            return _supplierRepository.GetById(id);
        }

        public List<Supplier> GetAll()
        {
            return _supplierRepository.GetAll().ToList();
        }

        public void Insert(Supplier model)
        {
            _supplierRepository.Insert(model);
        }

        public void Update(Supplier model)
        {
            _supplierRepository.Update(model);
        }

        public void Delete(Supplier model)
        {
            _supplierRepository.Delete(model);
        }

        public IEnumerable<Supplier> Find(Func<Supplier, bool> predicate)
        {
            return _supplierRepository.Find(predicate).ToList();
        }
    }
}
