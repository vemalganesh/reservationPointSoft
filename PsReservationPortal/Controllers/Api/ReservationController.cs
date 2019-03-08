using PsReservationPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace PsReservationPortal.Controllers.Api
{
    public class ReservationController : ApiController
    {
        // GET: api/Reservation
        List<OutletModel> ListOfOutlets = new List<OutletModel>();
        

        //DB call
        private ApplicationDbContext _context;

        //public ReservationController()
        //{
        //    reservation.Add(new OutletModel { ReservationNum = 1, DinerName = "Anonymos" });
        //    reservation.Add(new OutletModel { ReservationNum = 3, DinerName = "bye" });
        //}

        public ReservationController()
        {
            _context = new ApplicationDbContext();
        }

        //public int generateApikey(CompanyId)
        //{
        //    Guid id = Guid.NewGuid();


        //    return id;
        //}

        //Getting List of Outlets using Company ID
        public List<OutletModel> Get()
        {
            ListOfOutlets = _context.Outlet.ToList();
            //ListOfOutlets = ListOfOutlets.Company_Id()
            return ListOfOutlets;
        }

        //Get api/values 
        public List<OutletModel> Get(long id)
        {
            ListOfOutlets = _context.Outlet.Where(x => x.CompanyId == id).ToList();

            return ListOfOutlets;
        }

        // POST: api/Reservation
        public void Post([FromBody] ReservationOrderModel Reservation)
        {
            ReservationOrderModel ReservationInfo = new ReservationOrderModel();

            ReservationInfo.DinerName = Reservation.DinerName;
            ReservationInfo.ReservationNum = Reservation.ReservationNum;
            ReservationInfo.DateTimeCreated = DateTime.Now;
            ReservationInfo.DateTimeUpdated = DateTime.Now;
            ReservationInfo.ReserveDateTime = DateTime.Now;

            _context.ReservationOrder.Add(ReservationInfo);
            _context.SaveChanges();
        }

        // PUT: api/Reservation/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Reservation/5
        public void Delete(int id)
        {
        }
    }
}
