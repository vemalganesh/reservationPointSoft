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
    [Authorize(Roles = "SuperAdmin,PointsoftSupport,CompanyAdmin")]
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

            var company = GetUserCompany();
            var users = _context.Users.ToList();
            CompanyDashboardViewModel vm = new CompanyDashboardViewModel();
            List<OutletModel> outlets = GetOutletsUserAssociatedWith(company.Id);
            List<UserInfoViewModel> staffs = new List<UserInfoViewModel>();

            foreach (UserExtraInfoModel user in company.UserExtraInfos)
            {
                UserInfoViewModel userdetail = new UserInfoViewModel();
                var userprofile = users.FirstOrDefault(a => a.Id == user.UserId);
                var outlet = outlets.Where(a => a.Managers != null && a.Managers.Select(b => b.UserId).Contains(user.UserId)).FirstOrDefault();
                userdetail.UserId = user.UserId;
                userdetail.Activated = user.Activated;
                if (outlet != null)
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
                CompanyModel oldCompany = GetCompanyById(company.Id);
                company.Outlets = oldCompany.Outlets;
                company.UserExtraInfos = oldCompany.UserExtraInfos;
                _context.Entry(company).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(company);
        }

        public ActionResult ManageStaff(string id)
        {
            var user = _context.Users.FirstOrDefault(a => a.Id == id);
            EditUserViewModel vm = new EditUserViewModel();
            vm.UserId = user.Id;
            vm.UserEmail = user.Email;
            vm.UserRoles = GetUserRolewithMultiSelect(id);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ManageStaff(string userId, string[] rolesid)
        {
            if (rolesid != null)
            {
                var userInDb = _context.Users.SingleOrDefault(u => u.Id == userId);
                
                var userroles = _context.Roles.Where(r => rolesid.Contains(r.Id)).ToList();

                //remove all user current roles
                var currentroles = await UserManager.GetRolesAsync(userId);
                await UserManager.RemoveFromRolesAsync(userId, currentroles.ToArray());

                //add new selected roles to user
                foreach (var selecteduserole in userroles)
                {
                    await UserManager.AddToRoleAsync(userId, selecteduserole.Name);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Company");
            }
            return View();
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

        private MultiSelectList GetUserRolewithMultiSelect(string userid)
        {
            string associatedroleid = "";
            var roles = _context.Roles.Where(r => r.Users.Any(u => u.UserId == userid)).ToList();
            foreach (var role in roles)
            {
                if(role.Name == "CompanyAdmin" || role.Name == "Manager")
                associatedroleid = associatedroleid + role.Id + ",";
            }
            if (associatedroleid.EndsWith(","))
                associatedroleid = associatedroleid.Remove(associatedroleid.Length - 1);

            var allroles = _context.Roles.Where(x => x.Name == "CompanyAdmin" || x.Name == "Manager" || x.Name == "Users").ToList();

            MultiSelectList retmlist = new MultiSelectList(allroles, "Id", "Name", new[] { associatedroleid });

            return retmlist;

        }

        private CompanyModel GetUserCompany()
        {
            var userId = User.Identity.GetUserId();
            var company = _context.UserExtraInfo.FirstOrDefault(a => a.UserId == userId).Companies.FirstOrDefault();

            return company;
        }

        private CompanyModel GetCompanyById(long Id)
        {
            var company = _context.Company.FirstOrDefault(x=>x.Id == Id);

            return company;
        }


    }
}