using Clean_architecture.Application.Common.Mappings;
using Clean_architecture.Domain.Entities;

namespace Clean_architecture.Application.ProductList.Queries.GetProductList;
public class ProductListDto : IMapFrom<demoindex>
{
    public string ProductReference { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public int Price { get; set; }
    public string customField => $"Product discription: {ProductReference} - {ProductName}";
}
