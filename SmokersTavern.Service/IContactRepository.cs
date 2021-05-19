using SmokersTavern.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//Zain
namespace SmokersTavern.Service
{
    public interface IContactRepository : IDisposable
    {
        Contact GetById(Int32 id);
        List<Contact> GetAll();
        void Insert(Contact model);
        void Update(Contact model);
        void Delete(Contact model);
        IEnumerable<Contact> Find(Func<Contact, bool> predicate);
    }
}
