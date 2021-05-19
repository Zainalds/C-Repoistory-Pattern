using SmokersTavern.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//Zain
namespace SmokersTavern.Service
{
    public interface IArchiveRepository : IDisposable
    {
        List<Archive> GetAll();
        void Insert(Archive model);
        Archive GetById(Int32 id);
        void Delete(Archive model);
        IEnumerable<Archive> Find(Func<Archive, bool> predicate);
    }
}
