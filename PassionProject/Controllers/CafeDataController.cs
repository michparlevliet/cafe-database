using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PassionProject.Models;

namespace PassionProject.Controllers
{
    public class CafeDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/CafeData/ListCafes
        [HttpGet]
        public IEnumerable<CafeDto> ListCafes()
        {
            List<cafe> cafes = db.Cafes.ToList();
            List<CafeDto> CafeDtos = new List<CafeDto>();

            cafes.ForEach(c => CafeDtos.Add(new CafeDto()
            {
                CafeId = c.CafeId,
                CafeName = c.CafeName,
                CafeLocation = c.CafeLocation,
                CafeAddress = c.CafeAddress,
                CafeSeating = c.CafeSeating,
                CafePatio = c.CafePatio,
                CafeMenu = c.CafeMenu,
                CafeAccessibility = c.CafeAccessibility
            }));

            return CafeDtos;
        }

        // GET: api/CafeData/FindCafe/2
        [ResponseType(typeof(cafe))]
        [HttpGet]
        public IHttpActionResult FindCafe(int id)
        {
            cafe cafe = db.Cafes.Find(id);
            CafeDto CafeDto = new CafeDto()
            {
                CafeId = cafe.CafeId,
                CafeName = cafe.CafeName,
                CafeLocation = cafe.CafeLocation,
                CafeAddress = cafe.CafeAddress,
                CafeSeating = cafe.CafeSeating,
                CafePatio = cafe.CafePatio,
                CafeMenu = cafe.CafeMenu,
                CafeAccessibility = cafe.CafeAccessibility
            };
            if (cafe == null)
            {
                return NotFound();
            }

            return Ok(CafeDto);
        }

        // PUT: api/CafeData/UpdateCafe/2
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateCafe(int id, cafe cafe)
        {
            Debug.WriteLine("I have reached the update cafe method.");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cafe.CafeId)
            {
                return BadRequest();
            }

            db.Entry(cafe).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!cafeExists(id))
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

        // POST: api/CafeData/AddCoffee
        [ResponseType(typeof(cafe))]
        public IHttpActionResult AddCafe(cafe cafe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cafes.Add(cafe);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cafe.CafeId }, cafe);
        }

        // DELETE: api/CafeData/DeleteCafe/2
        [ResponseType(typeof(cafe))]
        [HttpPost]
        public IHttpActionResult DeleteCafe(int id)
        {
            cafe cafe = db.Cafes.Find(id);
            if (cafe == null)
            {
                return NotFound();
            }

            db.Cafes.Remove(cafe);
            db.SaveChanges();

            return Ok(cafe);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool cafeExists(int id)
        {
            return db.Cafes.Count(e => e.CafeId == id) > 0;
        }
    }
}