using SmokersTavern.Business.Business_Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;


//Zain
namespace SmokersTavern.Controllers
{
    public class AdminController : Controller
    {
        CustomerBusiness customerBusiness = new CustomerBusiness();
        // GET: Customer
        public ActionResult Index(int? page)
        {
            var model = customerBusiness.GetAllCustomers();
            return View(model.ToPagedList(pageNumber: page ?? 1, pageSize: 20));
        }

        [HttpPost]
        public ActionResult Index(string name, int? page)
        {
            page = 1;

            if (!string.IsNullOrEmpty(name))
            {
                ViewBag.Result = true;
                var pglist = customerBusiness.GetAllCustomers()
                    .OrderBy(X => X.Email)
                    .Where(x => x.FirstMidName.ToLower() == name.ToLower() || x.FirstMidName.ToLower().StartsWith(name.ToLower()))
                    .ToPagedList(pageNumber: page ?? 1, pageSize: 20);

                if (pglist.Count == 0)
                {
                    TempData["custempty"] = "No Customers found";
                }
                return View(pglist);
            }
            else
            {
                TempData["custname"] = "Invalid Entry";
                return View(new List<Data.Customer>().ToPagedList(pageNumber: page ?? 1, pageSize: 20));
            }
        }

        // GET: Staff/Details/5
        public ActionResult Details(string email)
        {
            var model = customerBusiness.GetCustomer(email);
            return View(model);
        }
        public ActionResult Search()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Search(string name)
        {


            if (!string.IsNullOrEmpty(name))
            {
                name = name.ToLower();
                ViewBag.Result = true;
                return View(customerBusiness.GetAllCustomers().OrderBy(X => X.Email).Where(x => x.Email == name || x.Email.StartsWith(name)));
            }

            ModelState.AddModelError("", "No primary member found matching the searched criteria");
            return View();
        }
    }
}
