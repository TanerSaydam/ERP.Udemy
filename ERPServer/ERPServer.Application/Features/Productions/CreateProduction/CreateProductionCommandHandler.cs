using AutoMapper;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace ERPServer.Application.Features.Productions.CreateProduction;

internal sealed class CreateProductionCommandHandler(
    IProductionRepository productionRepository,
    IRecipeRepository recipeRepository,
    IStockMovementRepository stockMovementRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<CreateProductionCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateProductionCommand request, CancellationToken cancellationToken)
    {
        Production production = mapper.Map<Production>(request);
        List<StockMovement> newMovements = new();

        Recipe? recipe = 
            await recipeRepository
            .Where(p => p.ProductId == request.ProductId)
            .Include(p => p.Details!)
            .ThenInclude(p => p.Product)
            .FirstOrDefaultAsync(cancellationToken);

        if(recipe is not null && recipe.Details is not null)
        {
            var details = recipe.Details;
            foreach (var item in details)
            {
                List<StockMovement> movements = await stockMovementRepository.Where(p => p.ProductId == item.ProductId).ToListAsync(cancellationToken);

                List<Guid> depotIds = movements.GroupBy(p=> p.DepotId)
                    .Select(g=> g.Key)
                    .ToList();

                decimal stock = movements.Sum(p => p.NumberOfEntries) - movements.Sum(p => p.NumberOfOutputs);
                if(item.Quantity > stock)
                {
                    return Result<string>.Failure(item.Product!.Name + " ürününden üretim için yeterli miktarda yok. Eksik miktar: " + (item.Quantity - stock));
                }

                foreach (var depotId in depotIds)
                {
                    if (item.Quantity <= 0) break;

                    decimal quantity = movements.Where(p => p.DepotId == depotId).Sum(s => s.NumberOfEntries - s.NumberOfOutputs);

                    decimal totalPrice = 
                        movements
                        .Where(p => p.DepotId == depotId && p.NumberOfEntries > 0)
                        .Sum(s => s.Price);

                    decimal totalEntiresQuantity =
                        movements
                        .Where(p => p.DepotId == depotId && p.NumberOfEntries > 0)
                        .Sum(s => s.NumberOfEntries);

                    decimal price = totalPrice / totalEntiresQuantity;


                    StockMovement stockMovement = new()
                    {
                        ProductionId = production.Id,
                        DepotId = depotId,
                        Price = price
                    };

                    if (item.Quantity <= quantity)
                    {
                        stockMovement.NumberOfOutputs = item.Quantity;
                    }
                    else
                    {
                        stockMovement.NumberOfOutputs = quantity;
                    }
                        
                    item.Quantity -= quantity;

                    newMovements.Add(stockMovement);
                }                
            }
        }

        await stockMovementRepository.AddRangeAsync(newMovements, cancellationToken);
        await productionRepository.AddAsync(production, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Ürün başarıyla üretildi";
    }
}
