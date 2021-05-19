using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmokersTavern.Data.Models;
using SmokersTavern.Data;


//Zain
namespace SmokersTavern.Service
{
    public interface ICustomerRepository : IDisposable
    {
        List<Customer> GetCustomers();
        Customer GetCustomerByEmail(string objEmail);
        void InsertCustomer(Customer model);
        void DeleteCustomer(Customer model);
        void UpdateCustomer(Customer model);
        void Save();
    }
}
