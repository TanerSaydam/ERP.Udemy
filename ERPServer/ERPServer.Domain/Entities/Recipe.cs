using ERPServer.Domain.Abstractions;

namespace ERPServer.Domain.Entities;
public sealed class Recipe : Entity
{
    public Guid ProductId { get; set; }
    public Product? Product { get; set; }
    public List<RecipeDetail>? Details { get; set; }
}
