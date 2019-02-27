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
            return View();
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