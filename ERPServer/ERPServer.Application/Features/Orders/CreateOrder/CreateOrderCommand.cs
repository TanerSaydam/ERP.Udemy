using ERPServer.Domain.Dtos;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Orders.CreateOrder;
public sealed record CreateOrderCommand(
    Guid CustomerId,
    DateOnly Date,
    DateOnly DeliveryDate,
    List<OrderDetailDto> Details) : IRequest<Result<string>>;
