using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace ERPServer.Application.Features.Productions.GetAllProduction;

internal sealed class GetAllProductionQueryHandler(
    IProductionRepository productionRepository) : IRequestHandler<GetAllProductionQuery, Result<List<Production>>>
{
    public async Task<Result<List<Production>>> Handle(GetAllProductionQuery request, CancellationToken cancellationToken)
    {
        List<Production> productions = 
            await productionRepository
            .GetAll()
            .Include(p=> p.Product)
            .Include(p=> p.Depot)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync(cancellationToken);

        return productions;
    }
}
