using SmokersTavern.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Zain
namespace SmokersTavern.Business.Interfaces
{
    public interface ISupplierArchiveBusiness
    {
        IEnumerable<SupplierArchiveViewModel> GetAllArchives();
        SupplierArchiveViewModel GetById(int id);
        void Delete(int id);
    }
}
