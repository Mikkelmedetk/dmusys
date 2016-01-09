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
using AdminDMU.Models;

namespace AdminDMU.Controllers
{
    public class LektionController : ApiController
    {
        private DMUSys db = new DMUSys();

        // GET: api/Lektion
        public IQueryable<Lektion> GetLektions()
        {
            return db.Lektions;
        }

        // GET: api/Lektion/5
        [ResponseType(typeof(Lektion))]
        public IHttpActionResult GetLektion(int id)
        {
            Lektion lektion = db.Lektions.Find(id);
            if (lektion == null)
            {
                return NotFound();
            }

            return Ok(lektion);
        }

        // PUT: api/Lektion/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLektion(int id, Lektion lektion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != lektion.Id)
            {
                return BadRequest();
            }

            db.Entry(lektion).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LektionExists(id))
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

        // POST: api/Lektion
        [ResponseType(typeof(Lektion))]
        public IHttpActionResult PostLektion(Lektion lektion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Lektions.Add(lektion);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = lektion.Id }, lektion);
        }

        // DELETE: api/Lektion/5
        [ResponseType(typeof(Lektion))]
        public IHttpActionResult DeleteLektion(int id)
        {
            Lektion lektion = db.Lektions.Find(id);
            if (lektion == null)
            {
                return NotFound();
            }

            db.Lektions.Remove(lektion);
            db.SaveChanges();

            return Ok(lektion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LektionExists(int id)
        {
            return db.Lektions.Count(e => e.Id == id) > 0;
        }
    }
}