using AutoMapper;
using ERPServer.Application.Features.Customers.CreateCustomer;
using ERPServer.Application.Features.Customers.UpdateCustomer;
using ERPServer.Domain.Entities;

namespace ERPServer.Application.Mapping;
public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateCustomerCommand, Customer>();
        CreateMap<UpdateCustomerCommand, Customer>();
    }
}
