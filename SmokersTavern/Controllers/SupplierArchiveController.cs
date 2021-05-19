using SmokersTavern.Data;
using SmokersTavern.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SmokersTavern.Controllers
{
    public class SupplierArchiveController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public ActionResult Index()
        {
            var list = db.SupplierArchives.ToList();

            return View(list);
        }

        public ActionResult Restore(int? Id)
        {
            var db = new ApplicationDbContext();
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplierArchive Detail = db.SupplierArchives.Find(Id);
            if (Detail == null)
            {
                return HttpNotFound();
            }
            return View(Detail);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Restore(int Id, string ClientId)
        {
            var db = new ApplicationDbContext();
            SupplierArchive detail = db.SupplierArchives.Find(Id);
            string client = detail.ClientId;

            var newSupplier = new Supplier()
            {
                Id = detail.Id,
                ClientId = ClientId,
                SupplierName = detail.SupplierName,
                SupplierContactNumeber = detail.SupplierContactNumeber,
                SupplierEmail = detail.SupplierEmail,
                SupplierAddress = detail.SupplierAddress,
                SupplierContactPerson = detail.SupplierContactPerson,
                PaymentTerms = detail.PaymentTerms
            };

            db.Suppliers.Add(newSupplier);
            db.SupplierArchives.Remove(detail);
            db.SaveChanges();

            return RedirectToAction("Index", "SupplierArchive", new { ClientId = client });
        }
    }
}