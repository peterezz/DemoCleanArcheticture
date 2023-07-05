using AutoMapper;
using AutoMapper.QueryableExtensions;
using Clean_architecture.Application.Common.Repository;
using Clean_architecture.Domain.Entities;
using MediatR;

namespace Clean_architecture.Application.ProductList.Queries.GetProductList;
public record class GetProductListQuery : IRequest<List<ProductListDto>>;

public class GetProductListQueryHandeler : IRequestHandler<GetProductListQuery, List<ProductListDto>>
{
    private readonly IndexRepository<demoindex> _repository;
    private readonly IMapper _mapper;

    public GetProductListQueryHandeler(IndexRepository<demoindex> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<List<ProductListDto>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
    {
        var searchresult = await _repository.MatchAllQuery();
        return searchresult.Hits.Select(hit => hit.Source).AsQueryable().ProjectTo<ProductListDto>(_mapper.ConfigurationProvider).ToList();



    }
}
