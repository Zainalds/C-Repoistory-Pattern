using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using SmokersTavern.Business;
using SmokersTavern.Business.Business_Logic;
using SmokersTavern.Data;
using SmokersTavern.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


//Zain
namespace SmokersTavern.Controllers
{
    public class LoginController : Controller
    {

        private UserManager<ApplicationUser> UserManager { get; set; }
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }

        // GET: Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            var loginview = new LoginModel();
            return View(loginview);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginModel model/*, string returnUrl*/)
        {
            if (ModelState.IsValid)
            {
                var loginbusiness = new LoginBusiness();
                var result = await loginbusiness.LogUserIn(model, AuthenticationManager);
                if (result && User.IsInRole("Admin"))
                {
                        return RedirectToAction("Index", "Product");
                }   
                else if(result && !User.IsInRole("Admin"))
                {
                    return RedirectToAction("Index", "Customer");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }
            return View(model);
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Login", new { ReturnUrl = returnUrl }));
        }

        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginbusiness = new LoginBusiness();
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login","Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await loginbusiness.FindUserAsync(loginInfo, AuthenticationManager);

            if (result)
                return RedirectToAction("Index", "Customer");
            else
            {
                // If the user does not have an account, then prompt the user to create an account
                ViewBag.ReturnUrl = returnUrl;
                ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                return View("ExternalLoginConfirmation", new ExternalLoginConfirmationModel { Email = loginInfo.Email });
            }
        }

        [AllowAnonymous]
        public ActionResult ExternalLoginCallbackRedirect(string returnUrl)
        {
            return RedirectPermanent("/Login/ExternalLoginCallback");
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationModel model, string returnUrl)
        {
            var registerbusiness = new RegisterBusiness();
            var loginbusiness = new LoginBusiness();
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };
                var customer = new Customer()
                {
                    Email = model.Email,
                    FirstMidName = model.FirstMidName,
                    Surname = model.Surname,
                    Address = model.Address,
                    CellNo = model.CellNo
                };

                var result = await registerbusiness.RegisterUserAddLoginAsync(user, info, customer, AuthenticationManager);
                if (result)
                {
                    return RedirectToAction("Index", "Customer");
                }
                else
                    RedirectToAction("Index", "Customer");
                    
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }
        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Customer");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Customer");
            }
        }

        public void AddErrors(IdentityResult result)
        {
            foreach(var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        [HttpGet]
        public ActionResult UpdateUser()
        {
            var loginbusiness = new LoginBusiness();
            var userupdate = loginbusiness.GetCurrentUser();
            return View(userupdate);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> UpdateUser(UserUpdateViewModel model)
        {
            var loginbusiness = new LoginBusiness();
            var result = await loginbusiness.UpdateUser(model, AuthenticationManager);
            if (result)
                TempData["UpdateUser_Success"] = "Your Account Update Was Successful";
            else
                TempData["UpdateUser_Failure"] = "Your Account Update Was Unsuccessful";
            return View(model);
        }

        [HttpGet]
        public ActionResult PasswordChange()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> PasswordChange(PasswordUpdateViewModel model)
        {

            var loginbusiness = new LoginBusiness();
            var obj = new EmailBusiness();
            

            var result = await loginbusiness.ChangeUserPassword(model, AuthenticationManager);

            if (result)
            {
                obj.to = new MailAddress(User.Identity.GetUserName());
                obj.body = " " + "Hi" +" "+ User.Identity.GetUserName() + "<br/>" + "Password Reset Successful." + " " + "Details Are as follows:<br/><br/>" + "Password: " + model.NewPassword + "<br/><br/>" + "https://smokerstavernonline.azurewebsites.net" + "<br/><br/>" + "Kind Regards<br/>Smokers Tavern";
                ViewBag.feed = obj.PasswordChange();

                TempData["ChangePassword_Success"] = "Password Change Successful";
            }
            else
                TempData["ChangePassword_Failure"] = "Password Change Unsuccessful";


            return View(model);
        }
        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            var obj = new EmailBusiness();
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);

                var token = UserManager.GeneratePasswordResetToken(user.UserName);
                // Generate the html link sent via email
                string resetLink = "<a href='"
                   + Url.Action("ResetPassword", "Login", new { rt = token }, "https")
                   + "'>Reset Password Link</a>";

                obj.to = new MailAddress(model.Email);
                obj.body = " " + "Hi" + " " + model.Email + "<br/>" + "Please reset your password by clicking here: <a href=\"" + resetLink + "\">link</a>";
                ViewBag.feed = obj.PasswordChange();

                return RedirectToAction("Login", "Login");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
    }
}