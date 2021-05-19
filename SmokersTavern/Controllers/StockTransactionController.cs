using PagedList;
using SmokersTavern.Business.Business_Logic;
using SmokersTavern.Business.Interfaces;
using SmokersTavern.Data;
using SmokersTavern.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmokersTavern.Controllers
{
    [Authorize]
    public class StockTransactionController : Controller
    {
        IStockTransactionBusiness _stockTransaction;
        ApplicationDbContext _context = new ApplicationDbContext();

        public StockTransactionController()
        {
            _stockTransaction = new StockTransactionBusiness();
        }

        public StockTransactionController(IStockTransactionBusiness model)
        {
            _stockTransaction = model;
        }

        [HttpGet]
        [Authorize(Roles = "Supervisor , Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Supplier/Create
        [HttpPost]
        [Authorize(Roles = "Supervisor , Admin")]
        public ActionResult Create(StockTransactionsViewModel model)
        {
            IStockTransactionBusiness business = new StockTransactionBusiness();
            if (ModelState.IsValid)
            {
                model.Date = System.DateTime.Now;
                business.DecreaseProductQuantity(model);
                _stockTransaction.Insert(model);
                return RedirectToAction("Index", "Branch");
            }

            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Supervisor , Admin")]
        public ActionResult Increase()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Supervisor , Admin")]
        public ActionResult Increase(StockTransactionsViewModel model)
        {
            IStockTransactionBusiness business = new StockTransactionBusiness();
            if (ModelState.IsValid)
            {
                model.Date = System.DateTime.Now;
                business.IncreaseQuantity(model);
                _stockTransaction.Insert(model);
                return RedirectToAction("Index", "Branch");
            }

            return View();
        }

        public ActionResult GetAllStockTransactions(string sortOrder, string currentFilter, string searchString, int? page)
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



            var business = new StockTransactionBusiness();
            var product = business.GetAllStockTransactions();

            if (!String.IsNullOrEmpty(searchString))
            {
                product = product.Where(x => x.ProductName.ToUpper().Contains(searchString) || x.ProductName.ToLower().Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    product = product.OrderByDescending(p => p.ProductName);
                    break;

                case "Date":
                    product = product.OrderBy(p => p.Date);
                    break;
                case "date_desc":
                    product = product.OrderByDescending(p => p.Date);
                    break;
                default:
                    product = product.OrderBy(p => p.ProductName);
                    break;
            }

            int pageSize = 50;
            int pageNumber = (page ?? 1);
            return View(product.ToPagedList(pageNumber, pageSize));
        }
    }
}