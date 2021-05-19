using SmokersTavern.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//Zain
namespace SmokersTavern.Service
{
    public interface ICategoryArchiveRepository : IDisposable
    {
        List<CategoryArchive> GetAll();
        void Insert(CategoryArchive model);
    }
}
