using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Depots.DeleteDepotById;
public sealed record DeleteDepotByIdCommand(
    Guid Id) : IRequest<Result<string>>;
