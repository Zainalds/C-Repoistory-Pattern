using PagedList;
using SmokersTavern.Data;
using SmokersTavern.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//Zain
namespace SmokersTavern.Controllers
{
    public class ReportController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public ActionResult TopCustomers()
        {
            var product = from x in db.Orders
                          group x by new { x.Username } into g
                          select new TopCustomerViewModel()
                          {
                              CustomerEmailAddress = g.Key.Username,
                              OrderAmount = g.Sum(x => x.Total)
                          };

            return View(product.ToList());
        }

        [HttpPost]
        public ActionResult TopCustomers(string val)
        {

            Session["Date"] = val;
            ViewBag.a = val;
            //ViewBag.b = val2;

            var fromDate = Convert.ToDateTime(val);
            //var toDate = Convert.ToDateTime(val2);

            var product = from x in db.Orders
                          where x.Date == fromDate
                          group x by new { x.Username } into g
                          select new TopCustomerViewModel()
                          {
                              CustomerEmailAddress = g.Key.Username,
                              OrderAmount = g.Sum(x => x.Total)
                          };

            return View(product.ToList());
        }

        public ActionResult TopCustomersPDF()
        {
            string val = (string)Session["Date"];
            if (val != null)
            {
                ViewBag.a = val;
                //ViewBag.b = val2;

                var fromDate = Convert.ToDateTime(val);
                //var toDate = Convert.ToDateTime(val2);

                var product = (from x in db.Orders
                               where x.Date == fromDate
                               group x by new { x.Username } into g
                               select new TopCustomerViewModel()
                               {
                                   CustomerEmailAddress = g.Key.Username,
                                   OrderAmount = g.Sum(x => x.Total)

                               }).ToList();

                return new Rotativa.MVC.ViewAsPdf("TopCustomersPDF", product);
            }
            else
            {

                var product = (from x in db.Orders
                               group x by new { x.Username } into g
                               select new TopCustomerViewModel()
                               {
                                   CustomerEmailAddress = g.Key.Username,
                                   OrderAmount = g.Sum(x => x.Total)

                               }).ToList();

                return new Rotativa.MVC.ViewAsPdf("TopCustomersPDF", product);
            }

        }

        [WordDocument]
        public ActionResult TopCustomersWord()
        {
            ViewBag.WordDocumentFilename = "TopCustomersWord";

            var product = from x in db.Orders
                          group x by x.Username into g
                          select new TopCustomerViewModel()
                          {
                              CustomerEmailAddress = g.Key,
                              OrderAmount = g.Sum(x => x.Total)
                          };

            return View(product.ToList());
        }

        public ActionResult TopSellingProductPDF()
        {
            string val = (string)Session["d"];
            var fromDate = Convert.ToDateTime(val);

            if (val != null)
            {
                var product = (from x in db.Sale
                               where x.ProductPurchaseQuantity >= 5 && x.ProductPurchaseDate == fromDate
                               group x by new { x.ProductPurchaseName } into g
                               select new TopSellingProductViewModel()
                               {
                                   ProductPurchaseName = g.Key.ProductPurchaseName,
                                   ProductPurchaseQuantity = g.Sum(x => x.ProductPurchaseQuantity)

                               }).ToList().OrderByDescending(x => x.ProductPurchaseName);

                return new Rotativa.MVC.ViewAsPdf("TopSellingProductPDF", product);
            }
            else
            {
                var product = (from x in db.Sale
                               where x.ProductPurchaseQuantity >= 5
                               group x by new { x.ProductPurchaseName } into g
                               select new TopSellingProductViewModel()
                               {
                                   ProductPurchaseName = g.Key.ProductPurchaseName,
                                   ProductPurchaseQuantity = g.Sum(x => x.ProductPurchaseQuantity)

                               }).ToList().OrderByDescending(x => x.ProductPurchaseName);

                return new Rotativa.MVC.ViewAsPdf("TopSellingProductPDF", product);

            }
        }

        [HttpGet]
        public ActionResult TopSellingProduct(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var product = (from x in db.Sale
                           where x.ProductPurchaseQuantity >= 5 
                           group x by new { x.ProductPurchaseName } into g
                           select new TopSellingProductViewModel()
                           {
                               ProductPurchaseName = g.Key.ProductPurchaseName,
                               ProductPurchaseQuantity = g.Sum(x => x.ProductPurchaseQuantity)

                           }).ToList().OrderByDescending(x => x.ProductPurchaseName);

            ViewBag.chart = product;

            int pageSize = 50;
            int pageNumber = (page ?? 1);
            return View(product.ToPagedList(pageNumber, pageSize));
        }



