using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace ERPServer.Application.Features.Productions.DeleteProductionById;

internal sealed class DeleteProductionByIdCommandHandler(
    IProductionRepository productionRepository,
    IStockMovementRepository stockMovementRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteProductionByIdCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteProductionByIdCommand request, CancellationToken cancellationToken)
    {
        Production? production = await productionRepository.GetByExpressionWithTrackingAsync(p => p.Id == request.Id, cancellationToken);

        if(production is null)
        {
            return Result<string>.Failure("Üretim bulunamadı");
        }

        List<StockMovement> movements = await stockMovementRepository.Where(p => p.ProductionId == production.Id).ToListAsync(cancellationToken);

        stockMovementRepository.DeleteRange(movements);

        productionRepository.Delete(production);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Üretim başarıyla silindi";
    }
}
