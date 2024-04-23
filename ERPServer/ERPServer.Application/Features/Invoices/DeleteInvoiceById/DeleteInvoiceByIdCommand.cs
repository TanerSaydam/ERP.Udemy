using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Invoices.DeleteInvoiceById;
public sealed record DeleteInvoiceByIdCommand(
    Guid Id) : IRequest<Result<string>>;
