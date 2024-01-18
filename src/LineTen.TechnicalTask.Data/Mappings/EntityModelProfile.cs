using AutoMapper;
using LineTen.TechnicalTask.Data.Entities.Sql;
using LineTen.TechnicalTask.Domain.Models;

namespace LineTen.TechnicalTask.Data.Mappings
{
    public class EntityModelProfile : Profile
    {
        public EntityModelProfile()
        {
            CreateMap<CustomerEntity, Customer>().ReverseMap();
            CreateMap<ProductEntity, Product>().ReverseMap();
            CreateMap<OrderEntity, Order>().ReverseMap();
        }
    }
}