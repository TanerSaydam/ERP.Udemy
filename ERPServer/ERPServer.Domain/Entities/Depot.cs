using ERPServer.Domain.Abstractions;

namespace ERPServer.Domain.Entities;
public sealed class Depot : Entity
{
    public string Name { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Town { get; set; } = string.Empty;
    public string FullAddress { get; set; } = string.Empty;
}
