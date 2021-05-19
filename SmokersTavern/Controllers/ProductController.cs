using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmokersTavern.Business.Business_Logic;
using SmokersTavern.Model;
using SmokersTavern.Business.Interfaces;
using SmokersTavern.Data;
using SmokersTavern.Data.Models;
using System.Data.Entity;
using System.IO;
using PagedList;
using SmokersTavern.Service;
using Microsoft.Reporting.WebForms;


//Zain
namespace SmokersTavern.Controllers
{
   [Authorize]
    public class ProductController : Controller
    {

        IProductBusiness _productBusiness;
        ApplicationDbContext _context = new ApplicationDbContext();

        public ProductController()
        {
            _productBusiness = new ProductBusiness();

        }

        public ProductController(IProductBusiness model)
        {
            _productBusiness = model;
        }
        //
        // GET: /Product/
        [Authorize(Roles = "Supervisor , Admin")]
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
            var product = from x in db.Products
                          select x;

            if (!String.IsNullOrEmpty(searchString))
            {
                product = product.Where(x => x.ProductName.ToUpper().Contains(searchString) || x.ProductName.ToLower().Contains(searchString));
            }

            switch(sortOrder)
            {
                case "name_desc":
                    product = product.OrderByDescending(p => p.ProductName);
                    break;

                case "Date":
                    product = product.OrderBy(p => p.ProductPrice);
                    break;
                case "date_desc":
                    product = product.OrderByDescending(p => p.ProductPrice);
                    break;
                default:
                    product = product.OrderBy(p => p.ProductName);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(product.ToPagedList(pageNumber, pageSize));
        }

        [Authorize(Roles = "Supervisor , Admin")]
        [HttpGet]
        public ActionResult Create()
        {
            var BranchBusiness = new BranchBusiness();
            ViewBag.CategoryId = new SelectList(_productBusiness.GetAllCategories(), "Id", "CategoryName");
            ViewBag.BranchId = new SelectList(BranchBusiness.GetAll(), "BranchId", "BranchName");
            return View();
        }

        [Authorize(Roles = "Supervisor , Admin")]
        [HttpPost]
        public ActionResult Create(ProductViewModel product, HttpPostedFileBase file)
        {
            var BranchBusiness = new BranchBusiness();
            ViewBag.CategoryId = new SelectList(_productBusiness.GetAllCategories(), "Id", "CategoryName", product.CategoryId);
            ViewBag.BranchId = new SelectList(BranchBusiness.GetAll(), "BranchId", "BranchName",product.BranchId);
            if (ModelState.IsValid)
            {
                
                string ImageName = System.IO.Path.GetFileName(file.FileName);
                string physicalPath = Server.MapPath("~/Content/Images/" + ImageName);
                file.SaveAs(physicalPath);
                product.ImageUrl = ImageName;
                _productBusiness.Insert(product);
                return RedirectToAction("Index", "Product");
            }
            return View(product);
        }

        [Authorize(Roles = "Supervisor , Admin")]
        [HttpGet]
        public ActionResult Update(int id)
        {
            var BranchBusiness = new BranchBusiness();
            var model = _productBusiness.GetById(id);
            ViewBag.CategoryId = new SelectList(_productBusiness.GetAllCategories(), "Id", "CategoryName");
            ViewBag.BranchId = new SelectList(BranchBusiness.GetAll(), "BranchId", "BranchName");
            return View(model);
        }

        [Authorize(Roles = "Supervisor , Admin")]
        [HttpPost]
        public ActionResult Update(ProductViewModel product)
        {
            var BranchBusiness = new BranchBusiness();
            _productBusiness.Update(product);
            ViewBag.BranchId = new SelectList(BranchBusiness.GetAll(), "BranchId", "BranchName");
            ViewBag.CategoryId = new SelectList(_productBusiness.GetAllCategories(), "Id", "CategoryName", product.CategoryId);
            return RedirectToAction("Index", "Product");
        }

        [Authorize(Roles = "Supervisor , Admin")]
        [HttpGet]
        public ActionResult CreateCategory()
        {
            return View();
        }

