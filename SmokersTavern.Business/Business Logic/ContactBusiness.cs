using SmokersTavern.Data.Models;
using SmokersTavern.Model;
using SmokersTavern.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;


//Zain
namespace SmokersTavern.Business.Business_Logic
{
    public class ContactBusiness
    {
        public void Email(ContactViewModel model)
        {
            try
            {

                var body = "<p> Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                MailMessage mm = new MailMessage();
                SmtpClient client = new SmtpClient("smtp.gmail.com");
                mm.From = new MailAddress(model.Email);

                mm.To.Add("nabizain2@gmail.com");
                mm.Subject = model.Subject;
                mm.Body = string.Format(body, model.Name, model.Email, model.Message);
                mm.IsBodyHtml = true;
                
                client.Credentials = new NetworkCredential("nabizain2@gmail.com", "123456");

                client.Credentials = new System.Net.NetworkCredential
                ("nabizain2@gmail.com", "p@ssw0rd003");

                client.EnableSsl = true;

                client.Send(mm);


            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }              
        }
        public IEnumerable<ProductViewModel> GetAll()
        {
            var products = new List<ProductViewModel>();

            using (var repository = new ProductRepository())
            {
                products = repository.GetAll().Select(model => new ProductViewModel
                {

                    ProductName = model.ProductName,
                    ProductDescription = model.ProductDescription,
                    BrandName = model.BrandName,
                    ProductColor = model.ProductColor,
                    ProductQuantity = model.ProductQuantity,
                    ProductPrice = model.ProductPrice,
                    ImageUrl = model.ImageUrl

                }).ToList();
            }
            return products;
        }
    }
}
