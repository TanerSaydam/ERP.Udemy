using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Customers.CreateCustomer;
public sealed record CreateCustomerCommand(
    string Name,
    string TaxDepartment,
    string TaxNumber,
    string City,
    string Town,
    string FullAddress) : IRequest<Result<string>>;
