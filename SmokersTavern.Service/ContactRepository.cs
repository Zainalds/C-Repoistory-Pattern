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
    public class ContactRepository
    {
       private ApplicationDbContext _datacontext = null;
        private readonly IRepositoryService<Contact> _contactRepository;

        public ContactRepository()
        {
            _datacontext = new ApplicationDbContext();
            _contactRepository = new RepositoryService<Contact>(_datacontext);

        }

        public void Dispose()
        {
            _datacontext.Dispose();
            _datacontext = null;
        }

        public Contact GetById(int id)
        {
            return _contactRepository.GetById(id);
        }

        public List<Contact> GetAll()
        {
            return _contactRepository.GetAll().ToList();
        }

        public void Insert(Contact model)
        {
            _contactRepository.Insert(model);
        }

        public void Update(Contact model)
        {
            _contactRepository.Update(model);
        }

        public void Delete(Contact model)
        {
            _contactRepository.Delete(model);
        }
        public IEnumerable<Contact> Find(Func<Contact, bool> predicate)
        {
            return _contactRepository.Find(predicate).ToList();
        }
    }
}
