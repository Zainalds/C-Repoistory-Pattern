using SmokersTavern.Business.Business_Logic;
using SmokersTavern.Business.Interfaces;
using SmokersTavern.Data;
using SmokersTavern.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;


//21520289
namespace SmokersTavern.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        private IOrderBusiness _ob;
        ApplicationDbContext _context = new ApplicationDbContext();

        public CartController()
        {
            _ob = new OrderBusiness();
        }

        public CartController(IOrderBusiness ordiz)
        {
            _ob = ordiz;
        }
        // GET: Cart
        public ActionResult Cart()
        {
            return View();
        }
        public ActionResult Cancel(OrderViewModel model)
        {
            model.OrderId = _ob.GetAllOrder().Last(x => x.Username.Equals(HttpContext.User.Identity.Name)).OrderId;
            _ob.PostDeleteMethod(model.OrderId);
            return View();
        }
        public ActionResult Success(OrderViewModel model)
        {
            model.Username = User.Identity.Name;
            EmailBusiness obj = new EmailBusiness();
            obj.to = new MailAddress(model.Username);
            obj.body = "Hi " + model.Username + " Thank you for purchasing at Smokers Tavern, we have recieved your payment" + "." + "<br/>Please print your order online and produce it as your proof of payment" + "<br/><br/>Kindest Regards<br/>Smokers Tavern";
            ViewBag.feed = obj.Online();
            return View();
        }
    }
}