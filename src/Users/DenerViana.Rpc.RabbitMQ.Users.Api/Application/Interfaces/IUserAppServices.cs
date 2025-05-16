using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Dtos;
using DenerViana.Rpc.RabbitMQ.Users.Api.Application.Models.Request;
using DenerViana.Rpc.RabbitMQ.Users.Api.Application.Models.Response;

namespace DenerViana.Rpc.RabbitMQ.Users.Api.Application.Interfaces;

public interface IUserAppServices
{
    Task<IEnumerable<UserResponse>> GetAllAsync();
    Task<UserDetailsResponse> GetByIdAsync(Guid id);
    Task<UserDetailsResponse> GetByNameAsync(string name);

    Task<bool> RegisterUserAsync(RegisterUserRequest request, UserInfoDto userInfo);
}
