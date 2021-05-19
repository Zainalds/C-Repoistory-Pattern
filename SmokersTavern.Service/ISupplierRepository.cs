//Shreshtha
using SmokersTavern.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokersTavern.Service
{
    public interface ISupplierRepository : IDisposable
    {
        Supplier GetById(Int32 id);
        List<Supplier> GetAll();
        void Insert(Supplier model);
        void Update(Supplier model);
        void Delete(Supplier model);
        IEnumerable<Supplier> Find(Func<Supplier, bool> predicate);
    }
}
