using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassionProject.Models
{
    public class cafexcoffee
    {
        [Key]

        public int CafeCoffeeId { get; set; }

        [ForeignKey("CafeId")]
        public virtual cafe cafe { get; set; }
        public int CafeId { get; set; }

        [ForeignKey("CoffeeId")]
        public virtual coffee coffee { get; set; }
        public int CoffeeId { get; set; }

        // here is an explicit M-M relationship:
        // more data can be added here where information can be linked directly to the association of the two
        // this is only necessary really if you want to have more columns than just the two 
    }
}