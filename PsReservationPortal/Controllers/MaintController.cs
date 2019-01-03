using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PsReservationPortal.Controllers
{
    [Authorize(Roles ="SuperAdmin,PointsoftSupport")]

    public class MaintController : Controller
    {
        // GET: Maint
        public ActionResult Index()
        {
            return View();
        }
    }
}