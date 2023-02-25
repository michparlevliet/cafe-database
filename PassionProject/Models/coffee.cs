using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassionProject.Models
{
    public class coffee
    {
        [Key]

        public int CoffeeId { get; set; }

        public string CoffeeName { get; set; }

        public string CompanyName { get; set; }

        public string RoastType { get; set; }

        // many-many relationship:
        // a cafe may offer many differeny coffees and a coffee may be offered at many different cafes

        // implicit m-m relationship
        // creates a simple bridging table with cafeid and coffeeid
        // drawback: limited expression on bridging table, no additional information can be displayed here
        // benefits: easy to set up
        public ICollection<cafe> Cafe { get; set; }

    }

    public class CoffeeDto
    {
        public int CoffeeId { get; set; }

        public string CoffeeName { get; set; }

        public string CompanyName { get; set; }

        public string RoastType { get; set; }

      
    }
}