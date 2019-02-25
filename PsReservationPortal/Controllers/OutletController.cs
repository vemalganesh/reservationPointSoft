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
    [Authorize]
    public class OutletController : Controller
    {
        private ApplicationDbContext _context;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            OutletModel outlet = _context.Outlet.FirstOrDefault(a => a.OutletId == id);
            return View(outlet);
        }

        public ActionResult Details(int id)
        {
            OutletModel outlet = _context.Outlet.FirstOrDefault(a => a.OutletId == id);
            return View(outlet);
        }
    }
}