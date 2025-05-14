namespace DenerViana.Rpc.RabbitMQ.BuildingBlocks.Dtos;

public class HeaderErrorsDto
{
    public bool Result { get; set; }

    public Dictionary<string, string> Errors { get; set; }
}
