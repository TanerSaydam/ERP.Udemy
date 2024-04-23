using ERPServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Invoices.GetAllInvoice;
public sealed record GetAllInvoiceQuery(
    int Type) : IRequest<Result<List<Invoice>>>;
