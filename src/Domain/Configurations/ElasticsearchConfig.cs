namespace Clean_architecture.Domain.Configurations;
public class ElasticsearchConfig
{

    public NodeConfig Node { get; set; } = new NodeConfig();
    public string url { get; set; } = string.Empty;
    public string userName { get; set; } = string.Empty;
    public string password { get; set; } = string.Empty;
}
