using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassionProject.Models.ViewModels
{
    public class UpdateCoffee
    {
        public CoffeeDto SelectedCoffee { get; set; }
        public IEnumerable<CafeDto> CafeOptions { get; set; }
    }
}