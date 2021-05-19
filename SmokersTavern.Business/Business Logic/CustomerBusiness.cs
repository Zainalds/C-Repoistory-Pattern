using SmokersTavern.Data;
using SmokersTavern.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//Vishalen
namespace SmokersTavern.Business.Business_Logic
{
    public class CustomerBusiness
    {
        public List<Customer> GetAllCustomers()
        {
            using (var customerRepo = new CustomerRepository(new ApplicationDbContext()))
            {
                return customerRepo.GetCustomers();
            }
        }

        public Customer GetCustomer(string email)
        {
            using (var customerRepo = new CustomerRepository(new ApplicationDbContext()))
            {
                return customerRepo.GetCustomerByEmail(email);
            }
        }
    }

}
