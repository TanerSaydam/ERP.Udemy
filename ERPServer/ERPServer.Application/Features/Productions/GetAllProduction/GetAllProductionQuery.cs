using ERPServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Productions.GetAllProduction;
public sealed record GetAllProductionQuery() : IRequest<Result<List<Production>>>;
