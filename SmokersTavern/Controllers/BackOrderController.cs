using SmokersTavern.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SmokersTavern.Controllers
{
    public class BackOrderController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();


        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.abc = new SelectList(db.Suppliers.ToList(), "Id", "SupplierName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BackOrder model)
        {
            ViewBag.abc = new SelectList(db.Suppliers.ToList(), "Id", "SupplierName");


            Guid guid = Guid.NewGuid();
            string id = guid.ToString();

            int prodId = Convert.ToInt32(model.SupplierId);

            ViewBag.l = prodId;

            if (id != null)
            {
                var name = (from x in db.Suppliers
                            where x.Id == prodId
                            select x).ToList();

                foreach (var item in name)
                {
                    ViewBag.ln = item.SupplierName;
                    var newItem = new BackOrder()
                    {
                        ClientId = id,
                        ContactPerson = model.ContactPerson,
                        ContactNumber = model.ContactNumber,
                        DeliveryAddress = model.DeliveryAddress,
                        CreateTime = DateTime.Now,
                        amntOutstanding = model.amntOutstanding,
                        amntAvailable = model.amntAvailable,
                        SupplierId = model.SupplierId,
                        SupplierName = item.SupplierName
                    };
                    db.BackOrders.Add(newItem);
                    db.SaveChanges();
                }
            }
            ModelState.Clear();
            return View();
        }

        [HttpGet]
        public ActionResult DeleteBackOrder(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BackOrder a = db.BackOrder.Find(Id);
            if (Detail == null)
            {
                return HttpNotFound();
            }
            return View(a);
        }
        public ActionResult Invoice(string ClientId)
        {

            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
            var stringChars = new char[5];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var final = new String(stringChars);

            var q = from x in db.BackOrders
                    join y in db.PurchaseOrders on x.ClientId equals y.ClientId
                    where x.ClientId == ClientId
                    select x;


            var query = (from x in db.BackOrders
                         join y in db.PurchaseOrders on x.ClientId equals y.ClientId
                         where x.ClientId == ClientId
                         select new BackOrdersViewModel()
                         {
                             Quantity = y.Quantity,
                             OrderContactPerson = x.SupplierName,
                             OrderContactNumber = x.ContactNumber,
                             OrderDeliveryAddress = x.DeliveryAddress,
                             amntAvailable = x.amntAvailable,
                             amntOutstanding = x.amntOutstanding,
                             ProductName = y.ProductName,
                             ProductPrice = y.ProductCostPrice,
                             LinePrice = y.ProductCostPrice * y.Quantity,
                             PrintTime = DateTime.Now

                         }).ToList();

            return new Rotativa.MVC.ViewAsPdf("InvoicePDF", query);
        }

        public ActionResult Index()
        {
            return View();
        }
	}
}