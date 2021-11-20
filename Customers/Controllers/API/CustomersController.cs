using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Data.Entity;
using System.Web.Http;
using Customers.Contexts;
using Customers.DTOs;
using Customers.Models;
using AutoMapper;

namespace Customers.Controllers.API
{
    public class CustomersController : ApiController
    {
        private dbContext _context;

        public CustomersController()
        {
            _context = new dbContext();
        }

        //GET /api/customers
        [HttpGet]
        public IHttpActionResult GetCustomers()
        {
            var customersDTOs = _context.Customers
                .Include(c => c.Addresses)
                .ToList()
                .Select(Mapper.Map<Customer, CustomerDto>);

            return Ok(customersDTOs);
        }

        //GET /api/customers/CustomerID
        [HttpGet]
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.Include(c => c.Addresses).SingleOrDefault(c => c.CustomerID == id);

            if (customer == null)
                return NotFound();

            return Ok(Mapper.Map<Customer, CustomerDto>(customer)) ;
        }

        //POST /api/customers
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {

            if (!ModelState.IsValid)
                return BadRequest();

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.CustomerID = customer.CustomerID;

            return Created(new System.Uri(Request.RequestUri + "/" + customer.CustomerID), customerDto);
        }

        //PUT /api/customers/CustomerID
        [HttpPut]
        public void UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customerInDb = _context.Customers.SingleOrDefault(c => c.CustomerID == id);

            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(customerDto, customerInDb);

            _context.SaveChanges();
        }

        //DELETE /api/customers/CustomerID
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.CustomerID == id);

            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();
        }
    }
}




