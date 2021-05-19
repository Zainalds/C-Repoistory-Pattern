using PagedList;
using SmokersTavern.Data;
using SmokersTavern.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
//Zain
namespace SmokersTavern.Controllers
{
    public class SupplierController : Controller
    {
        [HttpGet]
        public ActionResult AddSupplier()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSupplier(Supplier model)
        {
            var db = new ApplicationDbContext();

            Guid guid = Guid.NewGuid();
            string id = guid.ToString();

                var newSupplier = new Supplier()
                {
                    ClientId = id,
                    SupplierName = model.SupplierName,
                    SupplierContactNumeber = model.SupplierContactNumeber,
                    SupplierEmail = model.SupplierEmail,
                    SupplierAddress = model.SupplierAddress,
                    SupplierContactPerson = model.SupplierContactPerson,
                    PaymentTerms = model.PaymentTerms

                };

                db.Suppliers.Add(newSupplier);
                db.SaveChanges();

            Session["suppId"] = id;
            Session["suppName"] = model.SupplierName;
            Session["suppContact"] = model.SupplierContactPerson;
            Session["suppNumber"] = model.SupplierContactNumeber;

            ModelState.Clear();
            return View();
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


            var db = new ApplicationDbContext();
            var product = from x in db.Suppliers
                          select x;

            if (!String.IsNullOrEmpty(searchString))
            {
                product = product.Where(x => x.SupplierName.ToUpper().Contains(searchString) || x.SupplierName.ToLower().Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    product = product.OrderByDescending(p => p.SupplierName);
                    break;

                case "Date":
                    product = product.OrderBy(p => p.SupplierContactNumeber);
                    break;
                case "date_desc":
                    product = product.OrderByDescending(p => p.SupplierContactNumeber);
                    break;
                default:
                    product = product.OrderBy(p => p.SupplierName);
                    break;
            }

            int pageSize = 50;
            int pageNumber = (page ?? 1);
            return View(product.ToPagedList(pageNumber, pageSize));
        }


        //Edit children
        public ActionResult Edit(int? Id)
        {
            var db = new ApplicationDbContext();
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier Detail = db.Suppliers.Find(Id);
            if (Detail == null)
            {
                return HttpNotFound();
            }
            return View(Detail);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Supplier detail, string ClientId)
        {
            if (ModelState.IsValid)
            {
                var db = new ApplicationDbContext();
                Supplier Detail = db.Suppliers.Find(detail.Id);


                Detail.ClientId = ClientId;
                Detail.SupplierName = detail.SupplierName;
                Detail.SupplierContactNumeber = detail.SupplierContactNumeber;
                Detail.SupplierEmail = detail.SupplierEmail;
                Detail.SupplierContactPerson = detail.SupplierContactPerson;
                Detail.PaymentTerms = detail.PaymentTerms;

                string client = detail.ClientId;

                db.Entry(Detail).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index", "Supplier", new { ClientId = client });
            }
            return View(detail);
        }

        public ActionResult Delete(int? Id)
        {
            var db = new ApplicationDbContext();
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier Detail = db.Suppliers.Find(Id);
            if (Detail == null)
            {
                return HttpNotFound();
            }
            return View(Detail);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int Id)
        {
            var db = new ApplicationDbContext();
            Supplier detail = db.Suppliers.Find(Id);
            string client = detail.ClientId;

            var newSupplierArchive = new SupplierArchive()
            {
                Id = detail.Id,
                ClientId = detail.ClientId,
                SupplierName = detail.SupplierName,
                SupplierContactNumeber = detail.SupplierContactNumeber,
                SupplierEmail = detail.SupplierEmail,
                SupplierAddress = detail.SupplierAddress,
                SupplierContactPerson = detail.SupplierContactPerson,
                PaymentTerms = detail.PaymentTerms
            };

            db.SupplierArchives.Add(newSupplierArchive);
            db.Suppliers.Remove(detail);
            db.SaveChanges();

            return RedirectToAction("Index", "Supplier", new { ClientId = client });
        }



    }
}