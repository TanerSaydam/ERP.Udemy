using AutoMapper;
using ERPServer.Application.Features.Customers.CreateCustomer;
using ERPServer.Domain.Entities;

namespace ERPServer.Application.Mapping;
public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateCustomerCommand, Customer>();
    }
}
