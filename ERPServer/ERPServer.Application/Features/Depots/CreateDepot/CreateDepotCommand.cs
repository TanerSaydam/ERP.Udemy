using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Depots.CreateDepot;
public sealed record CreateDepotCommand(
    string Name,
    string City,
    string Town,
    string FullAddress) : IRequest<Result<string>>;
