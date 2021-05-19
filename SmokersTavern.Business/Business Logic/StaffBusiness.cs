//Vishalen
using SmokersTavern.Business.Interfaces;
using SmokersTavern.Model;
using SmokersTavern.Service;
using SmokersTavern.Data;
using Microsoft.Owin.Security;
using System.Web;
using SmokersTavern.Data.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace SmokersTavern.Business.Business_Logic
{
    public class StaffBusiness : IStaffBusiness
    {
        public void Insert(StaffViewModel model)
        {
            using (var staffRepo = new StaffRepository())
            {
                var objStaff = new Staff()
                {
                    UserName = model.Username,
                    FirstName = model.FirstName,
                    Surname = model.Surname,
                    Position = model.Position,
                    Email = model.Email,
                    CellPhone = model.Cellphone,
                    Password = model.Password,
                    ApplicationUser = model.ApplicationUser
                };
                staffRepo.Insert(objStaff);

            }
        }
        public List<StaffViewModel> GetAll()
        {
            var staff = new List<StaffViewModel>();
            using (var staffRepo = new StaffRepository())
            {
                staff = staffRepo.GetAll().Select(x => new StaffViewModel
                {
                    Id=x.Id,
                    Username = x.UserName,
                    FirstName = x.FirstName,
                    Surname = x.Surname,
                    Role = x.Position,
                    Email = x.Email,
                    Cellphone = x.CellPhone,
                    Password = x.Password,
                    ApplicationUser = x.ApplicationUser
                }).ToList();
            }
            return staff;
        }

        public StaffViewModel GetStaffById(int id)
        {
            {

                using (var staffRepository = new StaffRepository())
                {

                    Staff staff = staffRepository.GetById(id);
                    var objStaff = new StaffViewModel();
                    if (staff != null)
                    {
                        objStaff.Username = staff.UserName;
                        objStaff.FirstName = staff.FirstName;
                        objStaff.Surname = staff.Surname;
                        objStaff.Email = staff.Email;
                        objStaff.Cellphone = staff.CellPhone;
                        objStaff.Role = staff.Position;
                    }
                    return objStaff;
                }
            }
        }

        public void Update(StaffViewModel model)
        {

            using (var staffRepo = new StaffRepository())
            {
                var dis = new Staff();
                dis = staffRepo.GetById(model.Id);
                if (dis != null)
                {
                    dis.FirstName = model.FirstName;
                    dis.Surname = model.Surname;
                    dis.Email = model.Email;
                    dis.CellPhone = model.Cellphone;
                    dis.Position = model.Role;
                    dis.Id = model.Id;

                    staffRepo.Update(dis);
                }
            }
        }

        public StaffViewModel GETeditMethod(int? id)
        {
            StaffViewModel cv = new StaffViewModel();

            using (var s = new StaffRepository())
            {
                if (id.HasValue && id != 0)
                {
                    Staff _staff = s.GetById(id.Value);

                    cv.Id = _staff.Id;
                    cv.Username = _staff.UserName;
                    cv.FirstName = _staff.FirstName;
                    cv.Surname = _staff.Surname;
                    cv.Cellphone = _staff.CellPhone;
                    cv.Password = _staff.Password;
                }

                return cv;
            }
        }


        public StaffViewModel posteditMethod(StaffViewModel model)
        {
            using (var s = new StaffRepository())
            {
                    Staff _staff = s.GetById(model.Id);
                    _staff.Id = model.Id;
                    _staff.UserName = model.Username;
                    _staff.FirstName = model.FirstName;
                    _staff.Surname = model.Surname;
                    _staff.CellPhone = model.Cellphone;
                    _staff.Password = model.Password;
                    _staff.Position = model.Role;
                    s.Update(_staff);

                return model;
            }
        }

    }
}
