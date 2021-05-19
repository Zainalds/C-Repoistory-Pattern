using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SmokersTavern.Model;
using SmokersTavern.Business;
using Microsoft.AspNet.Identity;
using System.Net.Mail;
using SmokersTavern.Business.Business_Logic;


//Zain
namespace SmokersTavern.Controllers
{
    public class RegisterController : Controller
    {
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        // GET: Register

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Register(RegisterModel objRegisterModel)
        {


            TempData["PostalAddress"] = objRegisterModel.Address;
            try
            {
                var registerbusiness = new RegisterBusiness();
                var obj = new EmailBusiness();

                if (registerbusiness.FindUser(objRegisterModel.Email, AuthenticationManager))
                {
                    ModelState.AddModelError("", "User already exists");
                    return View(objRegisterModel);
                }


                var result = await registerbusiness.RegisterUser(objRegisterModel, AuthenticationManager);


                if (result)
                {
                    obj.to = new MailAddress(objRegisterModel.Email);
                    obj.body = "Hi " + " " + objRegisterModel.FirstMidName + "<br/>" + "Registration Complete." + " " + "Details Are as follows:<br/><br/>" + "Username: " + " " + objRegisterModel.Email + "<br/>Password: " + " " + objRegisterModel.Password + "<br/><br/>Kind Regards<br/>Smokers Tavern";
                    ViewBag.feed = obj.NewRegistration();
                    return RedirectToAction("Index", "Customer");
                }
                else
                {
                    ModelState.AddModelError("", result.ToString());
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting  
                        // the current instance as InnerException  
                        raise = new InvalidOperationException(message, raise);
                    }
                }
            }
            return View(objRegisterModel);
        }
    }
}