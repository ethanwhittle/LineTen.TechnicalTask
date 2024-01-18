using AutoMapper;
using LineTen.TechnicalTask.Domain.Models;
using LineTen.TechnicalTask.Service.Domain.Models;

namespace LineTen.TechnicalTask.Service.Domain.Mappings
{
    public class RequestModelProfile : Profile
    {
        public RequestModelProfile()
        {
            CreateMap<AddCustomerRequest, Customer>()
                .ForMember(d => d.Id, opt => opt.Ignore());

            CreateMap<UpdateCustomerRequest, Customer>();

            CreateMap<AddProductRequest, Product>()
                .ForMember(d => d.Id, opt => opt.Ignore());

            CreateMap<UpdateProductRequest, Product>();

            CreateMap<AddOrderRequest, Order>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.CreatedDate, opt => opt.Ignore())
                .ForMember(d => d.UpdatedDate, opt => opt.Ignore());

            CreateMap<UpdateOrderRequest, Order>()
                .ForMember(d => d.CreatedDate, opt => opt.Ignore())
                .ForMember(d => d.UpdatedDate, opt => opt.Ignore());
        }
    }
}