using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using SmokersTavern.Business.Business_Logic;
using SmokersTavern.Business.Interfaces;
using SmokersTavern.Data;
using SmokersTavern.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Web.Routing;
using SmokersTavern.Business;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SmokersTavern.Data.Models;
//Zain
namespace SmokersTavern.Controllers
{
    public class OrderController : Controller
    {
        IOrderBusiness _ob;
        ApplicationDbContext _context = new ApplicationDbContext();
        public UserManager<ApplicationUser> UserManager { get; set; }

        public OrderController()
        {
            _ob = new OrderBusiness();
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

        }

        public OrderController(IOrderBusiness orderbiz)
        {
            _ob = orderbiz;
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        }

        //GET:ORDER
        [HttpGet]
        public ActionResult GetCustomerOrder(int? page)
        {
            var model = _ob.GetAllOrderCustomer();
            return View(model.ToPagedList(pageNumber: page ?? 1, pageSize: 20));
        }

        [HttpGet]
        public ActionResult GetPendingOrders(int? page)
        {
            var model = _ob.GetAllPendingOrders();
            return View(model.ToPagedList(pageNumber: page ?? 1, pageSize: 20));
        }
        [HttpGet]
        public ActionResult GetDispatchedOrders(int? page)
        {
            var model = _ob.GetAllDispatchedOrders();
            return View(model.ToPagedList(pageNumber: page ?? 1, pageSize: 20));
        }
        [HttpGet]
        public ActionResult GetDeliveredOrders(int? page)
        {
            var model = _ob.GetAllDeliveredOrders();
            return View(model.ToPagedList(pageNumber: page ?? 1, pageSize: 20));
        }
        [HttpGet]
        public ActionResult GetReturnOrders(int? page)
        {
            var model = _ob.GetAllReturnedOrders();
            return View(model.ToPagedList(pageNumber: page ?? 1, pageSize: 20));
        }

        [HttpGet]
        public ActionResult Index(int? page)
        {
            var model = _ob.GetAllOrder();
            return View(model.ToPagedList(pageNumber: page ?? 1, pageSize: 20));
        }

        [HttpPost]
        public ActionResult Index(string name, int? page)
        {
            page = 1;

            if (!string.IsNullOrEmpty(name))
            {
                ViewBag.Result = true;
                var pglist = _ob.GetAllOrder()
                    .OrderBy(X => X.Username)
                    .Where(x => x.Username.ToLower() == name.ToLower() || x.Username.ToLower().StartsWith(name.ToLower()))
                    .ToPagedList(pageNumber: page ?? 1, pageSize: 20);

                if (pglist.Count == 0)
                {
                    TempData["custempty"] = "No Order found";
                }
                return View(pglist);
            }
            else
            {
                TempData["custname"] = "Invalid Entry";
                return View(new List<SmokersTavern.Data.Models.Order>().ToPagedList(pageNumber: page ?? 1, pageSize: 20));
            }
        }
        [HttpGet]
        public ActionResult Pending(int? page)
        {
            var model = _ob.GetPendingOrders();
            TempData["Orders"] = model;
            return View(model.ToPagedList(pageNumber: page ?? 1, pageSize: 20));
        }

        [HttpPost]
        public ActionResult Pending(string name, int? page)
        {
            page = 1;

            if (!string.IsNullOrEmpty(name))
            {
                ViewBag.Result = true;
                var pglist = _ob.GetPendingOrders()
                    .OrderBy(X => X.Username)
                    .Where(x => x.Username.ToLower() == name.ToLower() || x.Username.ToLower().StartsWith(name.ToLower()))
                    .ToPagedList(pageNumber: page ?? 1, pageSize: 20);

                if (pglist.Count == 0)
                {
                    TempData["custempty"] = "No Order found";
                }
                return View(pglist);
            }
            else
            {
                TempData["custname"] = "Invalid Entry";
                return View(new List<SmokersTavern.Data.Models.Order>().ToPagedList(pageNumber: page ?? 1, pageSize: 20));
            }
        }
        [HttpGet]
        public ActionResult Delivered(int? page)
        {
            var model = _ob.GetDeliveredOrders();
            return View(model.ToPagedList(pageNumber: page ?? 1, pageSize: 20));
        }

