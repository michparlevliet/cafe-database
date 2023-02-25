using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using PassionProject.Models;
using PassionProject.Models.ViewModels;
using System.Web.Script.Serialization;

namespace PassionProject.Controllers
{
    public class CoffeeController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();


        static CoffeeController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44312/api/");

        }

        // GET: /Coffee/List
        public ActionResult List()
        {
            // objective: communicate with coffeedata api to retrieve a list of coffees
            string url = "coffeedata/listcoffees";
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<CoffeeDto> Coffees = response.Content.ReadAsAsync<IEnumerable<CoffeeDto>>().Result;

            //Debug.WriteLine("Number of coffees received : ");
            //Debug.WriteLine(Coffees.Count());

            return View(Coffees);
        }

        // GET: Coffee/Details/5
        public ActionResult Details(int id)
        {

            DetailsCoffee ViewModel = new DetailsCoffee();
            // objective: retrieve one coffee
            string url = "coffeedata/findcoffee/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            CoffeeDto SelectedCoffee = response.Content.ReadAsAsync<CoffeeDto>().Result;

            //Debug.WriteLine("Coffee received : ");
            //Debug.WriteLine(SelectedCoffee.CoffeeName);
            ViewModel.SelectedCoffee = SelectedCoffee;

            // show all cafes asociated with coffee
            url = "cafedata/listcafesforcoffee/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<CafeDto> RelatedCafes = response.Content.ReadAsAsync<IEnumerable<CafeDto>>().Result;

            ViewModel.RelatedCafes = RelatedCafes;

            // show all cafes unrelated
            url = "cafedata/listcafesnotrelated/"+id;
            response = client.GetAsync(url).Result;
            IEnumerable<CafeDto> CafeOptions = response.Content.ReadAsAsync<IEnumerable<CafeDto>>().Result;

            ViewModel.CafeOptions = CafeOptions;


            return View(ViewModel);
        }

        // POST: Coffee/Associate/{coffeeid}
        [HttpPost]
        public ActionResult Associate(int id, int CafeId)
        {
            string url = "coffeedata/associatecoffeewithcafe/" + id + "/" + CafeId;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            return RedirectToAction("Details/" + id);

        }
        // GET: Coffee/UnAssociate/{id}?CafeId={cafeId}
        [HttpGet]
        public ActionResult UnAssociate(int id, int CafeId)
        {
            string url = "coffeedata/unassociatecoffeewithcafe/" + id + "/" + CafeId;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            return RedirectToAction("Details/" + id);
        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: Coffee/New
        public ActionResult New()
        {
            string url = "cafedata/listcafes";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<CafeDto> CafeOptions = response.Content.ReadAsAsync<IEnumerable<CafeDto>>().Result;

            return View(CafeOptions);
        }

        // POST: Coffee/Create
        [HttpPost]
        public ActionResult Create(coffee coffee)
        {
            Debug.WriteLine("The inputted coffee name is:");
            Debug.WriteLine(coffee.CoffeeName);
            //objective: add new coffee into system using api
            string url = "coffeedata/addcoffee";

            string jsonpayload = jss.Serialize(coffee);

            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }

           
        }

        // GET: Coffee/Edit/5
        public ActionResult Edit(int id)
        {
            UpdateCoffee ViewModel = new UpdateCoffee();

            string url = "coffeedata/findcoffee/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            CoffeeDto SelectedCoffee = response.Content.ReadAsAsync<CoffeeDto>().Result;
            ViewModel.SelectedCoffee = SelectedCoffee;

            // include all coffees to choose from when updating this cafe

            url = "cafedata/findcafe/";
            response = client.GetAsync(url).Result;
            IEnumerable<CafeDto> CafeOptions = response.Content.ReadAsAsync<IEnumerable<CafeDto>>().Result;
            ViewModel.CafeOptions = CafeOptions;

            return View(ViewModel);
        }

        // POST: Coffee/Edit/5
        [HttpPost]
        public ActionResult Update(int id, coffee coffee)
        {
            string url = "coffeedata/updatecoffee/" + id;
            string jsonpayload = jss.Serialize(coffee);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Coffee/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "coffeedata/findcoffee/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            CoffeeDto SelectedCoffee = response.Content.ReadAsAsync<CoffeeDto>().Result;
            return View(SelectedCoffee);
        }

        // POST: Coffee/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "coffeedata/deletecoffee/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
    }
}
