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
    public class OperationTypeController : Controller
    {
        private ApplicationDbContext _context;

        public OperationTypeController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index(long outletId)
        {
            List<OperationTypeModel> setting = _context.OperationType.Where(x => x.OutletId == outletId).ToList();
            return View(setting);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(OperationTypeModel setting)
        {
            var userId = User.Identity.GetUserId();
            var outlet = _context.Outlet.FirstOrDefault(x => x.Managers.Any(z => z.UserId == userId));
            if (ModelState.IsValid)
            {
                setting.DateTimeCreated = DateTime.UtcNow;
                setting.DateTimeUpdated = DateTime.UtcNow;
                setting.OutletId = outlet.Id;
                setting.Outlet = outlet;
                outlet.OperationTypes.Add(setting);

                _context.OperationType.Add(setting);
                _context.Entry(outlet).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();

                return RedirectToAction("Index", "OperationType", new { outletId = outlet.Id });
            }

            return View();
        }

        public ActionResult Edit(int id)
        {
            OperationTypeModel setting = _context.OperationType.Find(id);
            return View(setting);
        }

        [HttpPost]
        public ActionResult Edit(OperationTypeModel setting)
        {
            OperationTypeModel oldSetting = GetOneOperationType(setting.Id);
            if (ModelState.IsValid)
            {
                oldSetting.DateTimeUpdated = DateTime.UtcNow;
                oldSetting.Name = setting.Name;
                oldSetting.isAllowReserve = setting.isAllowReserve;

                _context.Entry(oldSetting).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "OperationType", new { outletId = oldSetting.OutletId });
        }

        public ActionResult DeleteType(int id)
        {
            var userId = User.Identity.GetUserId();
            var outlet = _context.Outlet.FirstOrDefault(x => x.Managers.Any(z => z.UserId == userId));
            var setting = GetOneOperationType(id);
            
            outlet.OperationTypes.Remove(setting);
            _context.Entry(outlet).State = System.Data.Entity.EntityState.Modified;
            _context.OperationType.Remove(setting);
            _context.SaveChanges();

            return RedirectToAction("Index", "OperationType", new { outletId = outlet.Id });
        }

        public OperationTypeModel GetOneOperationType (int id)
        {
            return _context.OperationType.FirstOrDefault(x => x.Id == id);
        }
    }
}