        [HttpPost]
        public ActionResult Delivered(string name, int? page)
        {
            page = 1;

            if (!string.IsNullOrEmpty(name))
            {
                ViewBag.Result = true;
                var pglist = _ob.GetDeliveredOrders()
                    .OrderBy(X => X.Username)
                    .Where(x => x.Username.ToLower() == name.ToLower() || x.Username.ToLower().StartsWith(name.ToLower()))
                    .ToPagedList(pageNumber: page ?? 1, pageSize: 20);

                if (pglist.Count == 0)
                {
                    TempData["custempty"] = "No Order found";
                }
                return View(pglist);
            }
            else
            {
                TempData["custname"] = "Invalid Entry";
                return View(new List<SmokersTavern.Data.Models.Order>().ToPagedList(pageNumber: page ?? 1, pageSize: 20));
            }
        }

        [HttpGet]
        public ActionResult Dispatched(int? page)
        {
            var model = _ob.GetDispatchedOrders();
            return View(model.ToPagedList(pageNumber: page ?? 1, pageSize: 20));
        }

        [HttpPost]
        public ActionResult Dispatched(string name, int? page)
        {
            page = 1;

            if (!string.IsNullOrEmpty(name))
            {
                ViewBag.Result = true;
                var pglist = _ob.GetDispatchedOrders()
                    .OrderBy(X => X.Username)
                    .Where(x => x.Username.ToLower() == name.ToLower() || x.Username.ToLower().StartsWith(name.ToLower()))
                    .ToPagedList(pageNumber: page ?? 1, pageSize: 20);

                if (pglist.Count == 0)
                {
                    TempData["custempty"] = "No Order found";
                }
                return View(pglist);
            }
            else
            {
                TempData["custname"] = "Invalid Entry";
                return View(new List<SmokersTavern.Data.Models.Order>().ToPagedList(pageNumber: page ?? 1, pageSize: 20));
            }
        }

        [HttpGet]
        public ActionResult Returns(int? page)
        {
            var model = _ob.GetReturnedOrders();
            return View(model.ToPagedList(pageNumber: page ?? 1, pageSize: 20));
        }

