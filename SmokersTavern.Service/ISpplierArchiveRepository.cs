using SmokersTavern.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokersTavern.Service
{
    public interface ISpplierArchiveRepository : IDisposable
    {
        void Insert(ArchiveSupplier model);
        ArchiveSupplier GetById(Int32 id);
        List<ArchiveSupplier> GetAll();
        void Update(ArchiveSupplier model);
        void Delete(ArchiveSupplier model);
        IEnumerable<ArchiveSupplier> Find(Func<ArchiveSupplier, bool> predicate);
    }
}
