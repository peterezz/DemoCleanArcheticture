using AutoMapper;
using AutoMapper.QueryableExtensions;
using Clean_architecture.Application.Common.Repository;
using Clean_architecture.Domain.Entities;
using MediatR;

namespace Clean_architecture.Application.ProductList.Queries.GetProductList;
public class GetFilterProductListQuery : MediatR.IRequest<List<ProductListDto>>
{
    public string FieldName { get; set; }
}
public class GetFilterProductListQueryHandeler : IRequestHandler<GetFilterProductListQuery, List<ProductListDto>>
{
    private readonly IndexRepository<demoindex> _indexRepository;
    private readonly IMapper _mapper;

    public GetFilterProductListQueryHandeler(IndexRepository<demoindex> indexRequest, IMapper mapper)
    {
        _indexRepository = indexRequest;
        _mapper = mapper;
    }
    public async Task<List<ProductListDto>> Handle(GetFilterProductListQuery request, CancellationToken cancellationToken)
    {
        var searchResult = await _indexRepository.PerformFilterAggregation(request.FieldName);
        if (searchResult.IsValid)
        {

            var filterAggrate = searchResult.Aggregations.Filters("filters_buckets");
            var singleBucketAggregate = filterAggrate.NamedBucket("Product refrances starts with 14....");
            var topHitsAggregate = singleBucketAggregate.TopHits("my_docs");
            return topHitsAggregate.Documents<demoindex>().AsQueryable().ProjectTo<ProductListDto>(_mapper.ConfigurationProvider).ToList();

        }

        return null;
    }
}