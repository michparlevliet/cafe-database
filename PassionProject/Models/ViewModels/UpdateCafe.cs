using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassionProject.Models.ViewModels
{
    public class UpdateCafe
    {
        public CafeDto SelectedCafe { get; set; }
        public IEnumerable<CoffeeDto> CoffeeOptions { get; set; }
    }
}