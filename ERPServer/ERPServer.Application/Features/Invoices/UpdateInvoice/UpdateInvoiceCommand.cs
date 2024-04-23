using ERPServer.Domain.Dtos;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Invoices.UpdateInvoice;
public sealed record UpdateInvoiceCommand(
    Guid Id,
    DateOnly Date,
    string InvoiceNumber,
    List<InvoiceDetailDto> Details) : IRequest<Result<string>>;
