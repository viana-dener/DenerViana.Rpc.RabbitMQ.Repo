namespace DenerViana.Rpc.RabbitMQ.BuildingBlocks.Settings;

public class AppSettings
{
    public string Key { get; set; }
    public string Secret { get; set; }
    public string Elasticsearch { get; set; }
    public string[] Origins { get; set; }
}
