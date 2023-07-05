using Nest;

namespace Clean_architecture.Application.Common.Repository;
public class IndexRepository<TDocument> where TDocument : class
{
    private readonly ElasticClient _elasticClient;

    public IndexRepository(ElasticClient elasticClient)
    {
        _elasticClient = elasticClient;

    }
    #region MY Requests
    public async Task<IndexResponse> IndexDocument(TDocument document)
    {
        var documentResponse = await _elasticClient.IndexDocumentAsync(document);
        if (documentResponse.IsValid)
            Console.WriteLine("document added successfully \n");
        else
            Console.WriteLine("Error Occuared");
        return documentResponse;

    }

    public async Task<BulkResponse> BulkCreate(TDocument[] DocumentArray, string indexName)
    {
        var indexManyResponse = await _elasticClient.BulkAsync(b => b.Index(indexName).IndexMany(DocumentArray));
        if (!indexManyResponse.Errors)
            Console.WriteLine("Bulk Create requested successfully \n -------------\n documents \n------------------------------");
        else
            Console.WriteLine("Error Occuared");

        return indexManyResponse;

    }

    public async Task<ISearchResponse<TDocument>> MatchAllQuery()
    {
        var searchResponse = await _elasticClient.SearchAsync<TDocument>(s => s
            .Query(q => q
                .MatchAll()
            )
        );
        return searchResponse;

    }
    public async Task<ISearchResponse<TDocument>> BoolQuery(string must, string filter = null, int price = 0, string mustnot = null)
    {

        return await _elasticClient.SearchAsync<TDocument>(s => s
        .Query(q => q
            .Bool(b => b
             //.Must(b => b
             //    .Match(t => t.Field("productReference").Query(must))

             //)
             //.Must(f => f
             //    .Term(t => t.ProductReference, must)

             //)
             .Should(

                 s => s.Range(r => r.Field("price").GreaterThan(4060))
             )
             )
         )
        .Size(100));
    }

    //aggregation
    public async Task<ISearchResponse<TDocument>> PreformSumAggregation(string fieldName)
    {
        return await _elasticClient.SearchAsync<TDocument>(s => s
             .Aggregations(a => a
                 .Sum("total_price", sa => sa
                   .Field(fieldName)
               )
              )
            );

    }
    public async Task<ISearchResponse<TDocument>> PerformFilterAggregation(string fieldName)
    {
        return await _elasticClient.SearchAsync<TDocument>(s => s.Size(0)
        .Aggregations(a => a.Filters("filters_buckets", f => f
        .OtherBucket()
        .OtherBucketKey("Other state bucket")
        .NamedFilters(fillter => fillter

        .Filter("Product refrances starts with 14....", f => f.Wildcard(m => m.Field(fieldName).Value("14*")))
        .Filter("Product refrances starts with 15....", f => f.Wildcard(w => w.Field(fieldName).Value("15*")))).Aggregations(aa => aa
            .TopHits("my_docs", tha => tha
                .Size(10)   // limit to 10 documents per bucket
            )))

        ));
    }

    #endregion
}
