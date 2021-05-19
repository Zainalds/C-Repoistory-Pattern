//Romell

using SmokersTavern.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokersTavern.Service
{
    public interface IStaffArchive
    {
        List<Staff> GetAll();
        void Insert(Staff model);
        Staff GetById(Int32 id);
        void Delete(Staff model);
        IEnumerable<Staff> Find(Func<Staff, bool> predicate);
    }
}
