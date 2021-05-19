using SmokersTavern.Data;
using SmokersTavern.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokersTavern.Service
{

    //Zain
    public class ArchiveRepository : IArchiveRepository
    {
        private ApplicationDbContext _datacontext = null;
        private readonly IRepositoryService<Archive> _archiveRepository;

        public ArchiveRepository()
        {
            _datacontext = new ApplicationDbContext();
            _archiveRepository = new RepositoryService<Archive>(_datacontext);

        }

        public void Dispose()
        {
            _datacontext.Dispose();
            _datacontext = null;
        }
        public List<Archive> GetAll()
        {
            return _archiveRepository.GetAll().ToList();
        }

        public void Insert(Archive model)
        {
            _archiveRepository.Insert(model);
        }

        public Archive GetById(int id)
        {
            return _archiveRepository.GetById(id);
        }
        public void Delete(Archive model)
        {
            _archiveRepository.Delete(model);
        }
        public IEnumerable<Archive> Find(Func<Archive, bool> predicate)
        {
            return _archiveRepository.Find(predicate).ToList();
        }
    }
}
