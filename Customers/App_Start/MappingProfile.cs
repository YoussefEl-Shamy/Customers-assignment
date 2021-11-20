using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Customers.DTOs;
using Customers.Models;

namespace Customers.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //.ForMember(c => c.id, opt => opt.Ignore())
            Mapper.CreateMap<Customer, CustomerDto>();
            Mapper.CreateMap<CustomerDto, Customer>().ForMember(c => c.CustomerID, opt => opt.Ignore());
        }
    }
}