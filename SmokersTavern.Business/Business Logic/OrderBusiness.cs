//Vishalen 
using SmokersTavern.Business.Interfaces;
using SmokersTavern.Data;
using SmokersTavern.Data.Models;
using SmokersTavern.Model;
using SmokersTavern.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SmokersTavern.Business.Business_Logic
{
    public class OrderBusiness : IOrderBusiness
    {
        public void Insert(OrderViewModel model)
        {
            using (var repository = new OrderRepository())
            {
                repository.Insert(new Order
                {
                    //OrderId = model.OrderId,
                    Status = model.Status,
                    Date = System.DateTime.Now.Date,
                    Username = model.Username,
                    Products = model.Products,
                    OrderPostalAddress = model.OrderPostalAddress,
                    //Quantity = model.Quantity,
                    //Price = model.Price,
                    //SubTotal = model.SubTotal,
                    Total = model.Total
                });
            }

        }

        public IEnumerable<OrderViewModel> GetAllOrder()
        {
            using (var order = new OrderRepository())
            {
                return order.GetAll().Select(o => new OrderViewModel()
                                {
                                    OrderId = o.OrderId,
                                    Status = o.Status,
                                    Date = o.Date,
                                    Username = o.Username,
                                    Products = o.Products,
                                    OrderPostalAddress = o.OrderPostalAddress,
                                    //Quantity = o.Quantity,
                                    //Price = o.Price,
                                    //SubTotal = o.SubTotal,
                                    Total = o.Total,
                                }).ToList();
            }

        }

        public IEnumerable<OrderViewModel> GetPendingOrders()
        {
            using (var order = new OrderRepository())
            {
                return order.GetAll().Select(o => new OrderViewModel()
                {
                    OrderId = o.OrderId,
                    Status = o.Status,
                    Date = o.Date,
                    Username = o.Username,
                    Products = o.Products,
                    OrderPostalAddress = o.OrderPostalAddress,
                    //Quantity = o.Quantity,
                    //Price = o.Price,
                    //SubTotal = o.SubTotal,
                    Total = o.Total,
                }).Where(x => x.Status == "Pending").ToList();
            }
        }
        public IEnumerable<OrderViewModel> GetDispatchedOrders()
        {
            using (var order = new OrderRepository())
            {
                return order.GetAll().Select(o => new OrderViewModel()
                {
                    OrderId = o.OrderId,
                    Status = o.Status,
                    Date = o.Date,
                    Username = o.Username,
                    Products = o.Products,
                    OrderPostalAddress=o.OrderPostalAddress,
                    //Quantity = o.Quantity,
                    //Price = o.Price,
                    //SubTotal = o.SubTotal,
                    Total = o.Total,
                }).Where(x => x.Status == "Dispatched").ToList();
            }
        }
        public IEnumerable<OrderViewModel> GetDeliveredOrders()
        {
            using (var order = new OrderRepository())
            {
                return order.GetAll().Select(o => new OrderViewModel()
                {
                    OrderId = o.OrderId,
                    Status = o.Status,
                    Date = o.Date,
                    Username = o.Username,
                    Products = o.Products,
                    OrderPostalAddress=o.OrderPostalAddress,
                    //Quantity = o.Quantity,
                    //Price = o.Price,
                    //SubTotal = o.SubTotal,
                    Total = o.Total,
                }).Where(x => x.Status == "Delivered").ToList();
            }
        }
        public IEnumerable<OrderViewModel> GetReturnedOrders()
        {
            using (var order = new OrderRepository())
            {
                return order.GetAll().Select(o => new OrderViewModel()
                {
                    OrderId = o.OrderId,
                    Status = o.Status,
                    Date = o.Date,
                    Username = o.Username,
                    Products = o.Products,
                    OrderPostalAddress=o.OrderPostalAddress,
                    //Quantity = o.Quantity,
                    //Price = o.Price,
                    //SubTotal = o.SubTotal,
                    Total = o.Total,
                }).Where(x => x.Status == "Returned").ToList();
            }
        }

        public IEnumerable<OrderViewModel> GetAllOrderCustomer()
        {
            using (var order = new OrderRepository())
            {
                return order.GetAll().Select(o => new OrderViewModel()
                                {
                                    OrderId = o.OrderId,
                                    Status = o.Status,
                                    Date = o.Date,
                                    Username = o.Username,
                                    Products = o.Products,
                                    OrderPostalAddress=o.OrderPostalAddress,
                                    //Quantity = o.Quantity,
                                    //Price = o.Price,
                                    //SubTotal = o.SubTotal,
                                    Total = o.Total,
                                }).Where(x => x.Username == HttpContext.Current.User.Identity.Name).ToList();
            }

        }
        public IEnumerable<OrderViewModel> GetAllPendingOrders()
        {
            using (var order = new OrderRepository())
            {
                return order.GetAll().Select(o => new OrderViewModel()
                {
                    OrderId = o.OrderId,
                    Status = o.Status,
                    Date = o.Date,
                    Username = o.Username,
                    Products = o.Products,
                    OrderPostalAddress=o.OrderPostalAddress,
                    //Quantity = o.Quantity,
                    //Price = o.Price,
                    //SubTotal = o.SubTotal,
                    Total = o.Total,
                }).Where(x => x.Username == HttpContext.Current.User.Identity.Name  && x.Status == "Pending").ToList();
            }

        }
        public IEnumerable<OrderViewModel> GetAllDispatchedOrders()
        {
            using (var order = new OrderRepository())
            {
                return order.GetAll().Select(o => new OrderViewModel()
                {
                    OrderId = o.OrderId,
                    Status = o.Status,
                    Date = o.Date,
                    Username = o.Username,
                    Products = o.Products,
                    OrderPostalAddress=o.OrderPostalAddress,
                    //Quantity = o.Quantity,
                    //Price = o.Price,
                    //SubTotal = o.SubTotal,
                    Total = o.Total,
                }).Where(x => x.Username == HttpContext.Current.User.Identity.Name && x.Status == "Dispatched").OrderByDescending(x => x.Date).ToList();
            }

        }

        public IEnumerable<OrderViewModel> GetAllDeliveredOrders()
        {
            using (var order = new OrderRepository())
            {
                return order.GetAll().Select(o => new OrderViewModel()
                {
                    OrderId = o.OrderId,
                    Status = o.Status,
                    Date = o.Date,
                    Username = o.Username,
                    Products = o.Products,
                    OrderPostalAddress=o.OrderPostalAddress,
                    //Quantity = o.Quantity,
                    //Price = o.Price,
                    //SubTotal = o.SubTotal,
                    Total = o.Total,
                }).Where(x => x.Username == HttpContext.Current.User.Identity.Name && x.Status == "Delivered").ToList();
            }

        }
        //Shivani Reddy
        public IEnumerable<OrderViewModel> GetAllReturnedOrders()
        {
            using (var order = new OrderRepository())
            {
                return order.GetAll().Select(o => new OrderViewModel()
                {
                    OrderId = o.OrderId,
                    Status = o.Status,
                    Date = o.Date,
                    Username = o.Username,
                    Products = o.Products,
                    OrderPostalAddress=o.OrderPostalAddress,
                    //Quantity = o.Quantity,
                    //Price = o.Price,
                    //SubTotal = o.SubTotal,
                    Total = o.Total,
                }).Where(x => x.Username == HttpContext.Current.User.Identity.Name && x.Status == "Returned").ToList();
            }

        }

        public OrderViewModel GetById(Int32 id)
        {
            using (var repository = new OrderRepository())
            {
                return repository.Find(x => x.OrderId == id).Select(model => new OrderViewModel
                {
                    OrderId = model.OrderId,
                    Status = model.Status,
                    Date = model.Date,
                    Username = model.Username,
                    Products = model.Products,
                    OrderPostalAddress = model.OrderPostalAddress,
                    //Quantity = model.Quantity,
                    //Price = model.Price,
                    //SubTotal = model.SubTotal,
                    Total = model.Total,
                }).FirstOrDefault();
            }
        }

        public void Update(OrderViewModel model)
        {

            if(model.Status == "Dispatched")
            {
                string prod = "";
                using (var ord = new OrderRepository())
                {
                    var o = new Order();
                    o = ord.GetById(model.OrderId);
                    if (o != null)
                    {
                        prod = o.Products;
                        o.Status = model.Status;
                        //o.OrderPostalAddress = model.OrderPostalAddress;
                        ord.Update(o);
                    }


                    int z = 1;
                    while (z != -1)
                    {
                        string rec = prod.Substring(0, (prod.IndexOf('|') + 1)); //Output  =  1 * rghhnj = R98,
                        string prodnme = rec.Substring((rec.IndexOf('X') + 1), (rec.Length - 3)); // rghhnj = R98,
                        string product = prodnme;                                                                                   //string prodnme = p.Substring(0,(p.IndexOf('='))-1); //Output = rghhnj
                        string strcartqty = rec.Substring(0, 1);
                        int cartqty = Convert.ToInt16(strcartqty);

                        var s = new Sales()
                        {
                            ProductPurchaseName = product,
                            ProductPurchaseQuantity = cartqty,
                            ProductPurchaseTotal = model.Total,
                            ProductPurchaseDate = model.Date
                        };
                        ApplicationDbContext db = new ApplicationDbContext();
                        db.Sale.Add(s);
                        db.SaveChanges();


                        using (var ord2 = new ProductRepository())
                        {
                            var dis = new Product();
                            dis = ord2.GetAll().First(x => x.ProductName.ToLower().Equals(prodnme.ToLower()));


                            dis.ProductQuantity = dis.ProductQuantity - cartqty;
                            ord2.Update(dis);


                            if (dis.ProductQuantity <= 0)
                            {
                                var productBusiness = new ProductBusiness();

                                productBusiness.Delete(dis.Id);
                            }
                        }

                        int h = (prod.IndexOf('|')) + 1;
                        prod = prod.Substring(h);
                        z = prod.IndexOf('X');
                    }
                }
            }
            else
            {
                string prod = "";
                using (var ord = new OrderRepository())
                {
                    var o = new Order();
                    o = ord.GetById(model.OrderId);
                    if (o != null)
                    {
                        prod = o.Products;
                        o.Status = model.Status;
                        //o.OrderPostalAddress = model.OrderPostalAddress;
                        ord.Update(o);
                    }
                }            
            }
              
        }

        public OrderViewModel deleteMethod(int id)
        {
            OrderViewModel v = new OrderViewModel();

            using (var ssd = new OrderRepository())
            {
                if (id != 0)
                {
                    Order model = ssd.GetById(id);

                    v.OrderId = model.OrderId;
                    //v.Username = model.Username;
                    //v.Date = model.Date;
                    v.Products = model.Products;
                    //v.Price = model.Price;
                    //v.Quantity = model.Quantity;
                    //v.SubTotal = model.SubTotal;
                    //v.Total = model.Total;
                    //v.Status = model.Status;
                }

                return v;
            }
        }

        public void PostDeleteMethod(int id)
        {
            using (var d = new OrderRepository())
            {
                if (id != 0)
                {
                    Order _model = d.GetById(id);
                    d.Delete(_model);
                }
            }

        }

        public void UpdateOrder(OrderViewModel order)
        {
            using (var ord = new OrderRepository())
            {
                var o = new Order();
                o = ord.GetById(order.OrderId);
                if (o != null)
                {
                    o.OrderId = order.OrderId;
                    o.OrderPostalAddress = order.OrderPostalAddress;
                    ord.Update(o);
                }
            }
        }
    }
}
