using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PsReservationPortal.Models;

namespace PsReservationPortal.Controllers.Api
{
    public class CompanyAPIController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/CompanyAPI
        public IQueryable<CompanyModel> GetCompany()
        {
            return db.Company;
        }

        // GET: api/CompanyAPI/5
        [ResponseType(typeof(CompanyModel))]
        public IHttpActionResult GetCompanyModel(long id)
        {
            CompanyModel companyModel = db.Company.Find(id);
            if (companyModel == null)
            {
                return NotFound();
            }

            return Ok(companyModel);
        }

        // PUT: api/CompanyAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCompanyModel(long id, CompanyModel companyModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != companyModel.Id)
            {
                return BadRequest();
            }

            db.Entry(companyModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/CompanyAPI
        [ResponseType(typeof(CompanyModel))]
        public IHttpActionResult PostCompanyModel(CompanyModel companyModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Company.Add(companyModel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = companyModel.Id }, companyModel);
        }

        // DELETE: api/CompanyAPI/5
        [ResponseType(typeof(CompanyModel))]
        public IHttpActionResult DeleteCompanyModel(long id)
        {
            CompanyModel companyModel = db.Company.Find(id);
            if (companyModel == null)
            {
                return NotFound();
            }

            db.Company.Remove(companyModel);
            db.SaveChanges();

            return Ok(companyModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CompanyModelExists(long id)
        {
            return db.Company.Count(e => e.Id == id) > 0;
        }
    }
}