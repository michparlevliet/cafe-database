using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassionProject.Models
{
    public class review
    {
        [Key]

        public int ReviewId { get; set; }

        // each review belongs to one cafe, but one cafe can have many reviews
        [ForeignKey("Cafe")]
        public int CafeId { get; set; }
        public virtual cafe Cafe { get; set; }

        public string ReviewerName { get; set; }

        public int ReviewRating { get; set; }

        public string ReviewComment { get; set; }
    }

    public class ReviewDto
    {
        public int ReviewId { get; set; }

        public int CafeId { get; set; }
        public string CafeName { get; set; }
        public string ReviewerName { get; set; }

        public int ReviewRating { get; set; }

        public string ReviewComment { get; set; }

    }
}