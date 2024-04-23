using ERPServer.Domain.Abstractions;

namespace ERPServer.Domain.Entities;

public sealed class OrderDetail : Entity
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public Product? Product { get; set; }
    public decimal Qantity { get; set; }
    public decimal Price { get; set; }
}