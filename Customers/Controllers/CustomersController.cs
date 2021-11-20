using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Customers.Contexts;
using Customers.Models;

namespace Customers.Controllers
{
    public class CustomersController : Controller
    {
        dbContext _context;
        List<Customer> customers;

        public CustomersController()
        {
            _context = new dbContext();
            customers = _context.Customers.Include(c => c.Addresses).ToList();
        }

        // GET: Customers
        public ActionResult Index()
        {
            return View("List", customers);
        }

        public ActionResult Add()
        {
            var customer = new Customer();
            return View("CustomerForm", customer);
        }

        [HttpPost]
        public void UpdateAddresses(List<string> addresses) {
            Session["addresses"] = addresses;
        }

        public ActionResult Update(int id)
        {
            var customer = _context.Customers
                .Include(c => c.Addresses)
                .SingleOrDefault(c => c.CustomerID == id);

            return View("CustomerForm", customer);
        }

        [HttpPost]
        public void deleteOldAddresses (List<int> addressesIDs) {
            Session["oldAddressesIDs"] = addressesIDs;
        }

        [HttpPost]
        public ActionResult Save(Customer customer, string writtenAddress)
        {
            if(customer.CustomerID == 0)
            {
                _context.Customers.Add(customer);
                _context.SaveChanges();

                //System.Diagnostics.Debug.WriteLine(customer.CustomerID);
                //System.Diagnostics.Debug.WriteLine(writtenAddress);
            }
            else
            {
                var customerInDb = _context.Customers
                    .Single(c => c.CustomerID == customer.CustomerID);

                customerInDb.CustomerFirstName = customer.CustomerFirstName;
                customerInDb.CustomerLastName = customer.CustomerLastName;
                customerInDb.CustomerDOB = customer.CustomerDOB;
                customerInDb.CustomerEmail = customer.CustomerEmail;
                customerInDb.Gender = customer.Gender;

                List<int> deletedOldAddressesIDs = (List<int>)Session["oldAddressesIDs"];
                if (deletedOldAddressesIDs != null)
                {
                    for(var i=0; i<deletedOldAddressesIDs.Count; i++)
                    {
                        Address deletedAddress = _context.Addresses.Find(deletedOldAddressesIDs[i]);
                        _context.Addresses.Remove(deletedAddress);
                    }
                }
            }

            List<string> addresses = (List<string>)Session["addresses"];

            if (addresses != null)
            {
                for (var i = 0; i < addresses.Count; i++)
                {
                    Address address = new Address
                    {
                        Addrxss = addresses[i],
                        CustomerID = customer.CustomerID,
                    };
                    _context.Addresses.Add(address);
                }
            }
            if (writtenAddress != null && writtenAddress.Trim() != "")
            {
                Address address = new Address
                {
                    Addrxss = writtenAddress,
                    CustomerID = customer.CustomerID,
                };
                _context.Addresses.Add(address);
            }

            Session.RemoveAll();
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public void DeleteCustomer(int id)
        {
            var customer = _context.Customers.Single(c => c.CustomerID == id);
            _context.Customers.Remove(customer);
            _context.SaveChanges();
        }
    }
}