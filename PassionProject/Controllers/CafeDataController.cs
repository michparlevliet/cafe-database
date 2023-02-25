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

            cafes.ForEach(ca => CafeDtos.Add(new CafeDto()
            {
                CafeId = ca.CafeId,
                CafeName = ca.CafeName,
                CafeLocation = ca.CafeLocation,
                CafeAddress = ca.CafeAddress,
                CafeSeating = ca.CafeSeating,
                CafePatio = ca.CafePatio,
                CafeMenu = ca.CafeMenu,
                CafeAccessibility = ca.CafeAccessibility,
                //Coffee = ca.Coffee
            }));

            return CafeDtos;
        }
        // GET: api/CafeData/ListCafesForCoffee/5
        [HttpGet]
        [ResponseType(typeof(CafeDto))]
        public IHttpActionResult ListCafesForCoffee(int id)
        {
            List<cafe> cafes = db.Cafes.Where(
                ca => ca.Coffee.Any(
                    co => co.CoffeeId == id)
                ).ToList();
            List<CafeDto> CafeDtos = new List<CafeDto>();

            cafes.ForEach(ca => CafeDtos.Add(new CafeDto()
            {
                CafeId = ca.CafeId,
                CafeName = ca.CafeName,
                CafeLocation = ca.CafeLocation,
                CafeAddress = ca.CafeAddress,
                CafeSeating = ca.CafeSeating,
                CafePatio = ca.CafePatio,
                CafeMenu = ca.CafeMenu,
                CafeAccessibility = ca.CafeAccessibility,
            }));

            return Ok(CafeDtos);
        }

        [HttpGet]
        [ResponseType(typeof(CafeDto))]
        public IHttpActionResult ListCafesNotRelated(int id)
        {
            List<cafe> Cafes = db.Cafes.Where(ca => !ca.Coffee.Any(co => co.CoffeeId == id)).ToList();
            List<CafeDto> CafeDtos = new List<CafeDto>();

            Cafes.ForEach(ca => CafeDtos.Add(new CafeDto()
            {
                CafeId = ca.CafeId,
                CafeName = ca.CafeName,
                CafeLocation = ca.CafeLocation,
                CafeAddress = ca.CafeAddress,
                CafeSeating = ca.CafeSeating,
                CafePatio = ca.CafePatio,
                CafeMenu = ca.CafeMenu,
                CafeAccessibility = ca.CafeAccessibility,
            }));

            return Ok(CafeDtos);
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
                CafeAccessibility = cafe.CafeAccessibility,
                //Coffee = cafe.Coffee
            };
            if (cafe == null)
            {
                return NotFound();
            }

            return Ok(CafeDto);
        }

        // POST: api/CafeData/UpdateCafe/2
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

        // POST: api/CafeData/AddCafe
        [ResponseType(typeof(cafe))]
        [HttpPost]
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