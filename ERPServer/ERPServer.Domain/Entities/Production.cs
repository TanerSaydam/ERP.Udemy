using ERPServer.Domain.Abstractions;

namespace ERPServer.Domain.Entities;
public sealed class Production : Entity
{
    public Guid ProductId { get; set; }
    public Product? Product { get; set; }
    public decimal Quantity { get; set; }
    public DateTime CreatedAt { get; set; }
}
