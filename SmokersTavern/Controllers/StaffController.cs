using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using SmokersTavern.Business.Business_Logic;
using SmokersTavern.Business.Interfaces;
using SmokersTavern.Data;
using SmokersTavern.Data.Models;
using SmokersTavern.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static SmokersTavern.App_Start.IdentityConfig;


//ZAin
namespace SmokersTavern.Controllers
{
    //[Authorize]
    public class StaffController : Controller
    {

        RepositoryService<Staff> su = new RepositoryService<Staff>(new ApplicationDbContext());
        public UserManager<ApplicationUser> UserManager { get; set; }
        public ApplicationDbContext DbContext = new ApplicationDbContext();
        private IStaffBusiness _objStaffBusiness = new StaffBusiness();

        public StaffController()
        {
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        }

        public StaffController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }


        // GET: Register

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public ActionResult RegisterStaff()
        {

            var roles = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(DbContext));
            ViewBag.RoleId = new SelectList(roles.Roles.ToList(), "Name", "Name");

            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult> RegisterStaff(StaffViewModel objStaff)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(DbContext));

            ViewBag.RoleId = new SelectList(roleManager.Roles.ToList(), "Name", "Name");
           
            EmailBusiness obj = new EmailBusiness();

            if (ModelState.IsValid)
            {
                var find = _objStaffBusiness.GetAll().Where(x => x.Username.ToLower() == objStaff.Username.ToLower());

                if (find.Count() > 0)
                {
                    TempData["username"] = "Username already taken";
                    return View(objStaff);
                }

                try
                {
                    var user = new ApplicationUser()
                    {
                        UserName = objStaff.Username,
                        Email = objStaff.Email,
                        PhoneNumber = objStaff.Cellphone,

                    };

                    //IdentityResult 

                    var result = await UserManager.CreateAsync(user, objStaff.Password);
                    _objStaffBusiness.Insert(objStaff);

                    if (result.Succeeded)
                    {
                        var roleResult = await UserManager.AddToRoleAsync(user.Id, objStaff.Role);
                    }
                    obj.to = new MailAddress(objStaff.Email);
                    obj.body = "Hi " +" "+ objStaff.FirstName + " You Have Been Registered Sucessfully As A Shanz Hair Salon Employee" + "." + "Details Are as follows:<br/>" + "Username: " + objStaff.Username + "<br/>Password: " + objStaff.Password + "<br/><br/>Kind Regards<br/>Shanz Hair Salon";                 
                    return RedirectToAction("GetAllStaff","Staff");
                }

                catch (Exception e)
                {
                    ModelState.AddModelError(string.Empty, "Error! " + e.Message);
                }


            }

            ViewBag.RoleId = new SelectList(roleManager.Roles.ToList(), "Name", "Name");
            return View(objStaff);

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult GetAllStaff()
        {
            var staffBusiness = new StaffBusiness();
            TempData["Staff"] = staffBusiness.GetAll().Count();
            return View(staffBusiness.GetAll().ToList());
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            StaffBusiness ee = new StaffBusiness();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(DbContext));
            ViewBag.RoleId = new SelectList(roleManager.Roles.ToList(), "Name", "Name");
            return View(ee.GETeditMethod(id));

        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(StaffViewModel model, int id)
        {
            StaffBusiness sb = new StaffBusiness();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(DbContext));
            ViewBag.RoleId = new SelectList(roleManager.Roles.ToList(), "Name", "Name");
            sb.posteditMethod(model);
            return RedirectToAction("GetAllStaff","Staff");

        }
    }
}