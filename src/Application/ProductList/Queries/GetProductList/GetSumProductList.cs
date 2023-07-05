using AutoMapper;
using Clean_architecture.Application.Common.Repository;
using Clean_architecture.Domain.Entities;
using MediatR;

namespace Clean_architecture.Application.ProductList.Queries.GetProductList;
public class GetSumProductListQuery : IRequest<string>
{
    public string FieldName { get; set; } = string.Empty;
}
public class GetSumProductListQueryHandler : IRequestHandler<GetSumProductListQuery, string>
{
    private readonly IMapper _mapper;
    private readonly IndexRepository<demoindex> _indexRepository;

    public GetSumProductListQueryHandler(IMapper mapper, IndexRepository<demoindex> indexRepository)
    {
        _mapper = mapper;
        _indexRepository = indexRepository;
    }

    public async Task<string> Handle(GetSumProductListQuery request, CancellationToken cancellationToken)
    {
        var searchResponse = await _indexRepository.PreformSumAggregation(request.FieldName);
        double? resultCount = 0;
        if (searchResponse.IsValid)
        {
            resultCount = searchResponse.Aggregations.Sum("total_price").Value;
            if (resultCount <= 0)
            {
                return "No product found!";
            }
            return $"Total product prices: ${resultCount}";
        }
        else
            return "Not valid command!";
    }
}