        [Authorize(Roles = "Supervisor , Admin")]
        [HttpPost]
        public ActionResult CreateCategory(CategoryViewModel model)
        {
            if(ModelState.IsValid)
            {
                _productBusiness.InsertCategory(model);
                return RedirectToAction("AllCategories", "Product");
            }
            return View(model);
        }
        [Authorize(Roles = "Supervisor , Admin")]
        [HttpGet]
        public ActionResult AllCategories(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;



            var category = _productBusiness.GetAllCategories();

            if (!String.IsNullOrEmpty(searchString))
            {
                category = category.Where(x => x.CategoryName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    category = category.OrderByDescending(p => p.CategoryName);
                    break;
                default:
                    category = category.OrderBy(p => p.CategoryName);
                    break;
            }

            TempData["Categories"] = _productBusiness.GetAllCategories().Count();

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(category.ToPagedList(pageNumber, pageSize));

        }
        [Authorize(Roles = "Supervisor , Admin")]
        [HttpGet]
        public ActionResult UpdateCategory(int id)
        {
            var model = _productBusiness.GetByCategoryId(id);
            return View(model);
        }
        [Authorize(Roles = "Supervisor , Admin")]
        [HttpPost]
        public ActionResult UpdateCategory(CategoryViewModel category)
        {
            _productBusiness.UpdateCategory(category);
            return RedirectToAction("AllCategories", "Product");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            var model = _productBusiness.GetById(id);
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        // POST: /Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(ProductViewModel model, int id)
        {
            _productBusiness.GetById(id);
            _productBusiness.Delete(id);
            return RedirectToAction("Index","Product");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult ArchiveCategory(int id)
        {
            var model = _productBusiness.GetByCategoryId(id);
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        // POST: /Book/Delete/5
        [HttpPost, ActionName("ArchiveCategory")]
        [ValidateAntiForgeryToken]
        public ActionResult CategoryDeleteConfirmed(CategoryViewModel model, int id)
        {
            _productBusiness.GetByCategoryId(id);
            _productBusiness.DeleteCategory(id);
            return RedirectToAction("Index", "Product");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult ProductArchive(string sortOrder, string currentFilter, string searchString, int? page)
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



            var product = _productBusiness.GetProductArchive();

            TempData["Archive"] = product;

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
                    product = product.OrderBy(p => p.ProductPrice);
                    break;
                case "date_desc":
                    product = product.OrderByDescending(p => p.ProductPrice);
                    break;
                default:
                    product = product.OrderBy(p => p.ProductName);
                    break;
            }

            TempData["ProductArchive"] = _productBusiness.GetProductArchive().Count();

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(product.ToPagedList(pageNumber, pageSize));
        }
        [Authorize(Roles = "Admin")]
        public ActionResult CategoryArchive(string sortOrder, string currentFilter, string searchString, int? page)
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



            var product = _productBusiness.GetCategoryArchive();

            if (!String.IsNullOrEmpty(searchString))
            {
                product = product.Where(x => x.CategoryName.ToUpper().Contains(searchString) || x.CategoryName.ToLower().Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    product = product.OrderByDescending(p => p.CategoryName);
                    break;

                default:
                    product = product.OrderBy(p => p.CategoryName);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(product.ToPagedList(pageNumber, pageSize));
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Report(string id)
        {
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Report"), "Products.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Index");
            }
            var productBusiness = new ProductBusiness();

            var cm = productBusiness.GetAll();

            ReportDataSource rd = new ReportDataSource("ProductsDataSet", cm);
            lr.DataSources.Add(rd);
            string reportType = id;
            string mimeType;
            string encoding;
            string fileNameExtension;



            string deviceInfo =

            "<DeviceInfo>" +
            "  <OutputFormat>" + id + "</OutputFormat>" +
            "  <PageWidth>8.5in</PageWidth>" +
            "  <PageHeight>11in</PageHeight>" +
            "  <MarginTop>0.5in</MarginTop>" +
            "  <MarginLeft>1in</MarginLeft>" +
            "  <MarginRight>1in</MarginRight>" +
            "  <MarginBottom>0.5in</MarginBottom>" +
            "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);
            return File(renderedBytes, mimeType);
        }
        [AllowAnonymous]
        public ActionResult Details(int id)
        {

            var model = _productBusiness.GetById(id);

            if (model != null)
                ViewBag.CategoryId = new SelectList(_productBusiness.GetAllCategories(), "Id", "CategoryName", model.CategoryId);
            else
                ViewBag.CategoryId = new SelectList(_productBusiness.GetAllCategories(), "Id", "CategoryName");
            ViewBag.p = ViewBag.a;
            return View(model);
        }
       [Authorize(Roles = "Supervisor , Admin")]
        public ActionResult GetProduct(int id,int? page)
        {
            var prodb = new ProductBusiness();
            return View(prodb.GetProductByCategory(id).ToPagedList(pageNumber: page ?? 1, pageSize: 5));
        }
        [HttpGet]
        [AllowAnonymous]

        public ActionResult GetCustomerProduct(int id,int? page)
        {
            var prodb = new ProductBusiness();
            return View(prodb.GetProductByCategory(id).ToPagedList(pageNumber: page ?? 1, pageSize: 3));
        }
        [Authorize(Roles = "Admin")]
        public ActionResult RestoreProduct(int id)
        {
            var model = _productBusiness.GetProductArchiveById(id);
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        // POST: /Book/Delete/5
        [HttpPost, ActionName("RestoreProduct")]
        [ValidateAntiForgeryToken]
        public ActionResult ProductRestoreConfirmed(ProductArchiveViewModel model, int id)
        {
            _productBusiness.GetProductArchiveById(id);
            _productBusiness.RestoreProduct(id);
            return RedirectToAction("Index", "Product");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult OutOfStock()
        {
            return View(_productBusiness.GetOutOfStock().ToList());
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult StockPerBranch(int id, int? page)
        {
            var prodb = new ProductBusiness();
            return View(prodb.GetStockPerBranch(id).ToPagedList(pageNumber: page ?? 1, pageSize: 10));
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult GetSummary(int id, int? page)
        {
            var prodb = new ProductBusiness();
            return View(prodb.GetSummaryPerUnit(id).ToPagedList(pageNumber: page ?? 1, pageSize: 10));
        }
        [Authorize(Roles = "Admin")]

        public ActionResult GetGraph()
        {
            ViewBag.GetData = _productBusiness.GetPriceGraph().Distinct().ToList();
            return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult GetBranchGraph(int id)
        {
            ViewBag.GetData = _productBusiness.GetStockPerBranchGraph(id).ToList();
            return View(_productBusiness.GetStockPerBranchGraph(id).ToList());
        }
    }
}