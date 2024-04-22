using ERPServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Depots.GetAllDepot;
public sealed record GetAllDepotQuery() : IRequest<Result<List<Depot>>>;