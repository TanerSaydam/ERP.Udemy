using ERPServer.Domain.Abstractions;
using ERPServer.Domain.Enums;

namespace ERPServer.Domain.Entities;
public sealed class Order : Entity
{
    public Guid CustomerId { get; set; }
    public Customer? Customer { get; set; }

    public string OrderNumber { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public DateTime DeliveryDate { get; set; }
    public OrderStatusEnum Status { get; set; } = OrderStatusEnum.Pending;
    public List<OrderDetail>? Details { get; set; }
}
