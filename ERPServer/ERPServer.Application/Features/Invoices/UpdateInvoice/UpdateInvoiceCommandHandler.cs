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
    IInvoiceDetailRepository invoiceDetailRepository,
    IStockMovementRepository stockMovementRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<UpdateInvoiceCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateInvoiceCommand request, CancellationToken cancellationToken)
    {
        Invoice? invoice =
            await invoiceRepository
            .WhereWithTracking(p => p.Id == request.Id)
            .Include(p=> p.Details)
            .FirstOrDefaultAsync(cancellationToken);

        if (invoice is null)
        {
            return Result<string>.Failure("Fatura bulunamadı");
        }

        List<StockMovement> movements =
            await stockMovementRepository
            .Where(p => p.InvoiceId == invoice.Id)
            .ToListAsync(cancellationToken);

        stockMovementRepository.DeleteRange(movements);

        invoiceDetailRepository.DeleteRange(invoice.Details);        

        invoice.Details = request.Details.Select(s => new InvoiceDetail
        {
            InvoiceId = invoice.Id,
            DepotId = s.DepotId,
            ProductId = s.ProductId,
            Price = s.Price,
            Quantity = s.Quantity
        }).ToList();

        await invoiceDetailRepository.AddRangeAsync(invoice.Details, cancellationToken);

        mapper.Map(request, invoice);

        List<StockMovement> newMovements = new();
        foreach (var item in request.Details)
        {
            StockMovement movement = new()
            {
                InvoiceId = invoice.Id,
                NumberOfEntries = invoice.Type.Value == 1 ? item.Quantity : 0,
                NumberOfOutputs = invoice.Type.Value == 2 ? item.Quantity : 0,
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
