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
using WebApiChrysallis.Models;

namespace WebApiChrysallis.Controllers
{
    public class esdevenimentsController : ApiController
    {
        private chrysallisEntities db = new chrysallisEntities();

        // GET: api/esdeveniments
        public List<esdeveniments> Getesdeveniments()
        {
            db.Configuration.LazyLoadingEnabled = false;

            List<esdeveniments> _esdeveniments = (
                from e in db.esdeveniments.Include("comunitats")
                select e
                ).ToList();

            return _esdeveniments;
        }

        // GET: api/esdeveniments/5
        [ResponseType(typeof(esdeveniments))]
        public IHttpActionResult Getesdeveniments(int id)
        {
            IHttpActionResult resultado;

            db.Configuration.LazyLoadingEnabled = false;

            esdeveniments _esdeveniment =
                (
                    from e in db.esdeveniments.Include("documents")
                    where e.id == id
                    select e
                ).FirstOrDefault();

            if (_esdeveniment == null)
            {
                resultado = NotFound();
            }
            else
            {
                resultado = Ok(_esdeveniment);
            }

            return resultado;
        }

        // PUT: api/esdeveniments/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putesdeveniments(int id, esdeveniments esdeveniments)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != esdeveniments.id)
            {
                return BadRequest();
            }

            db.Entry(esdeveniments).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!esdevenimentsExists(id))
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

        // POST: api/esdeveniments
        [ResponseType(typeof(esdeveniments))]
        public IHttpActionResult Postesdeveniments(esdeveniments esdeveniments)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.esdeveniments.Add(esdeveniments);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = esdeveniments.id }, esdeveniments);
        }

        // DELETE: api/esdeveniments/5
        [ResponseType(typeof(esdeveniments))]
        public IHttpActionResult Deleteesdeveniments(int id)
        {
            esdeveniments esdeveniments = db.esdeveniments.Find(id);
            if (esdeveniments == null)
            {
                return NotFound();
            }

            db.esdeveniments.Remove(esdeveniments);
            db.SaveChanges();

            return Ok(esdeveniments);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool esdevenimentsExists(int id)
        {
            return db.esdeveniments.Count(e => e.id == id) > 0;
        }
    }
}