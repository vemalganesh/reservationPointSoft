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
    public class ReserveExclDayController : Controller
    {
        private ApplicationDbContext _context;

        public ReserveExclDayController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Edit(long outletId)
        {
            ReservationExclusionDayModel setting = _context.ReservationExclusionDay.FirstOrDefault(a => a.OutletId.Id == outletId);
            return View(setting);
        }
        

        [HttpPost]
        public ActionResult Edit(ReservationExclusionDayModel setting, long OutletId)
        {
            OutletModel outlet = _context.Outlet.FirstOrDefault(a => a.Id == OutletId);

            if (setting.DateTimeCreated == null)
            {
                setting.DateTimeCreated = DateTime.UtcNow;
                _context.Entry(outlet).State = System.Data.Entity.EntityState.Added;
            }
            else
            {
                setting.DateTimeUpdated = DateTime.UtcNow;
                _context.Entry(outlet).State = System.Data.Entity.EntityState.Modified;
            }

            setting.OutletId = outlet;
            _context.SaveChanges();

            return RedirectToAction("Index", "Outlet");
        }
    }
}