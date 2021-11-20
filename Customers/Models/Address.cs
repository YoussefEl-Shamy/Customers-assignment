using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Customers.Models
{
    public class Address
    {
        [Key]
        public int AddressID { get; set; }

        [Display(Name = "Address")]
        public string Addrxss { get; set; }

        public int CustomerID { get; set; }

    }
}