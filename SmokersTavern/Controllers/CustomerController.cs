using SmokersTavern.Business.Business_Logic;
using SmokersTavern.Business.Interfaces;
using SmokersTavern.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using SmokersTavern.Model;


//Zain
namespace SmokersTavern.Controllers
{
    [AllowAnonymous]
    public class CustomerController : Controller
    {
        IProductBusiness _cb;
        ApplicationDbContext _context = new ApplicationDbContext();

        public CustomerController()
        {
            _cb = new ProductBusiness();

        }

        public CustomerController(IProductBusiness prodbiz)
        {
            _cb = prodbiz;
        }

        public ActionResult Index(int? page)
        {
            var prodb = new ProductBusiness();
            ViewBag.ProductTypeId = new SelectList(_cb.GetAllCategories(), "Id", "CategoryName");
            var p = prodb.GetAll().Distinct();
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(p.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult Index(int criteria, string name, int? page)
        {
            page = 1;
            var productBusiness = new ProductBusiness();
            ViewBag.ProductTypeId = new SelectList(_cb.GetAllCategories(), "Id", "CategoryName");

            if (name == "Category")
            {
                TempData["prodname"] = "Invalid Category";
                return View();
            }

            if (criteria > 0)
            {
                if (!string.IsNullOrEmpty(name))
                {
                    ViewBag.Result = true;
                    var pglist = productBusiness.GetAll().Distinct()
                        .OrderBy(X => X.ProductName)
                        .Where(x => x.ProductName.ToLower() == name.ToLower() || x.ProductName.ToLower().StartsWith(name.ToLower()) && x.CategoryId == criteria).ToPagedList(pageNumber: page ?? 1, pageSize: 5);
                    if (pglist.Count == 0)
                    {
                        TempData["productempty"] = "No items found";
                    }
                    return View(pglist);
                }
                else
                {
                    var pglist = productBusiness.GetAll().Distinct()
                        .OrderBy(X => X.ProductName)
                        .Where(x => x.CategoryId == criteria)
                        .ToPagedList(pageNumber: page ?? 1, pageSize: 5);
                    if (pglist.Count == 0)
                    {
                        TempData["productempty"] = "No items found";
                    }
                    return View(pglist);
                }
            }
            return View();
        }
        public ActionResult Privacy()
        {
            return View();
        }

        public ActionResult CustomerCategories()
        {
            var category = _cb.GetAllCategories();
            return View(category.ToList());

        }

        [HttpGet]
        public ActionResult PriceRange(int? page)
        {
            var prodb = new ProductBusiness();
            ViewBag.ProductTypeId = new SelectList(_cb.GetAllCategories(), "Id", "CategoryName");
            var p = from x in prodb.GetAll()
                    select x;

            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(p.ToPagedList(pageNumber, pageSize));
        }
        [HttpPost]
        public ActionResult PriceRange(int? page, int? val)
        {

            int num = Convert.ToInt32(val);
            ViewBag.a = num;
            var prodb = new ProductBusiness();
            ViewBag.ProductTypeId = new SelectList(_cb.GetAllCategories(), "Id", "CategoryName");
            var p = from x in prodb.GetAll()
                    where x.ProductPrice == val
                    select x;

            ViewBag.d = p;

            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(p.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Color(int? page)
        {
            var prodb = new ProductBusiness();
            ViewBag.ProductTypeId = new SelectList(_cb.GetAllCategories(), "Id", "CategoryName");
            var p = from x in prodb.GetAll()
                    select x;

            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(p.ToPagedList(pageNumber, pageSize));
        }
        [HttpPost]
        public ActionResult Color(int? page, string color)
        {

            ViewBag.a = color;
            var prodb = new ProductBusiness();
            ViewBag.ProductTypeId = new SelectList(_cb.GetAllCategories(), "Id", "CategoryName");
            var p = from x in prodb.GetAll()
                    where x.ProductColor == color
                    select x;

            ViewBag.d = p;

            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(p.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Items()
        {
            var db = new ApplicationDbContext();
            var query = from x in db.Products.GroupBy(x => new {x.ProductName, x.ImageUrl })
                        select new ProductViewModel()
                        {
                            Id = x.Max(y => y.Id),
                            ProductName = x.Key.ProductName,
                            ProductQuantity = x.Sum(y => y.ProductQuantity),
                            ImageUrl = x.Key.ImageUrl,
                            ProductPrice = x.Max(y => y.ProductPrice)
                        };

            return View(query.ToList());
        }
    }
}