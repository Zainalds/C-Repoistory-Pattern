using SmokersTavern.Business.Business_Logic;
using SmokersTavern.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;


//Zain
namespace SmokersTavern.Controllers
{
    public class ContactController : Controller
    {
        //
        // GET: /Contact/
        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Contact(ContactViewModel model)
        {
            var contactBusiness = new ContactBusiness();

            contactBusiness.Email(model);

            return View();
        }
	}
}