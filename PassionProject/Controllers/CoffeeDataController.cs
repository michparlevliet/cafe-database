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
using PassionProject.Models;

namespace PassionProject.Controllers
{
    public class CoffeeDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/CoffeeData/ListCoffees
        [HttpGet]
        public IEnumerable<CoffeeDto> ListCoffees()
        {
            List<coffee> coffees = db.Coffees.ToList();
            List<CoffeeDto> CoffeeDtos = new List<CoffeeDto>();

            coffees.ForEach(c => CoffeeDtos.Add(new CoffeeDto()
            { 
                CoffeeId = c.CoffeeId,
                CoffeeName = c.CoffeeName,
                CompanyName = c.CompanyName,
                RoastType = c.RoastType
            }));

            return CoffeeDtos;
        }

        // GET: api/CoffeeData/FindCoffee/5
        [ResponseType(typeof(coffee))]
        [HttpGet]
        public IHttpActionResult FindCoffee(int id)
        {
            coffee coffee = db.Coffees.Find(id);
            CoffeeDto CoffeeDto = new CoffeeDto()
            {
                CoffeeId = coffee.CoffeeId,
                CoffeeName = coffee.CoffeeName,
                CompanyName = coffee.CompanyName,
                RoastType = coffee.RoastType
            };
            if (coffee == null)
            {
                return NotFound();
            }

            return Ok(CoffeeDto);
        }

        // POST: api/CoffeeData/UpdateCoffee/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateCoffee(int id, coffee coffee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != coffee.CoffeeId)
            {
                return BadRequest();
            }

            db.Entry(coffee).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!coffeeExists(id))
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

        // POST: api/CoffeeData/AddCoffee
        [ResponseType(typeof(coffee))]
        [HttpPost]
        public IHttpActionResult AddCoffee(coffee coffee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Coffees.Add(coffee);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = coffee.CoffeeId }, coffee);
        }

        // POST: api/CoffeeData/DeleteCoffee/5
        [ResponseType(typeof(coffee))]
        [HttpPost]
        public IHttpActionResult DeleteCoffee(int id)
        {
            coffee coffee = db.Coffees.Find(id);
            if (coffee == null)
            {
                return NotFound();
            }

            db.Coffees.Remove(coffee);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool coffeeExists(int id)
        {
            return db.Coffees.Count(e => e.CoffeeId == id) > 0;
        }
    }
}