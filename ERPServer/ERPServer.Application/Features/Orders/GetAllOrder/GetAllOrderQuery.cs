using ERPServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Orders.GetAllOrder;
public sealed record GetAllOrderQuery() : IRequest<Result<List<Order>>>;
