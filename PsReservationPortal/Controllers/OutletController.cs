using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PsReservationPortal.Models;
using PsReservationPortal.ViewModels;
using Kendo.Mvc.Extensions;
using Microsoft.AspNet.Identity.EntityFramework;

//Edit Outlet details
//Mange reservation settings
//Manage reservation

namespace PsReservationPortal.Controllers
{
    [Authorize(Roles = "SuperAdmin,PointsoftSupport,CompanyAdmin,Manager")]
    public class OutletController : Controller
    {
        private ApplicationDbContext _context;

        public OutletController()
        {
            _context = new ApplicationDbContext();
        }
        
        public ActionResult Index()
        {
            var outlet = GetOutletUserAssociated();
            OutletDashboardViewModel vm = new OutletDashboardViewModel();
            vm.Outlet = outlet;
            return View(vm);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(OutletModel outlet)
        {
            var userId = User.Identity.GetUserId();
            var company = _context.UserExtraInfo.FirstOrDefault(a => a.UserId == userId).Companies.FirstOrDefault();
            if (ModelState.IsValid)
            {
                outlet.DateTimeCreated = DateTime.UtcNow;
                outlet.DateTimeUpdated = DateTime.UtcNow;

                ReservationExclusionDayModel ReservExclDay = new ReservationExclusionDayModel();
                ReservExclDay.Outlet = outlet;
                ReservExclDay.DateTimeCreated = DateTime.UtcNow;
                ReservExclDay.DateTimeUpdated = DateTime.UtcNow;
                _context.ReservationExclusionDay.Add(ReservExclDay);

                outlet.ReservationExclusionDay = ReservExclDay;
                outlet.CompanyId = company.Id;
                outlet.Company = company;

                company.Outlets.Add(outlet);
                _context.Outlet.Add(outlet);
                _context.Entry(company).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();

                return RedirectToAction("Index","Company");
            }

            return View();
        }

        public ActionResult Edit(int id)
        {
            OutletModel outlet = _context.Outlet.Find(id);
            return View(outlet);
        }

        [HttpPost]
        public ActionResult Edit(OutletModel outlet)
        {
            if(ModelState.IsValid)
            {
                OutletModel oldOutlet = GetOneOutlet(outlet.Id);
                oldOutlet.DateTimeUpdated = DateTime.UtcNow;
                oldOutlet.Name = outlet.Name;
                oldOutlet.Location = outlet.Location;
                oldOutlet.Address = outlet.Address;
                oldOutlet.PhoneNum = outlet.PhoneNum;
                oldOutlet.ContactPersonPhoneNum = outlet.ContactPersonPhoneNum;
                oldOutlet.Description = outlet.Description;
                oldOutlet.isActive = outlet.isActive;
                _context.Entry(outlet).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
                return Redirect(Request.UrlReferrer.ToString());
            }
            return View(outlet);
        }
        
        public ActionResult DeleteOutlet(int id)
        {
            var userId = User.Identity.GetUserId();
            var company = _context.UserExtraInfo.FirstOrDefault(a => a.UserId == userId).Companies.FirstOrDefault();
            OutletModel outlet = GetOneOutlet(id);
            company.Outlets.Remove(outlet);
            _context.Entry(company).State = System.Data.Entity.EntityState.Modified;
            _context.Outlet.Remove(outlet);
            _context.SaveChanges();
            return RedirectToAction("Index","Company");
        }

        public ActionResult AssignOutlet(string id)
        {
            var user = _context.Users.FirstOrDefault(a => a.Id == id);
            var userinfo = _context.UserExtraInfo.FirstOrDefault(a => a.UserId == id);
            var usercomp = userinfo.Companies.FirstOrDefault();
            AssignOutletViewModel vm = new AssignOutletViewModel();
            UserInfoViewModel uservm = new UserInfoViewModel();
            uservm.UserId = user.Id;
            uservm.UserName = user.UserName;
            vm.User = uservm;
            vm.Outlets = _context.Outlet.Where(a => a.Company.Id == usercomp.Id).ToList();
            return View(vm);
        }

        [HttpPost]
        public ActionResult AssignOutlet(long outletId, string userId)
        {
            if (outletId > 0)
            {
                UserExtraInfoModel user = _context.UserExtraInfo.Find(userId);
                var outlet = GetOneOutlet(outletId);
                if(outlet.Managers == null)
                {
                    List<UserExtraInfoModel> managers = new List<UserExtraInfoModel>();
                    outlet.Managers = managers;
                }
                outlet.Managers.Add(user);
                _context.Entry(outlet).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index","Company");
            }
            return View();
        }

        public ActionResult UnAssignOutlet(string userId)
        {
            var user = _context.UserExtraInfo.FirstOrDefault(a => a.UserId == userId);
            //Need to change if we allow 1 manager to manage multiple Outlets
            OutletModel outlet = _context.Outlet.Where(a=> a.Managers.Any(b => b.UserId == userId)).FirstOrDefault();

            outlet.Managers.Remove(user);
            _context.Entry(outlet).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index","Company");
        }

        public ActionResult EditReservSetting(long outletId)
        {
            return View(GetOneOutlet(outletId));
        }

        [HttpPost]
        public ActionResult EditReservSetting(OutletModel outlet)
        {
            OutletModel oldOutlet = GetOneOutlet(outlet.Id);
            oldOutlet.ReservationAllowBefore = outlet.ReservationAllowBefore;
            oldOutlet.ReservationDuration = outlet.ReservationDuration;
            outlet.DateTimeUpdated = DateTime.UtcNow;

            _context.Entry(oldOutlet).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index", "Outlet");
        }

        private OutletModel GetOneOutlet(long id)
        {
            var userId = User.Identity.GetUserId();
            var company = _context.UserExtraInfo.FirstOrDefault(a => a.UserId == userId).Companies.FirstOrDefault();
            var outlet = _context.Outlet.Where(a => a.Company.Id == company.Id).FirstOrDefault(a => a.Id == id);
            return outlet;
        }

        private List<OutletModel>GetAllOutlets()
        {
            var userId = User.Identity.GetUserId();
            var company = _context.UserExtraInfo.FirstOrDefault(a => a.UserId == userId).Companies.FirstOrDefault();
            return _context.Outlet.Where(a => a.Company.Id == company.Id).ToList();
        }

        private OutletModel GetOutletUserAssociated()
        {
            var userId = User.Identity.GetUserId();
            var outlet = _context.Outlet.Where(a => a.Managers.Any(b => b.UserId == userId)).FirstOrDefault();

            return outlet;
        }
    }
}