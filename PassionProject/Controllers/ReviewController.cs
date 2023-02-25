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
    public class ReviewController : Controller
    {

        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static ReviewController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44312/api/");

        }

        // GET: Review/List
        public ActionResult List()
        {
            string url = "reviewdata/listreviews";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<ReviewDto> Reviews = response.Content.ReadAsAsync<IEnumerable<ReviewDto>>().Result;

            return View(Reviews);
        }

        // GET: Review/Details/5
        public ActionResult Details(int id)

        {
            DetailsReview ViewModel = new DetailsReview();
            string url = "reviewdata/findreview/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            ReviewDto SelectedReview = response.Content.ReadAsAsync<ReviewDto>().Result;

            ViewModel.SelectedReview = SelectedReview;

            return View(ViewModel);
        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: Review/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Review/Create
        [HttpPost]
        public ActionResult Create(review review)
        {
            string url = "reviewdata/addreview";

            string jsonpayload = jss.Serialize(review);

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

        // GET: Review/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Review/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Review/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Review/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
