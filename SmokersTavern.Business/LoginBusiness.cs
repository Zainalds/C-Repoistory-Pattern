using SmokersTavern.Data;
using SmokersTavern.Model;
using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Web.Http;
using SmokersTavern.Service;

//Zain
namespace SmokersTavern.Business
{
    public class LoginBusiness
    {
        public UserManager<ApplicationUser> UserManager { get; set; }

        public LoginBusiness()
        {
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        }

        public async Task<bool> LogUserIn(LoginModel objLoginModel, IAuthenticationManager authenticationManager)
        {
            var user = await UserManager.FindAsync(objLoginModel.Email, objLoginModel.Password);
            if (user != null)
            {
                await SignInAsync(user, objLoginModel.RememberMe, authenticationManager);
                return true;
            }
            return false;
        }

        public async Task SignInAsync(ApplicationUser user, bool isPersistent, IAuthenticationManager authenticationManager)
        {
            authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        public async Task<bool> FindUserAsync(ExternalLoginInfo Info, IAuthenticationManager authenticationManager)
        {
            var user = await UserManager.FindAsync(Info.Login);

            if (user != null)
            {
                await SignInAsync(user, true, authenticationManager);
                return true;
            }
            else
                return false;
        }

        public UserUpdateViewModel GetCurrentUser()
        {
            UserUpdateViewModel userupdate;
            var user = UserManager.FindById(HttpContext.Current.User.Identity.GetUserId());

            using (var customerRepo = new CustomerRepository(new ApplicationDbContext()))
            {
                var cust = customerRepo.GetCustomerByEmail(user.Email);
                userupdate = new UserUpdateViewModel
                {
                    FirstMidName = cust.FirstMidName,
                    Surname = cust.Surname,
                    Address = cust.Address,
                    CellNo = cust.CellNo
                };
            }
            return userupdate;
        }


        public async Task<bool> UpdateUser(UserUpdateViewModel objUpdateUser, IAuthenticationManager authenticationManager)
        {
            var user = UserManager.FindById(HttpContext.Current.User.Identity.GetUserId());
            var result = await UserManager.UpdateAsync(user);

            authenticationManager.SignOut();
            await SignInAsync(user, true, authenticationManager);
            if (result.Succeeded)
            {
                using (var customerRepo = new CustomerRepository(new ApplicationDbContext()))
                {
                    var cust = customerRepo.GetCustomerByEmail(user.Email);
                    cust.Address = objUpdateUser.Address;
                    cust.FirstMidName = objUpdateUser.FirstMidName;
                    cust.Surname = objUpdateUser.Surname;
                    cust.CellNo = objUpdateUser.CellNo;
                    customerRepo.UpdateCustomer(cust);
                    customerRepo.Save();
                }
                return true;
            }
            else
                return false;
        }
        public async Task<bool> ChangeUserPassword(PasswordUpdateViewModel objPasswordModel, IAuthenticationManager authenticationManager)
        {
            var result = await UserManager.ChangePasswordAsync(HttpContext.Current.User.Identity.GetUserId(), objPasswordModel.OldPassword, objPasswordModel.NewPassword);

            if (result.Succeeded)
            {
                var user = UserManager.FindById(HttpContext.Current.User.Identity.GetUserId());
                authenticationManager.SignOut();
                await SignInAsync(user, true, authenticationManager);
                return true;
            }
            else
                return false;

        }
    }
}
