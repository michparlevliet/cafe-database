using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassionProject.Models.ViewModels
{
    public class DetailsCafe
    {
        public CafeDto SelectedCafe { get; set; }
        public IEnumerable<CoffeeDto> CoffeesOffered { get; set; }

        public IEnumerable<CoffeeDto> Coffees { get; set;}

        public IEnumerable<ReviewDto> CafeReviews { get; set; }
    }
}