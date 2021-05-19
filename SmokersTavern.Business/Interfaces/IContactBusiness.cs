using SmokersTavern.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//Shivani Reddy
namespace SmokersTavern.Business.Interfaces
{
    public interface IContactBusiness
    {
        IEnumerable<ContactViewModel> GetAll();
    }
}
