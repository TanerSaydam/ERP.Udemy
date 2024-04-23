using AutoMapper;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Invoices.CreateInvoice;

internal sealed class CreateInvoiceCommandHandler(
    IInvoiceRepository invoiceRepository,
    IStockMovementRepository stockMovementRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<CreateInvoiceCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
    {
        Invoice invoice = mapper.Map<Invoice>(request);

        if(invoice.Details is not null)
        {
            List<StockMovement> movements = new();
            foreach (var item in invoice.Details)
            {
                StockMovement movement = new()
                {
                    InvoiceId = item.InvoiceId,
                    NumberOfEntries = request.Type == 1 ? item.Quantity : 0,
                    NumberOfOutputs = request.Type == 2 ? item.Quantity : 0,
                    DepotId = item.DepotId,
                    Price = item.Price,
                    ProductId = item.ProductId,
                };

                movements.Add(movement);
            }

            await stockMovementRepository.AddRangeAsync(movements, cancellationToken);
        }        

        await invoiceRepository.AddAsync(invoice, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Fatura başarıyla oluşturuldu";
    }
}
