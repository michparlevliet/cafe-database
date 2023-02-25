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
using PassionProject.Models.ViewModels;
using System.Web.Script.Serialization;

namespace PassionProject.Controllers
{
    public class CoffeeDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/CoffeeData/ListCoffees
        [HttpGet]
        [ResponseType(typeof(CoffeeDto))]
        public IHttpActionResult ListCoffees()
        {
            List<coffee> coffees = db.Coffees.ToList();
            List<CoffeeDto> CoffeeDtos = new List<CoffeeDto>();

            coffees.ForEach(co => CoffeeDtos.Add(new CoffeeDto()
            { 
                CoffeeId = co.CoffeeId,
                CoffeeName = co.CoffeeName,
                CompanyName = co.CompanyName,
                RoastType = co.RoastType,
            
            }));

            return Ok(CoffeeDtos);
        }
        // GET: api/CoffeeData/ListCoffeesForCafe/5
        [HttpGet]
        [ResponseType(typeof(CoffeeDto))]
        public IHttpActionResult ListCoffeesForCafe(int id)
        {
            List<coffee> coffees = db.Coffees.Where(co=>co.Cafe.Any(ca=>ca.CafeId==id)).ToList();
            List<CoffeeDto> CoffeeDtos = new List<CoffeeDto>();

            coffees.ForEach(co => CoffeeDtos.Add(new CoffeeDto()
            {
                CoffeeId = co.CoffeeId,
                CoffeeName = co.CoffeeName,
                CompanyName = co.CompanyName,
                RoastType = co.RoastType,
            }));

            return Ok(CoffeeDtos);
        }

        [HttpPost]
        [Route("api/CoffeeData/AssociateCoffeeWithCafe/{coffeeid}/{cafeid}")]
        public IHttpActionResult AssociateCoffeeWithCafe(int coffeeid, int cafeid)
        {
            coffee SelectedCoffee = db.Coffees.Include(co => co.Cafe).Where(co=> co.CoffeeId==coffeeid).FirstOrDefault();
            cafe SelectedCafe = db.Cafes.Find(cafeid);

            if(SelectedCoffee ==null || SelectedCafe == null)
            {
                return NotFound();
            }

            SelectedCoffee.Cafe.Add(SelectedCafe);
            db.SaveChanges();

            return Ok();
        }

        [HttpPost]
        [Route("api/CoffeeData/UnassociateCoffeeWithCafe/{coffeeid}/{cafeid}")]
        public IHttpActionResult UnassociateCoffeeWithCafe(int coffeeid, int cafeid)
        {
            coffee SelectedCoffee = db.Coffees.Include(co => co.Cafe).Where(co => co.CoffeeId == coffeeid).FirstOrDefault();
            cafe SelectedCafe = db.Cafes.Find(cafeid);

            if (SelectedCoffee == null || SelectedCafe == null)
            {
                return NotFound();
            }

            SelectedCoffee.Cafe.Remove(SelectedCafe);
            db.SaveChanges();

            return Ok();
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
                RoastType = coffee.RoastType,
                //Cafe = coffee.Cafe
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