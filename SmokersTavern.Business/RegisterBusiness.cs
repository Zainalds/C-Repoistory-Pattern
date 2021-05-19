using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmokersTavern.Data;
using SmokersTavern.Model;
using SmokersTavern.Service;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

//Zain
namespace SmokersTavern.Business
{
    public class RegisterBusiness
    {
        public UserManager<ApplicationUser> UserManager { get; set; }

        public RegisterBusiness()
        {
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        }

        public bool FindUser(string email, IAuthenticationManager authenticationManager)
        {
            var user = UserManager.FindByEmail(email);
            if (user != null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> RegisterUser(RegisterModel objRegisterModel, IAuthenticationManager authenticationManager)
        {
            var newuser = new ApplicationUser()
            {
                Id = objRegisterModel.Email,
                UserName = objRegisterModel.Email,
                Email = objRegisterModel.Email
            };

            var result = await UserManager.CreateAsync(newuser, objRegisterModel.Password);

            if (result.Succeeded)
            {
                using (var customerRepo = new CustomerRepository(new ApplicationDbContext()))
                {
                    var cust = new Customer()
                    {
                        Email = objRegisterModel.Email,
                        FirstMidName = objRegisterModel.FirstMidName,
                        Surname = objRegisterModel.Surname,
                        Address = objRegisterModel.Address,
                        CellNo = objRegisterModel.CellNo
                    };
                    customerRepo.InsertCustomer(cust);
                    customerRepo.Save();
                }
                await SignInAsync(newuser, true, authenticationManager);
                return true;
            }
            return false;
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent, IAuthenticationManager authenticationManager)
        {
            authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        public async Task<bool> RegisterUserAddLoginAsync(ApplicationUser newuser, ExternalLoginInfo loginInfo, Customer cust, IAuthenticationManager authenticationManager)
        {
            var result = await UserManager.CreateAsync(newuser);

            if (result.Succeeded)
            {
                result = await UserManager.AddLoginAsync(newuser.Id, loginInfo.Login);

                if (result.Succeeded)
                {
                    using (var customerRepo = new CustomerRepository(new ApplicationDbContext()))
                    {
                        customerRepo.InsertCustomer(cust);
                        customerRepo.Save();
                    }
                    await SignInAsync(newuser, true, authenticationManager);
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}
