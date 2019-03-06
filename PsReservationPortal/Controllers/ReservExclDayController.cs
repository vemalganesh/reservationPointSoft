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
    public class ReservExclDayController : Controller
    {
        private ApplicationDbContext _context;

        public ReservExclDayController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            return RedirectToAction("Index", "Outlet");
        }

        //get for view
        public ActionResult Edit(long outletId)
        {
            return View(GetReservExclDayByOutletId(outletId));
        }
        
        [HttpPost]
        public ActionResult Edit(ReservationExclusionDayModel setting)
        {
            ReservationExclusionDayModel oldSetting = GetReservExclDayByOutletId(setting.Id);
            oldSetting.Monday = setting.Monday;
            oldSetting.Tuesday = setting.Tuesday;
            oldSetting.Wednesday = setting.Wednesday;
            oldSetting.Thursday = setting.Thursday;
            oldSetting.Friday = setting.Friday;
            oldSetting.Saturday = setting.Saturday;
            oldSetting.Sunday = setting.Sunday;
            oldSetting.DateTimeUpdated = DateTime.UtcNow;
            _context.Entry(oldSetting).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();

            return RedirectToAction("Index", "Outlet");
        }

        public OutletModel GetOneOutlet(long Id)
        {
            OutletModel outlet = _context.Outlet.FirstOrDefault(a => a.Id == Id);
            return outlet;
        }

        public ReservationExclusionDayModel GetReservExclDayByOutletId(long OutletId)
        {
            ReservationExclusionDayModel setting = _context.ReservationExclusionDay.FirstOrDefault(a => a.Outlet.Id == OutletId);
            return setting;
        }
        
        public ReservationExclusionDayModel GetOneSetting(long id)
        {
            ReservationExclusionDayModel setting = _context.ReservationExclusionDay.FirstOrDefault(a => a.Id == id);
            return setting;
        }
    }
}