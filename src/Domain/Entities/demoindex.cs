using Nest;

namespace Clean_architecture.Domain.Entities;

[ElasticsearchType(IdProperty = nameof(ProductReference))]
public class demoindex
{
    [Text(Index = true)]
    public string ProductReference { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public int Price { get; set; }
}
