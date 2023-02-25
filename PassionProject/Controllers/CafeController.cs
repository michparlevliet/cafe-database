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

    public class CafeController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static CafeController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44312/api/");

        }

        // GET: Cafe/List
        public ActionResult List()
        {
            string url = "cafedata/listcafes";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<CafeDto> Cafes = response.Content.ReadAsAsync<IEnumerable<CafeDto>>().Result;

            return View(Cafes);

        }

        // GET: Cafe/Details/5
        public ActionResult Details(int id)
        {
            DetailsCafe ViewModel = new DetailsCafe();

            string url = "cafedata/findcafe/"+id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            CafeDto SelectedCafe = response.Content.ReadAsAsync<CafeDto>().Result;

            ViewModel.SelectedCafe = SelectedCafe;

            //show associated coffees with this cafe
            url = "coffeedata/listcoffeesforcafe/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<CoffeeDto> CoffeesOffered = response.Content.ReadAsAsync<IEnumerable<CoffeeDto>>().Result;  

            ViewModel.CoffeesOffered = CoffeesOffered;

            //show associated reviews with this cafe
            url = "reviewdata/listreviewsforcafe/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<ReviewDto> CafeReviews = response.Content.ReadAsAsync<IEnumerable<ReviewDto>>().Result;

            ViewModel.CafeReviews = CafeReviews;

            return View(ViewModel);
        }  

        public ActionResult Error()
        {
            return View();
        }

        // GET: Cafe/New
        public ActionResult New()
        {
            //information about all coffees in the system
            // GET api/coffeedata/listcoffees

            string url = "coffeedata/listcoffees";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<CoffeeDto> CoffeeOptions = response.Content.ReadAsAsync<IEnumerable<CoffeeDto>>().Result;

            return View(CoffeeOptions);
        }

        // POST: Cafe/Create
        [HttpPost]
        public ActionResult Create(cafe cafe)
        {
            //objective: add new cafe into system using api
            string url = "cafedata/addcafe";

            string jsonpayload = jss.Serialize(cafe);

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

        // GET: Cafe/Edit/5
        public ActionResult Edit(int id)
        {
            UpdateCafe ViewModel = new UpdateCafe();

            string url = "cafedata/findcafe/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            CafeDto SelectedCafe = response.Content.ReadAsAsync<CafeDto>().Result;
            ViewModel.SelectedCafe = SelectedCafe;

            // include all coffees to choose from when updating this cafe

            //url = "coffeedata/findcoffee/";
            //response = client.GetAsync(url).Result;
            //IEnumerable<CoffeeDto> CoffeeOptions = response.Content.ReadAsAsync<IEnumerable<CoffeeDto>>().Result;
            //ViewModel.CoffeeOptions = CoffeeOptions;

            return View(ViewModel);
        }

        // POST: Cafe/Update/5
        [HttpPost]
        public ActionResult Update(int id, cafe cafe)
        {
            string url = "cafedata/updatecafe/" + id;
            string jsonpayload = jss.Serialize(cafe);
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

        // GET: Cafe/DeleteConfirm/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "cafedata/findcafe/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            CafeDto SelectedCafe = response.Content.ReadAsAsync<CafeDto>().Result;
            return View(SelectedCafe);
        }

        // POST: Cafe/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "cafedata/deletecafe/" + id;
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
