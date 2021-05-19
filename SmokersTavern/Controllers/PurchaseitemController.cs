using SmokersTavern.Data;
using SmokersTavern.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//Zain
namespace SmokersTavern.Controllers
{
    public class PurchaseitemController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.a = new SelectList(db.Products.ToList(), "Id", "ProductName");
            return View();
        }

        [HttpPost]
        public ActionResult Create(PurchaseItem model, string ClientId)
        {
            ViewBag.a = new SelectList(db.Products.ToList(), "Id", "ProductName");

            int prodId = Convert.ToInt32(model.ProductId);

            ViewBag.l = prodId;

            if (ClientId != null)
            {
                var name = (from x in db.Products
                            where x.Id == prodId
                            select x).ToList();

                foreach (var item in name)
                {
                    ViewBag.c = item.ProductName;
                    ViewBag.d = item.ProductCostPrice;

                    var newItem = new PurchaseItem()
                    {
                        ClientId = ClientId,
                        Quantity = model.Quantity,
                        ProductId = model.ProductId,
                        ProductName = item.ProductName,
                        ProductCostPrice = item.ProductCostPrice
                    };
                    db.PurchaseItems.Add(newItem);
                    db.SaveChanges();
                }
            }
            ModelState.Clear();
            return View();
        }

    }
}