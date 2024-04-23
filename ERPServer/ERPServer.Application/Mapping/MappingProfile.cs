using AutoMapper;
using ERPServer.Application.Features.Customers.CreateCustomer;
using ERPServer.Application.Features.Customers.UpdateCustomer;
using ERPServer.Application.Features.Depots.CreateDepot;
using ERPServer.Application.Features.Depots.UpdateDepot;
using ERPServer.Application.Features.Invoices.CreateInvoice;
using ERPServer.Application.Features.Invoices.UpdateInvoice;
using ERPServer.Application.Features.Orders.CreateOrder;
using ERPServer.Application.Features.Orders.UpdateOrder;
using ERPServer.Application.Features.Products.CreateProduct;
using ERPServer.Application.Features.Products.UpdateProduct;
using ERPServer.Application.Features.RecipeDetails.CreateRecipeDetail;
using ERPServer.Application.Features.RecipeDetails.UpdateRecipeDetail;
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

        CreateMap<CreateRecipeDetailCommand, RecipeDetail>();
        CreateMap<UpdateRecipeDetailCommand, RecipeDetail>();

        CreateMap<CreateOrderCommand, Order>()
            .ForMember(member => member.Details,
            options =>
            options.MapFrom(p => p.Details.Select(s => new OrderDetail
            {
                Price = s.Price,
                ProductId = s.ProductId,                
                Quantity = s.Quantity
            }).ToList()));

        CreateMap<UpdateOrderCommand, Order>()
            .ForMember(member =>
            member.Details,
            options => options.Ignore());

        CreateMap<CreateInvoiceCommand, Invoice>()
            .ForMember(member => member.Type, options => 
            options.MapFrom(p=> InvoiceTypeEnum.FromValue(p.TypeValue)))
            .ForMember(member => member.Details,
            options =>
            options.MapFrom(p => p.Details.Select(s => new InvoiceDetail
            {
                Price = s.Price,
                ProductId = s.ProductId,
                DepotId = s.DepotId,
                Quantity = s.Quantity
            }).ToList()));

        CreateMap<UpdateInvoiceCommand, Invoice>()
            .ForMember(member =>
            member.Details,
            options => options.Ignore());
    }
}
