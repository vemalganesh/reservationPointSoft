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
    public class TableController : Controller
    {
        private ApplicationDbContext _context;

        public TableController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            var outlet = GetOutletUserAssociated();
            List<TableModel> setting = _context.Table.Where(x => x.OutletId == outlet.Id).ToList();
            return View(setting);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(TableModel table)
        {
            OutletModel outlet = GetOutletUserAssociated();
            if (ModelState.IsValid)
            {
                table.DateTimeCreated = DateTime.UtcNow;
                table.DateTimeUpdated = DateTime.UtcNow;
                table.OutletId = outlet.Id;
                table.Outlet = outlet;
                outlet.Tables.Add(table);

                _context.Table.Add(table);
                _context.Entry(outlet).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Edit(long id)
        {
            TableModel table = GetOneTable(id);
            return View(table);
        }

        [HttpPost]
        public ActionResult Edit(TableModel table)
        {
            TableModel oldTable = GetOneTable(table.Id);

            if (ModelState.IsValid)
            {
                oldTable.DateTimeUpdated = DateTime.UtcNow;
                oldTable.TableNumber = table.TableNumber;
                oldTable.Pax = table.Pax;
                oldTable.isActive = table.isActive;
                _context.Entry(oldTable).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult DeleteTable(long id)
        {
            OutletModel outlet = GetOutletUserAssociated();
            var table = GetOneTable(id);

            outlet.Tables.Remove(table);
            _context.Entry(outlet).State = System.Data.Entity.EntityState.Modified;
            _context.Table.Remove(table);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        private OutletModel GetOutletUserAssociated()
        {
            var userId = User.Identity.GetUserId();
            var outlet = _context.Outlet.Where(a => a.Managers.Any(b => b.UserId == userId)).FirstOrDefault();

            return outlet;
        }

        private TableModel GetOneTable(long id)
        {
            return _context.Table.FirstOrDefault(x => x.Id == id);
        }
    }
}