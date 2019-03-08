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
    public class OperationHourSettingController : Controller
    {
        private ApplicationDbContext _context;

        public OperationHourSettingController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            var outlet = GetOutletUserAssociated();
            List<OperationHourSettingModel> setting = _context.OperationHourSetting.Where(x => x.OutletId == outlet.Id).ToList();
            return View(setting);
        }


        public ActionResult Create()
        {
            OutletModel outlet = GetOutletUserAssociated();
            ViewBag.OperationTypes = GetOperationTypesByOutletId(outlet.Id);
            return View();
        }

        [HttpPost]
        public ActionResult Create(OperationHourSettingModel setting)
        {
            OutletModel outlet = GetOutletUserAssociated();
            if (ModelState.IsValid)
            {
                setting.DateTimeCreated = DateTime.UtcNow;
                setting.DateTimeUpdated = DateTime.UtcNow;
                setting.OutletId = outlet.Id;
                setting.Outlet = outlet;
                setting.OperationType = GetOneOperationType(setting.OperationTypeId);
                outlet.OperationHourSettings.Add(setting);

                _context.OperationHourSetting.Add(setting);
                _context.Entry(outlet).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Edit(int id)
        {
            OutletModel outlet = GetOutletUserAssociated();
            ViewBag.OperationTypes = GetOperationTypesByOutletId(outlet.Id);
            OperationHourSettingModel setting = GetOneOperationHour(id);
            return View(setting);
        }

        [HttpPost]
        public ActionResult Edit(OperationHourSettingModel setting)
        {
            OperationHourSettingModel oldSetting = GetOneOperationHour(setting.Id);

            if (ModelState.IsValid)
            {
                oldSetting.DateTimeUpdated = DateTime.UtcNow;
                oldSetting.Day = setting.Day;
                oldSetting.StartHour = setting.StartHour;
                oldSetting.StartMinute = setting.StartMinute;
                oldSetting.EndHour = setting.EndHour;
                oldSetting.EndMinute = setting.EndMinute;
                oldSetting.OperationTypeId = setting.OperationTypeId;
                oldSetting.OperationType = GetOneOperationType(setting.OperationTypeId);
                _context.Entry(oldSetting).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult DeleteSetting(int id)
        {
            OutletModel outlet = GetOutletUserAssociated();
            var setting = GetOneOperationHour(id);

            outlet.OperationHourSettings.Remove(setting);
            _context.Entry(outlet).State = System.Data.Entity.EntityState.Modified;
            _context.OperationHourSetting.Remove(setting);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        
        private OperationHourSettingModel GetOneOperationHour(int id)
        {
            return _context.OperationHourSetting.FirstOrDefault(x => x.Id == id);
        }

        private List<OperationHourSettingModel> GetOperationHourSettingsByOutletId(long outletId)
        {
            return _context.OperationHourSetting.Where(x => x.OutletId == outletId).ToList();
        }

        private OutletModel GetOutletUserAssociated()
        {
            var userId = User.Identity.GetUserId();
            var outlet = _context.Outlet.Where(a => a.Managers.Any(b => b.UserId == userId)).FirstOrDefault();

            return outlet;
        }

        private List<OperationTypeModel> GetOperationTypesByOutletId(long outletId)
        {
            return _context.OperationType.Where(x => x.OutletId == outletId).ToList();
        }

        private OperationTypeModel GetOneOperationType(int id)
        {
            return _context.OperationType.FirstOrDefault(x => x.Id == id);
        }
    }
}