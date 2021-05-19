//Vishalen 
using SmokersTavern.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokersTavern.Service
{
    public interface IStaffRepository : IDisposable
    {
        Staff GetById(Int32 id);
        List<Staff> GetAll();
        void Insert(Staff model);
        void Update(Staff model);
        void Delete(Staff model);
        IEnumerable<Staff> Find(Func<Staff, bool> predicate);
    }
}
