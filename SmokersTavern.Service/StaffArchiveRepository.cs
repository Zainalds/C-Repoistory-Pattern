using SmokersTavern.Data;
using SmokersTavern.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokersTavern.Service
{
    public class StaffArchiveRepository : IStaffArchive
    {
        private ApplicationDbContext _datacontext = null;
        private readonly IRepositoryService<Staff> _archiveRepository;

        public StaffArchiveRepository()
        {
            _datacontext = new ApplicationDbContext();
            _archiveRepository = new RepositoryService<Staff>(_datacontext);

        }

        public void Dispose()
        {
            _datacontext.Dispose();
            _datacontext = null;
        }
        public List<Staff> GetAll()
        {
            return _archiveRepository.GetAll().ToList();
        }

        public void Insert(Staff model)
        {
            _archiveRepository.Insert(model);
        }

        public Staff GetById(int id)
        {
            return _archiveRepository.GetById(id);
        }
        public void Delete(Staff model)
        {
            _archiveRepository.Delete(model);
        }
        public IEnumerable<Staff> Find(Func<Staff, bool> predicate)
        {
            return _archiveRepository.Find(predicate).ToList();
        }
    }
}
