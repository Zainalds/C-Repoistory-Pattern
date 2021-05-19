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
    [Authorize]
    public class BranchController : Controller
    {
        IBranchBusiness _branchBusiness;
        ApplicationDbContext _context = new ApplicationDbContext();

        public BranchController()
        {
            _branchBusiness = new BranchBusiness();

        }

        public BranchController(IBranchBusiness model)
        {
            _branchBusiness = model;
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



            var product = _branchBusiness.GetAll();
            TempData["Branches"] = product;

            if (!String.IsNullOrEmpty(searchString))
            {
                product = product.Where(x => x.BranchName.ToUpper().Contains(searchString) || x.BranchName.ToLower().Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    product = product.OrderByDescending(p => p.BranchName);
                    break;

                case "Date":
                    product = product.OrderBy(p => p.BranchName);
                    break;
                case "date_desc":
                    product = product.OrderByDescending(p => p.BranchManager);
                    break;
                default:
                    product = product.OrderBy(p => p.BranchName);
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
            return View();
        }

        [Authorize(Roles = "Supervisor , Admin")]
        [HttpPost]
        public ActionResult Create(BranchViewModel branch)
        {
            if (ModelState.IsValid)
            {
                _branchBusiness.Insert(branch);
                return RedirectToAction("Index", "Branch");
            }
            return View(branch);
        }

        [Authorize(Roles = "Supervisor , Admin")]
        [HttpGet]
        public ActionResult Update(int id)
        {
            var model = _branchBusiness.GetByBranchId(id);
            return View(model);
        }

        [Authorize(Roles = "Supervisor , Admin")]
        [HttpPost]
        public ActionResult Update(BranchViewModel branch)
        {
            _branchBusiness.Update(branch);
            return RedirectToAction("Index", "Branch");
        }
    }
}