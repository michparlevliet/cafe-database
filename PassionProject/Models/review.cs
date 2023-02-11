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
        [ForeignKey("CafeId")]
        public virtual cafe cafe { get; set; }
        public int CafeId { get; set; }

        public string ReviewerName { get; set; }

        public int ReviewRating { get; set; }

        public string ReviewComment { get; set; }
    }
}