using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using ERPServer.Infrastructure.Context;
using GenericRepository;

namespace ERPServer.Infrastructure.Repositories;
internal sealed class InvoiceRepository : Repository<Invoice, ApplicationDbContext>, IInvoiceRepository
{
    public InvoiceRepository(ApplicationDbContext context) : base(context)
    {
    }
}
