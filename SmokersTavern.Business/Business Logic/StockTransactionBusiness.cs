//Tryvynne
using SmokersTavern.Business.Interfaces;
using SmokersTavern.Data.Models;
using SmokersTavern.Model;
using SmokersTavern.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokersTavern.Business.Business_Logic
{
    public class StockTransactionBusiness : IStockTransactionBusiness
    {
        public void Insert(StockTransactionsViewModel model)
        {

            using (var p = new ProductRepository())
            {
                var s = new Product();
                s = p.GetById(model.Id);
                string parm = s.ProductName;

                using (var supp = new StockTransactionRepository())
                {
                    supp.Insert(new StockTransactions
                    {
                        StockId = model.StockId,
                        Reason = model.Reason,
                        Quantity = model.Quantity,
                        Date = DateTime.Now,
                        Id = model.Id,
                        ProductName = parm
                    });
                }
            }
        }

        //Romell
        public IEnumerable<StockTransactionsViewModel> GetAllStockTransactions()
        {
            var item = new List<StockTransactionsViewModel>();
            using (var suppp = new StockTransactionRepository())
            {
                item = suppp.GetAll().Select(x => new StockTransactionsViewModel
                {
                    StockId = x.StockId,
                    Reason = x.Reason,
                    Quantity = x.Quantity,
                    ProductName = x.ProductName,
                    Id = x.Id,
                    Date = x.Date
                }).ToList();

                using (var repository = new ProductRepository())
                {
                    item.ForEach(x => x.Product = repository.Find(y => y.Id == x.Id).Select(model => new ProductViewModel
                    {
                        Id = model.Id,
                        ProductName = model.ProductName

                    }).FirstOrDefault());
                }
                return item;
            }
        }

        public void DecreaseProductQuantity(StockTransactionsViewModel model)
        {
            using (var repo = new ProductRepository())
            {
                var product = new Product();
                product = repo.GetById(model.Id);

                if (product != null)
                {
                    product.ProductQuantity = +product.ProductQuantity - model.Quantity;
                    repo.Update(product);
                }
            }
        }

        public void IncreaseQuantity(StockTransactionsViewModel model)
        {
            using (var repo = new ProductRepository())
            {
                var product = new Product();
                product = repo.GetById(model.Id);

                if (product != null)
                {
                    product.ProductQuantity = +model.Quantity + product.ProductQuantity;

                    repo.Update(product);
                }
            }
        }
    }
}
