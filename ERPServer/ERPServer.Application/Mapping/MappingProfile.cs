using AutoMapper;
using ERPServer.Application.Features.Customers.CreateCustomer;
using ERPServer.Application.Features.Customers.UpdateCustomer;
using ERPServer.Application.Features.Depots.CreateDepot;
using ERPServer.Application.Features.Depots.UpdateDepot;
using ERPServer.Application.Features.Products.CreateProduct;
using ERPServer.Application.Features.Products.UpdateProduct;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Enums;

namespace ERPServer.Application.Mapping;
public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateCustomerCommand, Customer>();
        CreateMap<UpdateCustomerCommand, Customer>();

        CreateMap<CreateDepotCommand, Depot>();
        CreateMap<UpdateDepotCommand, Depot>();

        CreateMap<CreateProductCommand, Product>()
            .ForMember(member => member.Type,
                options => 
                options.MapFrom(p => ProductTypeEnum.FromValue(p.TypeValue)));

        CreateMap<UpdateProductCommand, Product>()
           .ForMember(member => member.Type,
               options =>
               options.MapFrom(p => ProductTypeEnum.FromValue(p.TypeValue)));
    }
}
