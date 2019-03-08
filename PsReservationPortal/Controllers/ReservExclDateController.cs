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

namespace PsReservationPortal.Controllers
{
    [Authorize(Roles = "SuperAdmin,PointsoftSupport,CompanyAdmin,Manager")]
    public class ReservExclDateController : Controller
    {
        private ApplicationDbContext _context;

        public ReservExclDateController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            var outlet = GetOutletUserAssociated();
            List<ReservationExclusionDateModel> setting = _context.ReservationExclusionDate.Where(x => x.OutletId == outlet.Id).ToList();
            return View(setting);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ReservationExclusionDateModel setting)
        {
            OutletModel outlet = GetOutletUserAssociated();
            if (ModelState.IsValid)
            {
                setting.DateTimeCreated = DateTime.UtcNow;
                setting.DateTimeUpdated = DateTime.UtcNow;
                setting.OutletId = outlet.Id;
                setting.Outlet = outlet;
                outlet.ReservationExclusionDates.Add(setting);

                _context.ReservationExclusionDate.Add(setting);
                _context.Entry(outlet).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Edit(int id)
        {
            ReservationExclusionDateModel setting = GetOneExclDate(id);
            return View(setting);
        }

        [HttpPost]
        public ActionResult Edit(ReservationExclusionDateModel setting)
        {
            ReservationExclusionDateModel oldSetting = GetOneExclDate(setting.Id);

            if (ModelState.IsValid)
            {
                oldSetting.DateTimeUpdated = DateTime.UtcNow;
                oldSetting.ExlusionDateName = setting.ExlusionDateName;
                oldSetting.DateFrom = setting.DateFrom;
                oldSetting.DateTo = setting.DateTo;
                _context.Entry(oldSetting).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult DeleteSetting(int id)
        {
            OutletModel outlet = GetOutletUserAssociated();
            var setting = GetOneExclDate(id);

            outlet.ReservationExclusionDates.Remove(setting);
            _context.Entry(outlet).State = System.Data.Entity.EntityState.Modified;
            _context.ReservationExclusionDate.Remove(setting);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        private OutletModel GetOutletUserAssociated()
        {
            var userId = User.Identity.GetUserId();
            var outlet = _context.Outlet.Where(a => a.Managers.Any(b => b.UserId == userId)).FirstOrDefault();

            return outlet;
        }

        private ReservationExclusionDateModel GetOneExclDate(int id)
        {
            return _context.ReservationExclusionDate.FirstOrDefault(x => x.Id == id);
        }
    }
}