using Clean_architecture.Domain.Entities;
using Nest;

namespace Clean_architecture.Infrastructure.Persistence.Configurations;
public static class DemoIndexConfiguration
{
    public static ICreateIndexRequest CreateMyOwnMapping(this CreateIndexDescriptor indexDescriptor)
    {
        return indexDescriptor.Map<demoindex>(mm => mm.Properties(p => p
         .Keyword(k => k
             .Name(n => n.ProductReference)
         )
         .Text(t => t
         .Name(n => n.ProductName)
         .Analyzer("my_analyzer")
         )
         .Number(n => n
         .Name(n => n.Price)
         .Type(NumberType.Integer)
         )
        )
       );

    }
}
