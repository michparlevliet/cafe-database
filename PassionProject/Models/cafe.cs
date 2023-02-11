using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// these allow the primary and foreign keys to be specifically indicated
// use [Key] above the property that is the primary key

namespace PassionProject.Models
{
    public class cafe
    {
        [Key]
        public int CafeId { get; set; }

        public string CafeName { get; set; }

        public string CafeLocation { get; set; }

        public string CafeAddress { get; set; }

        public int CafeSeating { get; set; }

        public bool CafePatio { get; set; }

        public string CafeMenu { get; set; }   
        // possible values:
        // "beverage only", "snacks/pastries", "meals"

        public bool CafeAccessibility { get; set; }

        // a cafe could have many coffees
        //public ICollection<coffee> Coffee { get; set; }

    }
    public class CafeDto
    {
        public int CafeId { get; set; }

        public string CafeName { get; set; }

        public string CafeLocation { get; set; }

        public string CafeAddress { get; set; }

        public int CafeSeating { get; set; }

        public bool CafePatio { get; set; }

        public string CafeMenu { get; set; }

        public bool CafeAccessibility { get; set; }
    }
}