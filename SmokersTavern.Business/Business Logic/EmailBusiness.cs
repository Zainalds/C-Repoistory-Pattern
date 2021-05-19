using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;


//Zain
namespace SmokersTavern.Business.Business_Logic
{
    public class EmailBusiness
    {
        public MailAddress to { get; set; }
        public MailAddress from { get; set; }
        public string sub { get; set; }
        public string body { get; set; }


        public string NewRegistration()
        {
            string feed = "";

            var m = new MailMessage()
            {
                Subject = "Successfully Registered",
                Body = body,
                IsBodyHtml = true,
            };
            m.From = new MailAddress("21122173@dut4life.ac.za", "Smokers Tavern");
            m.To.Add(to);
            SmtpClient smtp = new SmtpClient()
            {
                Host = "pod51014.outlook.com",
                Port = 587,
                Credentials = new System.Net.NetworkCredential("21122173@dut4life.ac.za", "Dut920924"),
                EnableSsl = true
            };
            try
            {
                smtp.Send(m);
                feed = "Check your email to see your Username and Password";
            }
            catch (Exception e)
            {
                feed = "Email not Sent" + e.Message;
            }
            return feed;
        }

        public string PasswordChange()
        {
            string feed = "";

            var m = new MailMessage()
            {
                Subject = "Password Change - Complete",
                Body = body,
                IsBodyHtml = true,
            };
            m.From = new MailAddress("21122173@dut4life.ac.za", "Smokers Tavern");
            m.To.Add(to);
            SmtpClient smtp = new SmtpClient()
            {
                Host = "pod51014.outlook.com",
                Port = 587,
                Credentials = new System.Net.NetworkCredential("21122173@dut4life.ac.za", "Dut920924"),
                EnableSsl = true
            };
            try
            {
                smtp.Send(m);
                feed = "Check your email to see your new Password";
            }
            catch (Exception e)
            {
                feed = "Email not Sent" + e.Message;
            }
            return feed;
        }

        public string OrderCollected()
        {
            string feed = "";

            var b = new MailMessage()
            {
                Subject = "Collected Order",
                Body = body,
                IsBodyHtml = true,
            };
            b.From = new MailAddress("21122173@dut4life.ac.za", "Smokers Tavern");
            b.To.Add(to);
            SmtpClient smtp = new SmtpClient
            {
                Host = "pod51014.outlook.com",
                Port = 587,
                Credentials = new System.Net.NetworkCredential("21122173@dut4life.ac.za", "Dut920924"),
                EnableSsl = true
            };
            try
            {
                smtp.Send(b);
                feed = "Check your email to see your Order Details";
            }
            catch (Exception e)
            {
                feed = "Email not Sent" + e.Message;
            }
            return feed;
        }
        public string Online()
        {
            string feed = "";

            var m = new MailMessage()
            {
                Subject = "Online Payment",
                Body = body,
                IsBodyHtml = true,
            };
            m.From = new MailAddress("21122173@dut4life.ac.za", "Smokers Tavern");
            m.To.Add(to);
            SmtpClient smtp = new SmtpClient
            {
                Host = "pod51014.outlook.com",
                Port = 587,
                Credentials = new System.Net.NetworkCredential("21122173@dut4life.ac.za", "Dut920924"),
                EnableSsl = true
            };
            try
            {
                smtp.Send(m);
                feed = "Check your email to see Payment Details";
            }
            catch (Exception e)
            {
                feed = "Email not Sent" + e.Message;
            }
            return feed;
        }
    }
}
