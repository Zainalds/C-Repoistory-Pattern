using PagedList;
using SmokersTavern.Data;
using SmokersTavern.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmokersTavern.Controllers
{
    public class DashboardController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var product = (from x in db.Sale
                           group x by new { x.ProductPurchaseName } into g
                           select new TopSellingProductViewModel()
                           {
                               ProductPurchaseName = g.Key.ProductPurchaseName,
                               ProductPurchaseQuantity = g.Sum(x => x.ProductPurchaseQuantity)

                           }).ToList().OrderByDescending(x => x.ProductPurchaseName);

            ViewBag.chart = product;
            return View();
        }

        public ActionResult GetChartData()
        {
            var product = (from x in db.Sale
                           group x by new { x.ProductPurchaseName } into g
                           select new TopSellingProductViewModel()
                           {
                               ProductPurchaseName = g.Key.ProductPurchaseName,
                               ProductPurchaseQuantity = g.Sum(x => x.ProductPurchaseQuantity)

                           }).ToList().OrderByDescending(x => x.ProductPurchaseName);

            return Json(product,JsonRequestBehavior.AllowGet);
        }
    }
}