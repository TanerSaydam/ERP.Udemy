using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Productions.DeleteProductionById;
public sealed record DeleteProductionByIdCommand(
    Guid Id): IRequest<Result<string>>;
