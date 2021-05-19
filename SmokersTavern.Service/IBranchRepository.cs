using SmokersTavern.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokersTavern.Service
{
    public interface IBranchRepository : IDisposable
    {
        Branch GetById(Int32 id);
        List<Branch> GetAll();
        void Insert(Branch model);
        void Update(Branch model);
        void Delete(Branch model);
        IEnumerable<Branch> Find(Func<Branch, bool> predicate);
    }
}
