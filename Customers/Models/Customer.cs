using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Customers.Models
{
    public class Customer
    {
        [Key]
        public int CustomerID { set; get; }

        [Display(Name = "First Name")]
        public string CustomerFirstName { get; set; }

        [Display(Name = "Last Name")]
        public string CustomerLastName { get; set; }

        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Display(Name = "Date Of Birth")]
        public DateTime CustomerDOB { get; set; }

        [Display(Name = "Email")]
        public string CustomerEmail { get; set; }

        public List<Address> Addresses { get; set; }
    }
}