        [HttpPost]
        public ActionResult Returns(string name, int? page)
        {
            page = 1;

            if (!string.IsNullOrEmpty(name))
            {
                ViewBag.Result = true;
                var pglist = _ob.GetReturnedOrders()
                    .OrderBy(X => X.Username)
                    .Where(x => x.Username.ToLower() == name.ToLower() || x.Username.ToLower().StartsWith(name.ToLower()))
                    .ToPagedList(pageNumber: page ?? 1, pageSize: 20);

                if (pglist.Count == 0)
                {
                    TempData["custempty"] = "No Order found";
                }
                return View(pglist);
            }
            else
            {
                TempData["custname"] = "Invalid Entry";
                return View(new List<SmokersTavern.Data.Models.Order>().ToPagedList(pageNumber: page ?? 1, pageSize: 20));
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(OrderViewModel model)
        {
            string username = User.Identity.Name;
            if (ModelState.IsValid)
            {
                model.Username = username;
                model.Status = "Pending";
                _ob.Insert(model);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = _ob.GetById(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(OrderViewModel model)
        {
                _ob.Update(model);
            return RedirectToAction("Index");

        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }



        [HttpPost]
        public string tot(string Name, int Price, string Comments)
        {

            ViewBag.n = Comments;
            OrderBusiness or = new OrderBusiness();

            OrderViewModel model = new OrderViewModel();

            //string str = "";

            //str = TempData["PostalAddress"].ToString();



            model.Status = "Pending";
            model.Username = HttpContext.User.Identity.Name;
            model.Products = Name;
            model.Total = Price;
            model.OrderPostalAddress = Comments;
            model.Date = DateTime.Now.Date;
            or.Insert(model);

            return Name;

        }



        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(string v)
        {
            if (!string.IsNullOrEmpty(v))
            {
                ViewBag.Result = true;
                return View(_ob.GetAllOrder().OrderBy(x => x.OrderId).Where(x => x.Username == v || x.Username.StartsWith(v)));
            }

            ModelState.AddModelError("", "No orders were found matching this voucher number");
            return View();
        }

        public ActionResult Details(int id)
        {
            var model = _ob.GetById(id);
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            return View(_ob.deleteMethod(id));
        }

        [HttpPost]
        public ActionResult Delete(int id, ProductViewModel models)
        {
            try
            {

                _ob.PostDeleteMethod(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public FileStreamResult PrintConfirmed(int id = 0)
        {

            // Set up the document and the MS to write it to and create the PDF writer instance
            MemoryStream ms = new MemoryStream();
            Document document = new Document(PageSize.A5, 0, 0, 0, 0);
            PdfWriter writer = PdfWriter.GetInstance(document, ms);
            Image addLogo = default(Image);
            addLogo = Image.GetInstance(Server.MapPath("~/Content/Images/Logo.jpg"));

            //var obj = new datacontext();
            // var obj = new DataContext();

            // Open the PDF document
            document.Open();
            addLogo.ScalePercent(40f);
            addLogo.Alignment = Image.ALIGN_RIGHT;
            document.Add(addLogo);

            // Set up fonts used in the document
            Font font_heading_3 = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.BOLD, iTextSharp.text.BaseColor.RED);
            Font font_body = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, iTextSharp.text.BaseColor.BLUE);

            // Create the heading paragraph with the headig font
            Paragraph paragraph;
            paragraph = new Paragraph("", font_heading_3);
            Paragraph paraslip;
            paraslip = new Paragraph("**********************Order Details***********************");


            // Create the heading paragraph with the headig font

            var db = new ApplicationDbContext();
            var info = from x in db.Orders
                       where x.OrderId == id
                       select x;

            foreach (var q in info)
            {
                //paragraph;
                // Add a horizontal line below the headig text and add it to the paragraph
                iTextSharp.text.pdf.draw.VerticalPositionMark seperator = new iTextSharp.text.pdf.draw.LineSeparator();
                seperator.Offset = -6f;


                PdfPTable table1 = new PdfPTable(1);
                PdfPTable table = new PdfPTable(1);
                PdfPTable table3 = new PdfPTable(1);
                PdfPTable table7 = new PdfPTable(1);
                PdfPTable table8 = new PdfPTable(1);
                // Remove table cell
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table1.DefaultCell.Border = Rectangle.NO_BORDER;
                table7.DefaultCell.Border = Rectangle.NO_BORDER;
                table8.DefaultCell.Border = Rectangle.NO_BORDER;
                table3.DefaultCell.Border = Rectangle.NO_BORDER;



                table.WidthPercentage = 80;
                table3.SetWidths(new float[] { 100 });
                table3.WidthPercentage = 80;
                table7.SetWidths(new float[] { 100 });
                table7.WidthPercentage = 80;
                table8.SetWidths(new float[] { 100 });
                table8.WidthPercentage = 80;
                PdfPCell cell = new PdfPCell(new Phrase(""));
                cell.Colspan = 3;
                table1.AddCell(cell);
                table8.AddCell("\nSmokers Tavern\n" + "\n\n");
                table7.AddCell(
                   "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t" +
                    "19 Parthenon St" + "\n" + "Phoenix 4068\n" + "South Africa\n" +
                    "Tel: 0318117039\n" + "Fax: 0315003508\n" +
                    "Email :info@smokerstavern.co.za/" + "\n" +
                    "Online Purchase Record\n" + "\n" + "\n");



                //for searching another view


                Order events_details = db.Orders.Find(q.OrderId);

                table.AddCell("\n\n");
                table.AddCell("Please print this as proof of payment, to produce when collectiong your order.\n\n");
                table.AddCell("Username     : " + q.Username);
                table.AddCell("Date              : " + q.Date);
                table.AddCell("Product(s)     : " + q.Products);
                table.AddCell("Total              : R" + q.Total + "\n\n\n");




                table.AddCell("Signature          : " + "................................");
                table.AddCell("Pickup Date: " + "...../" + "...../....." +
                              "                " + "     Stamp  : " + "......................");
                table.AddCell("\n\n");
                table.AddCell("Should you have any further queries, please do not hesitate to contact us.");

                //Intergrate information into 1 document

                table.AddCell(cell);
                document.Add(table3);
                document.Add(table8);
                document.Add(table7);
                document.Add(table1);
                document.Add(table);
                document.Close();
            }
            byte[] file = ms.ToArray();
            MemoryStream output = new MemoryStream();
            output.Write(file, 0, file.Length);
            output.Position = 0;
            FileStreamResult f = new FileStreamResult(output, "application/pdf");
            return f; //File(output, "application/pdf"); //new FileStreamResult(output, "application/pdf");   
        }

    }
}