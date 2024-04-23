using ERPServer.Domain.Dtos;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Orders.UpdateOrder;
public sealed record UpdateOrderCommand(
    Guid Id,
    Guid CustomerId,
    DateTime Date,
    DateTime DeliveryDate,
    List<OrderDetailDto> Details) : IRequest<Result<string>>;
