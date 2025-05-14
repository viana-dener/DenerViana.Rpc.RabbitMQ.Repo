namespace DenerViana.Rpc.RabbitMQ.BuildingBlocks.Interfaces;

public interface INotify
{
    ICollection<string> GetErrors();
    int GetStatusCode();
    void AddError(string error);
    void AddError(string error, int statusCode);
    void ClearErrors();
}

