using SmokersTavern.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Web.Mvc;
using static SmokersTavern.App_Start.IdentityConfig;
using System.Threading.Tasks;
using SmokersTavern.Model;
using System.Net;


//Rivashan/Tryvynne
namespace SmokersTavern.Controllers
{
    //[Authorize]
    public class RoleController : Controller
    {
        public ApplicationDbContext db = new ApplicationDbContext();

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            set
            {
                _userManager = value;
            }
        }


        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            return View(RoleManager.Roles);
        }
        //[Authorize(Roles = "Admin")]
        // GET: RoleAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RoleAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
       // [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create([Bind(Include = "Id,RoleName")] RoleViewModel model)
        {
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            if (ModelState.IsValid)
            {
                IdentityResult result = null;

                if (!RoleManager.RoleExists(model.RoleName))
                {
                    result = await RoleManager.CreateAsync(new IdentityRole(model.RoleName));
                }
                else
                {
                    ModelState.AddModelError("", "Role already exists.");
                }

                try
                {
                    if (result.Succeeded)
                        return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Role not Saved.");
                }

            }

            return View(model);
        }
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Details(string id)
        {
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = await RoleManager.FindByIdAsync(id);
            // Get the list of Users in this Role
            var users = new List<ApplicationUser>();

            // Get the list of Users in this Role
            foreach (var user in UserManager.Users.ToList())
            {
                if (await UserManager.IsInRoleAsync(user.Id, role.Name))
                {
                    users.Add(user);
                }
            }

            ViewBag.Users = users;
            ViewBag.UserCount = users.Count();
            return View(role);
        }
    }
}