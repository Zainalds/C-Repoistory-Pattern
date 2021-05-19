using SmokersTavern.Data;
using SmokersTavern.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokersTavern.Service
{
    public class StaffRepository : IStaffRepository
    {
        private ApplicationDbContext _datacontext = null;
        private readonly IRepositoryService<Staff> _staffRepository;

        public StaffRepository()
        {
            _datacontext = new ApplicationDbContext();
            _staffRepository = new RepositoryService<Staff>(_datacontext);

        }

        public void Dispose()
        {
            _datacontext.Dispose();
            _datacontext = null;
        }

        public Staff GetById(int id)
        {
            return _staffRepository.GetById(id);
        }

        public List<Staff> GetAll()
        {
            return _staffRepository.GetAll().ToList();
        }

        public void Insert(Staff model)
        {
            _staffRepository.Insert(model);
        }

        public void Update(Staff model)
        {
            _staffRepository.Update(model);
        }

        public void Delete(Staff model)
        {
            _staffRepository.Delete(model);
        }

        public IEnumerable<Staff> Find(Func<Staff, bool> predicate)
        {
            return _staffRepository.Find(predicate).ToList();
        }
    }
}
