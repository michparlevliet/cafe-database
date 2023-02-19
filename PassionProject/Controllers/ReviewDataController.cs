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
using System.Diagnostics;

namespace PassionProject.Controllers
{
    public class ReviewDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ReviewData/ListReviews
        [HttpGet]
        [ResponseType(typeof(ReviewDto))]
        public IHttpActionResult ListReviews()
        {
            List<review> reviews = db.Reviews.ToList();
            List<ReviewDto> ReviewDtos = new List<ReviewDto>();

            reviews.ForEach(r => ReviewDtos.Add(new ReviewDto()
            {
                ReviewId = r.ReviewId,
                CafeId = r.CafeId,
                ReviewerName = r.ReviewerName,
                ReviewRating = r.ReviewRating,
                ReviewComment = r.ReviewComment,
            }));

            return Ok(ReviewDtos);
        }

        [HttpGet]
        [ResponseType(typeof(ReviewDto))]
        public IHttpActionResult ListReviewsForCafe(int id)
        {
            List<review> reviews = db.Reviews.Where(r=>r.CafeId == id).ToList();
            List<ReviewDto> ReviewDtos = new List<ReviewDto>();

            reviews.ForEach(r => ReviewDtos.Add(new ReviewDto()
            {
                ReviewId = r.ReviewId,
                CafeId = r.CafeId,
                ReviewerName = r.ReviewerName,
                ReviewRating = r.ReviewRating,
                ReviewComment = r.ReviewComment
            }));

            return Ok(ReviewDtos);
        }

        // GET: api/ReviewData/FindReview/5
        [ResponseType(typeof(ReviewDto))]
        [HttpGet]
        public IHttpActionResult FindReview(int id)
        {
            review review = db.Reviews.Find(id);
            ReviewDto ReviewDto = new ReviewDto()
            {
                ReviewId = review.ReviewId,
                CafeId = review.CafeId,
                ReviewerName = review.ReviewerName,
                ReviewRating = review.ReviewRating,
                ReviewComment = review.ReviewComment,
            };
            if (review == null)
            {
                return NotFound();
            }

            return Ok(review);
        }

        // PUT: api/ReviewData/UpdateReview/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateReview(int id, review review)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != review.ReviewId)
            {
                return BadRequest();
            }

            db.Entry(review).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!reviewExists(id))
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

        // POST: api/ReviewData/AddReview
        [ResponseType(typeof(review))]
        public IHttpActionResult AddReview(review review)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Reviews.Add(review);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = review.ReviewId }, review);
        }

        // DELETE: api/ReviewData/DeleteReview/5
        [ResponseType(typeof(review))]
        [HttpPost]
        public IHttpActionResult DeleteReview(int id)
        {
            review review = db.Reviews.Find(id);
            if (review == null)
            {
                return NotFound();
            }

            db.Reviews.Remove(review);
            db.SaveChanges();

            return Ok(review);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool reviewExists(int id)
        {
            return db.Reviews.Count(e => e.ReviewId == id) > 0;
        }
    }
}