using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Customers.Models;

namespace Customers.DTOs
{
    public class CustomerDto
    {
        [Key]
        public int CustomerID { set; get; }

        public string CustomerFirstName { get; set; }

        public string CustomerLastName { get; set; }

        public string Gender { get; set; }

        public DateTime CustomerDOB { get; set; }

        public string CustomerEmail { get; set; }

        public List<Address> Addresses { get; set; }
    }
}