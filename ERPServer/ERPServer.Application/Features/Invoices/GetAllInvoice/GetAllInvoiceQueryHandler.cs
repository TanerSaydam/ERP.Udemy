﻿using ERPServer.Domain.Entities;
using ERPServer.Domain.Enums;
using ERPServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace ERPServer.Application.Features.Invoices.GetAllInvoice;

internal sealed class GetAllInvoiceQueryHandler(
    IInvoiceRepository invoiceRepository) : IRequestHandler<GetAllInvoiceQuery, Result<List<Invoice>>>
{
    public async Task<Result<List<Invoice>>> Handle(GetAllInvoiceQuery request, CancellationToken cancellationToken)
    {
        List<Invoice> invoices = 
            await invoiceRepository
            .Where(p => p.Type == InvoiceTypeEnum.FromValue(request.Type))
            .Include(p=> p.Customer)
            .Include(p=> p.Details!)
            .ThenInclude(p=> p.Product)
            .Include(p=> p.Details!)
            .ThenInclude(p=> p.Depot)
            .OrderBy(p => p.Date)
            .ToListAsync(cancellationToken);

        return invoices;
    }
}
