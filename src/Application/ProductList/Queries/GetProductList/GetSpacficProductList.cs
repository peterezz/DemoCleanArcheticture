using AutoMapper;
using AutoMapper.QueryableExtensions;
using Clean_architecture.Application.Common.Repository;
using Clean_architecture.Domain.Entities;
using MediatR;

namespace Clean_architecture.Application.ProductList.Queries.GetProductList;
public class GetSpacficProductListQuery : IRequest<List<ProductListDto>>
{
    public string Must { get; set; } = string.Empty;
    public string Filter { get; set; } = string.Empty;
    public int Price { get; set; }
    public string mustnot { get; set; } = string.Empty;
}
public class GetSpacficProductListHandler : IRequestHandler<GetSpacficProductListQuery, List<ProductListDto>>
{
    private readonly IMapper _mapper;
    private readonly IndexRepository<demoindex> _indexRepository;

    public GetSpacficProductListHandler(IMapper mapper, IndexRepository<demoindex> indexRepository)
    {
        _mapper = mapper;
        _indexRepository = indexRepository;
    }
    public async Task<List<ProductListDto>> Handle(GetSpacficProductListQuery request, CancellationToken cancellationToken)
    {
        var data = await _indexRepository.BoolQuery(request.Must, price: request.Price);
        return data.Hits.Select(hit => hit.Source).AsQueryable().ProjectTo<ProductListDto>(_mapper.ConfigurationProvider).ToList();
    }
}
