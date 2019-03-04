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
    [Authorize]
    public class OutletController : Controller
    {
        private ApplicationDbContext _context;

        public OutletController()
        {
            _context = new ApplicationDbContext();
        }
        
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            var outlet = _context.Outlet.Where(a => a.Managers.Any(b => b.UserId == userId)).FirstOrDefault();
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
                outlet.Company = company;
                _context.Outlet.Add(outlet);
                _context.Company.Find(company.Id).Outlets.Add(outlet);
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
                outlet.DateTimeUpdated = DateTime.UtcNow;
                _context.Entry(outlet).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
                return Redirect(Request.UrlReferrer.ToString());
            }
            return View(outlet);
        }
        
        [HttpPost]
        public ActionResult DeleteOutlet(int id)
        {
            var userId = User.Identity.GetUserId();
            var company = _context.UserExtraInfo.FirstOrDefault(a => a.UserId == userId).Companies.FirstOrDefault();
            OutletModel outlet = _context.Outlet.Find(id);
            _context.Company.Find(company.Id).Outlets.Remove(outlet);
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

        public OutletModel GetOneOutlet(long id)
        {
            var userId = User.Identity.GetUserId();
            var company = _context.UserExtraInfo.FirstOrDefault(a => a.UserId == userId).Companies.FirstOrDefault();
            var outlet = _context.Outlet.Where(a => a.Company.Id == company.Id).FirstOrDefault(a => a.Id == id);
            return outlet;
        }

        public List<OutletModel>GetAllOutlets()
        {
            var userId = User.Identity.GetUserId();
            var company = _context.UserExtraInfo.FirstOrDefault(a => a.UserId == userId).Companies.FirstOrDefault();
            return _context.Outlet.Where(a => a.Company.Id == company.Id).ToList();
        }
    }
}