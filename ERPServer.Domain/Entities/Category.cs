using ERPServer.Domain.Abstractions;

namespace ERPServer.Domain.Entities;

public sealed class Category : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<Product>? Products { get; set; }
}
