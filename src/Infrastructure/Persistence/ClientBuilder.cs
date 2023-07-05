using Clean_architecture.Domain.Configurations;
using Clean_architecture.Infrastructure.Persistence.Configurations;
using Elasticsearch.Net;
using Microsoft.Extensions.Options;
using Nest;
using Nest.JsonNetSerializer;
using Newtonsoft.Json;

namespace Clean_architecture.Infrastructure.Persistence;
public class ClientBuilder
{
    private readonly ElasticsearchConfig _elasticsearchConfig;
    public ClientBuilder(IOptions<ElasticsearchConfig> options)
    {
        _elasticsearchConfig = options.Value;
    }



    public ElasticClient CreateIndexIfNotExists<TMap>(string indexName, ConnectionSettings settings)
        where TMap : class
    {
        //var client = new ElasticClient();
        var client = CreateClient(settings);
        if (!client.Indices.Exists(indexName).Exists)
        {


            SendCreateIndexRequest(client, indexName);
        }
        else
            PrintIndexMapping(client, indexName);

        return client;
    }
    private static void SendCreateIndexRequest(ElasticClient client, string indexName)
    {
        var createIndexResponse = client.Indices.Create(indexName, c => c
        .Settings(s => s
            .Analysis(a => a
                .Analyzers(an => an
                    .Standard("my_analyzer", sa => sa
                        .StopWords("_english_")
                    )
                )
            )
        ).CreateMyOwnMapping()
        )
        ;



        if (createIndexResponse.IsValid)
        {
            Console.WriteLine("Index created successfully!");

        }
        else
        {
            Console.WriteLine("Failed to create index: " + createIndexResponse.ServerError.Error);
        }
    }
    public static ElasticClient CreateClient(ConnectionSettings settings)
                 => new ElasticClient(settings);

    public ConnectionSettings BuildConnectionSettings<TMap>()
        where TMap : class
    {
        var esUri = new Uri(_elasticsearchConfig.url);
        var pool = new SingleNodeConnectionPool(esUri);
        var settings = new ConnectionSettings(pool, sourceSerializer: (builtin, settings) =>
        {
            return new JsonNetSerializer(builtin, settings,
                   () => new JsonSerializerSettings
                   {
                       NullValueHandling = NullValueHandling.Ignore,
                       ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                       Formatting = Formatting.Indented
                   }
                                        );
        })
            .DefaultIndex(_elasticsearchConfig.Node.indexName)
            .DefaultMappingFor<TMap>(m => m.IndexName(_elasticsearchConfig.Node.indexName))
            .PrettyJson(true)
            .DisableDirectStreaming(true)
            .BasicAuthentication(_elasticsearchConfig.userName, _elasticsearchConfig.password)
            .ServerCertificateValidationCallback(CertificateValidations.AllowAll);
        //.OnRequestCompleted(details => EsLogRequest(esconfig, details));
        Console.WriteLine("Connection established successfully \n");
        return settings;
    }

    private static void PrintIndexMapping(ElasticClient client, string indexName)
    {
        var mappingResponse = client.Indices.GetMapping(new GetMappingRequest(indexName));
        if (mappingResponse.IsValid)
        {
            var mapping = mappingResponse.Indices[indexName].Mappings;
            Console.WriteLine("------------------- Mappings -------------------\n");
            foreach (var keyValuePair in mapping.Properties)
            {
                Console.WriteLine($"{keyValuePair.Key}: {keyValuePair.Value}\t");
            }
        }
        else
        {
            Console.WriteLine("Failed to get mapping: " + mappingResponse.ServerError.Error);
        }
    }

}
