using AutoMapper;
using LineTen.TechnicalTask.Domain.Models;
using LineTen.TechnicalTask.Service.Domain.Models;

namespace LineTen.TechnicalTask.Service.Domain.Mappings
{
    public class ResponseModelProfile : Profile
    {
        public ResponseModelProfile()
        {
            CreateMap<Customer, CustomerResponse>();
            CreateMap<Product, ProductResponse>();
            CreateMap<Order, OrderResponse>();
        }
    }
}