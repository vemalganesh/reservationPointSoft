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
using Microsoft.AspNet.Identity.EntityFramework;

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
            AddCompany(companyname);

            return RedirectToAction("ProcessUser", new { email = useremail });
        }

        public ActionResult ActivateUser(string useremail, string companyname)
        {
            //add the company into company table if it does not exists and both cases return the company id
            //if there is an error it will return -1;
            var companyid = AddCompany(companyname);

            if(companyid>0)
            {
                //get the use data by the user e-mail address
                var user = _context.Users.FirstOrDefault(u => u.Email == useremail);

                //if don't have the user then return to the process user page
                if (user == null)
                    return RedirectToAction("ProcessUser", new { email = useremail });

                //get the user extra info from the UserExtraInfo Table
                var userextrainfo = _context.UserExtraInfo.FirstOrDefault(u => u.UserId == user.Id);

                //if user extra info does not exists then return to the process user page
                if (userextrainfo == null)
                    return RedirectToAction("ProcessUser", new { email = useremail });

                //get the user registration data from the UserRegistration table
                var userregistrationinfo = _context.UserRegistrationInfo.FirstOrDefault(ur => ur.Email == useremail);
                if (userregistrationinfo != null)
                {
                    //remove the entry from the table before we start populating other tables
                    _context.UserRegistrationInfo.Remove(userregistrationinfo);
                }

                //get the user company from the company id we got with relation to the company name
                var usercompany = _context.Company.FirstOrDefault(c => c.Id == companyid);
                if (usercompany != null)
                {
                    //now we update the Company table with the information of the user and let entity be aware of the link
                    if(usercompany.UserExtraInfos!=null)
                    {
                        usercompany.UserExtraInfos.Add(userextrainfo);
                    }
                    else
                    {
                        usercompany.UserExtraInfos = new List<UserExtraInfoModel>
                        {
                            userextrainfo
                        };
                    }
                                                         

                    //now we update the user extra info table and also let entity framework be aware of the link to company table
                    userextrainfo.Activated = true;
                    userextrainfo.Companies.Add(usercompany);
                }
                //now we save all the changes
                _context.SaveChanges();
            }
            
            //finally return to the dashboard
            return RedirectToAction("Index");

        }

        public ActionResult RemoveRegistration(string email)
        {
            var reginfo = _context.UserRegistrationInfo.FirstOrDefault(rg => rg.Email == email);

            if(reginfo!=null)
            {
                _context.UserRegistrationInfo.Remove(reginfo);

                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult ToggleSelectedUserSuspendStatus(string email)
        {
            string suspendstatus = "";
            var seluser = _context.Users.FirstOrDefault(u => u.Email == email);

            if(seluser!=null)
            {
                var seluserxtra = _context.UserExtraInfo.FirstOrDefault(x => x.UserId == seluser.Id);
                if(seluserxtra!=null)
                {
                    seluserxtra.Suspended = !seluserxtra.Suspended;

                    _context.SaveChanges();

                    suspendstatus = seluserxtra.Suspended.ToString().ToLower();
                }
            }

            return Json(new { success = true, message = suspendstatus }, JsonRequestBehavior.AllowGet);
            //return RedirectToAction("Index");
        }
        
        public ActionResult EditUser(string userid)
        {           
            var user = _context.Users.FirstOrDefault(u => u.Id == userid);

            if(user==null)
            {
                return RedirectToAction("Index");
            }

            var userextrainfo = _context.UserExtraInfo.FirstOrDefault(x => x.UserId == userid);

            if (userextrainfo == null)
                return RedirectToAction("Index");

            var userinfo = new EditUserViewModel
            {
                Activated = userextrainfo.Activated,
                //Companies = GetAllCompanies(),
                EmailConfirmed = user.EmailConfirmed,
                Suspended = userextrainfo.Suspended,
                UserEmail = user.Email,
                UserId = user.Id,
                UserRoles = GetUserRolewithMultiSelect(user.Id),
                Companies = GetUserCompanywithMultiSelect(user.Id)
            };




            return View(userinfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SaveUserChanges(EditUserViewModel model,long[] companiesId,string[] rolesid)
        {
            var userInDb = _context.Users.SingleOrDefault(u => u.Id == model.UserId);

            if (userInDb == null)
                return RedirectToAction("EditUser", model.UserId);

            userInDb.EmailConfirmed = model.EmailConfirmed;

            var userroles = _context.Roles.Where(r => rolesid.Contains(r.Id)).ToList();

            //remove all user current roles
            var currentroles = await UserManager.GetRolesAsync(model.UserId);
            await UserManager.RemoveFromRolesAsync(model.UserId, currentroles.ToArray());

            //add new selected roles to user
            foreach(var selecteduserole in userroles)
            {
                await UserManager.AddToRoleAsync(model.UserId, selecteduserole.Name);
            }

            //remove all companies associated with user via userextrainfo table
            var currentuser = _context.UserExtraInfo.FirstOrDefault(r => r.UserId == model.UserId);
            List<CompanyModel> list = currentuser.Companies.ToList();
            foreach(CompanyModel c in list)
            {
                currentuser.Companies.Remove(c);
            }
            
            //add all new selected companies to users
            foreach(var id in companiesId)
            {
                var company = _context.Company.FirstOrDefault(r => r.Id == id);
                currentuser.Companies.Add(company);
            }

            //presist all changes to database
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        #region Helpers

        private long AddCompany(string companyname)
        {
            long retid = -1;

            var companystatus = _context.Company.FirstOrDefault(c => c.Name == companyname);

            if (companystatus == null)
            {
                CompanyModel company = new CompanyModel
                {
                    Name = companyname
                };

                _context.Company.Add(company);
                _context.SaveChanges();

                retid = company.Id;
            }
            else
            {
                retid = companystatus.Id;
            }

            return retid;
        }

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

        private MultiSelectList GetUserCompanywithMultiSelect(string userid)
        {
            string associatedcompanyid="";
            var companies = _context.Company.Where(c => c.UserExtraInfos.Any(u => u.UserId == userid)).ToList();
            foreach(var comp in companies)
            {
                associatedcompanyid = associatedcompanyid + comp.Id.ToString() + ",";
            }
            if (associatedcompanyid.EndsWith(","))
                associatedcompanyid = associatedcompanyid.Remove(associatedcompanyid.Length - 1);

            var allcompany = _context.Company.ToList();

            MultiSelectList retmlist = new MultiSelectList(allcompany, "Id", "Name", new[] { associatedcompanyid });

            return retmlist;

        }

        private MultiSelectList GetUserRolewithMultiSelect(string userid)
        {
            string associatedroleid = "";
            var roles = _context.Roles.Where(r => r.Users.Any(u => u.UserId == userid)).ToList();
            foreach(var role in roles)
            {
                associatedroleid = associatedroleid + role.Id + ",";
            }
            if (associatedroleid.EndsWith(","))
                associatedroleid = associatedroleid.Remove(associatedroleid.Length - 1);

            var allroles = _context.Roles.ToList();

            MultiSelectList retmlist = new MultiSelectList(allroles, "Id", "Name", new[] { associatedroleid });

            return retmlist;

        }

        private List<CompanyModel> GetAllCompanies()
        {
            var companies = _context.Company.ToList();

            return companies;
        }

        private IEnumerable<IdentityRole> GetAllRoles()
        {
            var roles = _context.Roles.ToList();

            return roles;
        }

        private UserRegistrationInfoModel GetUserRegistrationInfoByEmail(string email)
        {
            var reginfo = _context.UserRegistrationInfo.FirstOrDefault(r => r.Email == email);

            return reginfo;
        }

        #endregion




    }
}