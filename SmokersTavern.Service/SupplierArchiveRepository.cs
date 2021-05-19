using SmokersTavern.Data;
using SmokersTavern.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokersTavern.Service
{
    public class SupplierArchiveRepository : ISpplierArchiveRepository
    {
        private ApplicationDbContext _datacontext = null;
        private readonly IRepositoryService<ArchiveSupplier> _archiveSuppRepository;

        public SupplierArchiveRepository()
        {
            _datacontext = new ApplicationDbContext();
            _archiveSuppRepository = new RepositoryService<ArchiveSupplier>(_datacontext);

        }

        public void Insert(ArchiveSupplier model)
        {
            _archiveSuppRepository.Insert(model);
        }

        public ArchiveSupplier GetById(int id)
        {
            return _archiveSuppRepository.GetById(id);
        }

        public List<ArchiveSupplier> GetAll()
        {
            return _archiveSuppRepository.GetAll().ToList();
        }

        public void Update(ArchiveSupplier model)
        {
            _archiveSuppRepository.Update(model);
        }

        public void Delete(ArchiveSupplier model)
        {
            _archiveSuppRepository.Delete(model);
        }

        public IEnumerable<ArchiveSupplier> Find(Func<ArchiveSupplier, bool> predicate)
        {
            return _archiveSuppRepository.Find(predicate).ToList();
        }
        public void Dispose()
        {
            _datacontext.Dispose();
            _datacontext = null;
        }
    }
}