        [HttpPost]
        public ActionResult TopSellingProduct(string sortOrder, string currentFilter, string searchString, int? page, string Val)
        {
            Session["d"] = Val;

            var fromDate = Convert.ToDateTime(Val);

            var product = (from x in db.Sale
                           where x.ProductPurchaseDate == fromDate && x.ProductPurchaseQuantity >= 5
                           group x by new { x.ProductPurchaseName } into g
                           select new TopSellingProductViewModel()
                           {
                               ProductPurchaseName = g.Key.ProductPurchaseName,
                               ProductPurchaseQuantity = g.Sum(x => x.ProductPurchaseQuantity)

                           }).ToList().OrderByDescending(x => x.ProductPurchaseName);

            ViewBag.chart = product;

            int pageSize = 50;
            int pageNumber = (page ?? 1);
            return View(product.ToPagedList(pageNumber, pageSize));
        }


        [WordDocument]
        public ActionResult DailySales(string sortOrder, string currentFilter, string searchString, int? page)
        {

            ViewBag.WordDocumentFilename = "DailySales";

            var data = (from z in db.Sale
                        select z.ProductPurchaseTotal).Sum();

            decimal Val = Convert.ToDecimal(data);

            var tp = (from x in db.Sale
                      select x.ProductPurchaseQuantity).Sum();

            int totalPurchase = Convert.ToInt32(tp);

            ViewBag.c = totalPurchase;
            ViewBag.a = Val;

            var product = (from x in db.Sale
                           group x by new { x.ProductPurchaseName } into g
                           select new TopSellingProductViewModel()
                           {
                               ProductPurchaseName = g.Key.ProductPurchaseName,
                               ProductPurchaseTotal = g.Sum(x => x.ProductPurchaseTotal),
                               ProductPurchaseQuantity = g.Sum(x => x.ProductPurchaseQuantity)
                           }).ToList().OrderBy(x => x.ProductPurchaseName);

            int pageSize = 50;
            int pageNumber = (page ?? 1);
            return View(product.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult DailySales(string sortOrder, string currentFilter, int? page)
        {
            var data = (from z in db.Sale
                        select z.ProductPurchaseTotal).Sum();

            decimal Val = Convert.ToDecimal(data);

            var tp = (from x in db.Sale
                      select x.ProductPurchaseQuantity).Sum();

            int totalPurchase = Convert.ToInt32(tp);

            ViewBag.c = totalPurchase;
            ViewBag.a = Val;

            var product = (from x in db.Sale
                          group x by new { x.ProductPurchaseName } into g
                          select new TopSellingProductViewModel()
                          {
                              ProductPurchaseName = g.Key.ProductPurchaseName,
                              ProductPurchaseTotal = g.Sum(x => x.ProductPurchaseTotal),
                              ProductPurchaseQuantity = g.Sum(x => x.ProductPurchaseQuantity)
                          }).ToList().OrderBy(x => x.ProductPurchaseName);

            int pageSize = 50;
            int pageNumber = (page ?? 1);
            return View(product.ToPagedList(pageNumber, pageSize));
        }
        [HttpPost]
        public ActionResult DailySales(string sortOrder, string currentFilter, string searchString, int? page,string val)
        {
            var fromDate = Convert.ToDateTime(val);

            var data = (from z in db.Sale
                        where z.ProductPurchaseDate == fromDate
                        select z.ProductPurchaseTotal).Sum();

            decimal Val = Convert.ToDecimal(data);

            var tp = (from x in db.Sale
                      where x.ProductPurchaseDate == fromDate
                      select x.ProductPurchaseQuantity).Sum();

            int totalPurchase = Convert.ToInt32(tp);

            ViewBag.c = totalPurchase;
            ViewBag.a = Val;

            var product = (from x in db.Sale
                           where x.ProductPurchaseDate == fromDate
                           group x by new { x.ProductPurchaseName } into g
                           select new TopSellingProductViewModel()
                           {
                               ProductPurchaseName = g.Key.ProductPurchaseName,
                               ProductPurchaseTotal = g.Sum(x => x.ProductPurchaseTotal),
                               ProductPurchaseQuantity = g.Sum(x => x.ProductPurchaseQuantity)

                           }).ToList().OrderBy(x => x.ProductPurchaseName);

            int pageSize = 50;
            int pageNumber = (page ?? 1);
            return View(product.ToPagedList(pageNumber, pageSize));
        }

    }
}