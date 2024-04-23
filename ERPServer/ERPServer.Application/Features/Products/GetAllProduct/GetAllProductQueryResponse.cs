using ERPServer.Domain.Enums;

namespace ERPServer.Application.Features.Products.GetAllProduct;

public sealed record GetAllProductQueryResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ProductTypeEnum Type { get; set; } = ProductTypeEnum.Product;
    public decimal Stock { get; set; }
}
