using ERPServer.Domain.Abstractions;

namespace ERPServer.Domain.Entities;

public sealed class InvoiceDetail : Entity
{
    public Guid InvoiceId { get; set; }
    public Guid ProductId { get; set; }
    public Product? Product { get; set; }
    public Guid DepotId { get; set; }
    public Depot? Depot { get; set; }
    public decimal Quantity { get; set; }
    public decimal Price { get; set; }
}