using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Microsoft.AspNet.Identity.Owin;
using PsReservationPortal.Models;
using PsReservationPortal.ViewModels;
using Kendo.Mvc.Extensions;

namespace PsReservationPortal.Controllers
{
    [Authorize(Roles ="SuperAdmin,PointsoftSupport")]

    public class MaintController : Controller
    {
        private ApplicationDbContext _context;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public MaintController()
        {
            _context = new ApplicationDbContext();
        }

        public MaintController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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

        // GET: Maint
        public ActionResult Index()
        {
            var datamodel = PrepareDashboardModel();

            return View(datamodel);
        }

        public ActionResult ProcessUser(string email)
        {
            ProcessUserViewModel modeldata = new ProcessUserViewModel
            {
                UserRegistrationInfo = GetUserRegistrationInfoByEmail(email),
                Companies = GetAllCompanies()
            };
                       

            return View(modeldata);
        }

        public ActionResult AddCompany(string useremail,string companyname)
        {
            var companystatus = _context.Company.FirstOrDefault(c => c.Name == companyname);

            if(companystatus==null)
            {
                CompanyModel company = new CompanyModel
                {
                    Name = companyname
                };

                _context.Company.Add(company);
                _context.SaveChanges();
            }                 


            return RedirectToAction("ProcessUser", new { email = useremail });
        }

        public ActionResult ActivateUser(string useremail, string companyname)
        {
            var sometrest = companyname;


            return RedirectToAction("ProcessUser", new { email = useremail });
        }


        #region Helpers
        private MaintDashboardViewModel PrepareDashboardModel()
        {
            MaintDashboardViewModel model = new MaintDashboardViewModel
            {
                UserRegistrationInfoList = _context.UserRegistrationInfo.ToList()
            };

            var userlist = _context.Users.ToList();
            if (userlist != null)
            {
                List<UserInfoViewModel> usersview = new List<UserInfoViewModel>();

                foreach (var user in userlist)
                {
                    var userxinfo = _context.UserExtraInfo.FirstOrDefault(u => u.UserId == user.Id);
                    if (userxinfo != null)
                    {
                        UserInfoViewModel userinfo = new UserInfoViewModel
                        {
                            UserId = user.Id,
                            UserEmail = user.Email,
                            EmailConfirmed = user.EmailConfirmed,
                            Activated = userxinfo.Activated,
                            Suspended = userxinfo.Suspended,
                            UserName = user.UserName,
                            UserRoles = GetUserRoles(user.Id),
                            Companies = GetCompaniesUserAssociatedWith(user.Id, user.Email)
                        };

                        usersview.Add(userinfo);
                    }
                }

                model.UserInfoList = usersview;
            }

            return model;
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

        private List<string> GetCompaniesUserAssociatedWith(string userid, string useremail)
        {
            List<string> companynamelist = new List<string>();

            var companies = _context.Company.Where(c => c.UserExtraInfos.Any(u => u.UserId == userid)).ToList();
            if (companies != null)
            {
                if (companies.Count > 0)
                {
                    foreach (var company in companies)
                    {
                        companynamelist.Add(company.Name);
                    }
                }
                else
                {
                    var userreginfo = _context.UserRegistrationInfo.FirstOrDefault(ur => ur.Email == useremail);
                    if (userreginfo != null)
                    {
                        companynamelist.Add(userreginfo.CompanyName);
                    }
                }
            }
            return companynamelist;
        }

        private List<CompanyModel> GetAllCompanies()
        {
            var companies = _context.Company.ToList();

            return companies;
        }

        private UserRegistrationInfoModel GetUserRegistrationInfoByEmail(string email)
        {
            var reginfo = _context.UserRegistrationInfo.FirstOrDefault(r => r.Email == email);

            return reginfo;
        }

        #endregion




    }
}