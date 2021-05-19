using SmokersTavern.Data;
using SmokersTavern.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using SmokersTavern.Model;
using System.Net;
using System.Data.Entity;
//Zain
namespace SmokersTavern.Controllers
{
    public class PurchaseOrderController : Controller
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
        public ActionResult Create(PurchaseOrder model)
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
                    var newItem = new PurchaseOrder()
                    {
                        ClientId = id,
                        ContactPerson = model.ContactPerson,
                        ContactNumber = model.ContactNumber,
                        DeliveryAddress = model.DeliveryAddress,
                        CreateTime = DateTime.Now,
                        SupplierId = model.SupplierId,
                        SupplierName = item.SupplierName
                    };
                    db.PurchaseOrders.Add(newItem);
                    db.SaveChanges();
                }
            }
            ModelState.Clear();
            return View();
        }

        [HttpGet]
        public ActionResult PurchaseItemsIndex(string ClientId)
        {
            var list = db.PurchaseItems.ToList().Where(x => x.ClientId == ClientId);
            return View(list);
        }

        //Edit children
        public ActionResult EditPurchaseOrder(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseItem Detail = db.PurchaseItems.Find(Id);
            if (Detail == null)
            {
                return HttpNotFound();
            }
            ViewBag.a = new SelectList(db.Products.ToList(), "Id", "ProductName");
            return View(Detail);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPurchaseOrder(PurchaseItem detail, string ClientId)
        {
            if (ModelState.IsValid)
            {
                ViewBag.a = new SelectList(db.Products.ToList(), "Id", "ProductName");

                
                if (detail != null)
                {
                    var name = (from x in db.Products
                                where x.Id == detail.ProductId
                                select x).ToList();

                    ViewBag.b = detail.ProductId;

                    PurchaseItem Detail = db.PurchaseItems.Find(detail.Id);

                    foreach (var item in name)
                    {                      

                        Detail.ClientId = ClientId;
                        Detail.Quantity = detail.Quantity;
                        Detail.ProductName = item.ProductName;
                        Detail.ProductCostPrice = item.ProductCostPrice;
                    }

                    db.Entry(Detail).State = EntityState.Modified;
                    db.SaveChanges();
                }

                string client = detail.ClientId;
                return RedirectToAction("PurchaseItemsIndex", "PurchaseOrder", new { ClientId = client });
            }
            return View(detail);
        }

        //delete child
        [HttpGet]
        public ActionResult DeletePurchaseItem(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseItem Detail = db.PurchaseItems.Find(Id);
            if (Detail == null)
            {
                return HttpNotFound();
            }
            return View(Detail);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePurchaseItem(int Id)
        {
            PurchaseItem detail = db.PurchaseItems.Find(Id);
            string client = detail.ClientId;

            db.PurchaseItems.Remove(detail);
            db.SaveChanges();
            return RedirectToAction("PurchaseItemsIndex", "PurchaseOrder", new { ClientId = client });
        }

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PriceSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;


            var product = from x in db.PurchaseOrders
                          select x;

            if (!String.IsNullOrEmpty(searchString))
            {
                product = product.Where(x => x.ContactPerson.ToUpper().Contains(searchString) || x.ContactPerson.ToLower().Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    product = product.OrderByDescending(p => p.ContactPerson);
                    break;

                case "Date":
                    product = product.OrderBy(p => p.CreateTime);
                    break;
                case "date_desc":
                    product = product.OrderByDescending(p => p.CreateTime);
                    break;
                default:
                    product = product.OrderBy(p => p.ContactPerson);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(product.ToPagedList(pageNumber, pageSize));
        }


        public ActionResult Invoice(string ClientId)
        {

            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
            var stringChars = new char[5];
            var random = new Random();

            for(int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var final = new String(stringChars);

            var q = from x in db.PurchaseOrders
                    join y in db.PurchaseItems on x.ClientId equals y.ClientId
                    where x.ClientId == ClientId
                    select x;


            var query = (from x in db.PurchaseOrders
                         join y in db.PurchaseItems on x.ClientId equals y.ClientId
                         where x.ClientId == ClientId
                         select new PuchaseOrderItemsViewModel()
                         {
                             Quantity = y.Quantity,
                             OrderContactPerson = x.SupplierName,
                             OrderContactNumber = x.ContactNumber,
                             OrderDeliveryAddress = x.DeliveryAddress,
                             OrderReferenceNumber = final,
                             ProductName = y.ProductName,
                             ProductPrice = y.ProductCostPrice,
                             LinePrice = y.ProductCostPrice * y.Quantity,
                             PrintTime = DateTime.Now

                         }).ToList();

            return new Rotativa.MVC.ViewAsPdf("InvoicePDF", query);
        }

    }
}