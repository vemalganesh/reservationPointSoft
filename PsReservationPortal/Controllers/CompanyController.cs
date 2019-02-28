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

//Change user roles
//Create Outlet
//Edit Outlet details
//Assign User to outlet
//Edit company profile

namespace PsReservationPortal.Controllers
{
    [Authorize]
    public class CompanyController : Controller
    {
        private ApplicationDbContext _context;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public CompanyController()
        {
            _context = new ApplicationDbContext();
        }
        public CompanyController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            _context = new ApplicationDbContext();
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var company = _context.UserExtraInfo.FirstOrDefault(a => a.UserId == userId).Companies.FirstOrDefault();
            var users = _context.Users.ToList();
            CompanyDashboardViewModel vm = new CompanyDashboardViewModel();
            List<OutletModel> outlets = GetOutletsUserAssociatedWith(company.Id);
            List<UserInfoViewModel> staffs = new List<UserInfoViewModel>();
            
            foreach(UserExtraInfoModel user in company.UserExtraInfos)
            {
                UserInfoViewModel userdetail = new UserInfoViewModel();
                var userinfo = _context.UserExtraInfo.Find(user.UserId);
                var userprofile = users.FirstOrDefault(a => a.Id == user.UserId);
                var outlet = outlets.Where(a => a.Managers.Contains(userinfo)).FirstOrDefault();
                userdetail.UserId = user.UserId;
                userdetail.Activated = user.Activated;
                if(outlet != null)
                {
                    userdetail.OutletName = outlet.Name + " - " + outlet.Location;
                }
                userdetail.Suspended = user.Suspended;
                userdetail.UserName = userprofile.UserName;
                userdetail.UserEmail = userprofile.Email;
                userdetail.UserRoles = GetUserRoles(user.UserId);
                staffs.Add(userdetail);
            }
            
            
            vm.Outlets = outlets;
            vm.Users = staffs;
            vm.Company = company;
            return View(vm);
        }
        
        public ActionResult Edit(int id)
        {
            CompanyModel company = _context.Company.Find(id);
            return View(company);
        }

        [HttpPost]
        public ActionResult Edit(CompanyModel company)
        {
            if(company.Name != "")
            {
                _context.Entry(company).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(company);
        }
        
        private List<OutletModel> GetOutletsUserAssociatedWith(long companyid)
        {
            List<OutletModel> outletlist = new List<OutletModel>();
            outletlist = _context.Outlet.Where(c => c.Company.Id == companyid).ToList();
            return outletlist;
        }

        private List<string> GetUserRoles(string userid)
        {
            List<string> rolelist = new List<string>();

            var userrole = UserManager.GetRolesAsync(userid);
            if (userrole != null)
            {
                foreach (var role in userrole.Result)
                {
                    rolelist.Add(role);
                }
            }
            return rolelist;
        }
    }
}