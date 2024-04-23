using ERPServer.Domain.Dtos;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Orders.CreateOrder;
public sealed record CreateOrderCommand(
    Guid CustomerId,
    DateTime Date,
    DateTime DeliveryDate,
    List<OrderDetailDto> Details) : IRequest<Result<string>>;
