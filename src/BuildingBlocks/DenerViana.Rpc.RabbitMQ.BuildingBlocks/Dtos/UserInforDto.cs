namespace DenerViana.Rpc.RabbitMQ.BuildingBlocks.Dtos;

public class UserInfoDto
{
    public string Origin { get; set; }
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string UserBusinessArea { get; set; }
    public string CorrelationId { get; set; }
}
