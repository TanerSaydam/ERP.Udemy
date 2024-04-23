using AutoMapper;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace ERPServer.Application.Features.Invoices.UpdateInvoice;

internal sealed class UpdateInvoiceCommandHandler(
    IInvoiceRepository invoiceRepository,
    IStockMovementRepository stockMovementRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<UpdateInvoiceCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateInvoiceCommand request, CancellationToken cancellationToken)
    {
        Invoice? invoice =
            await invoiceRepository.GetByExpressionWithTrackingAsync(p => p.Id == request.Id, cancellationToken);

        if (invoice is null)
        {
            return Result<string>.Failure("Fatura bulunamadı");
        }

        List<StockMovement> movements =
            await stockMovementRepository
            .Where(p => p.InvoiceId == invoice.Id)
            .ToListAsync(cancellationToken);

        stockMovementRepository.DeleteRange(movements);

        mapper.Map(request, invoice);

        List<StockMovement> newMovements = new();
        foreach (var item in request.Details)
        {
            StockMovement movement = new()
            {
                InvoiceId = invoice.Id,
                NumberOfEntries = request.Type == 1 ? item.Quantity : 0,
                NumberOfOutputs = request.Type == 2 ? item.Quantity : 0,
                DepotId = item.DepotId,
                Price = item.Price,
                ProductId = item.ProductId,
            };

            newMovements.Add(movement);
        }

        await stockMovementRepository.AddRangeAsync(newMovements, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Fatura başarıyla güncelleştirildi";
    }
}
