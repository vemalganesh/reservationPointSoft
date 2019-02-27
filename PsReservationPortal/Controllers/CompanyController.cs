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


//Assign User under company
//Create Outlet
//Edit Outlet details and manager
//Edit company profile

namespace PsReservationPortal.Controllers
{
    [Authorize]
    public class CompanyController : Controller
    {
        private ApplicationDbContext _context;

        public CompanyController()
        {
            _context = new ApplicationDbContext();
        }
        
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var company = _context.UserExtraInfo.FirstOrDefault(a => a.UserId == userId).Companies.FirstOrDefault();
            List<OutletModel> outlets = GetOutletsUserAssociatedWith(company.Id);
            CompanyDashboardViewModel outletList = new CompanyDashboardViewModel();
            outletList.Outlets = outlets;
            return View(outletList);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(OutletModel outlet)
        {
            if(ModelState.IsValid)
            {
                outlet.DateTimeCreated = DateTime.UtcNow;
                outlet.DateTimeUpdated = DateTime.UtcNow;
                _context.Outlet.Add(outlet);
                _context.SaveChanges();
                return RedirectToAction("Index");
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
                return RedirectToAction("Index");
            }
            return View(outlet);
        }

        public ActionResult Details(int id)
        {
            OutletModel outlet = _context.Outlet.Find(id);
            return View(outlet);
        }

        [HttpPost]
        public ActionResult DeleteOutlet(int id)
        {
            OutletModel outlet = _context.Outlet.Find(id);
            _context.Outlet.Remove(outlet);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        private List<OutletModel> GetOutletsUserAssociatedWith(long companyid)
        {
            List<OutletModel> outletlist = new List<OutletModel>();
            outletlist = _context.Outlet.Where(c => c.Company.Id == companyid).ToList();
            return outletlist;
        }

        
    }
}