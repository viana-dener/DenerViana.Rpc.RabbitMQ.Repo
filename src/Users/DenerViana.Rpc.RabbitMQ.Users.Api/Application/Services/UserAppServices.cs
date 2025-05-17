using AutoMapper;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Dtos;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Interfaces;
using DenerViana.Rpc.RabbitMQ.Users.Api.Application.Interfaces;
using DenerViana.Rpc.RabbitMQ.Users.Api.Application.Models.Request;
using DenerViana.Rpc.RabbitMQ.Users.Api.Application.Models.Response;
using DenerViana.Rpc.RabbitMQ.Users.Api.Domain.Entities;
using DenerViana.Rpc.RabbitMQ.Users.Api.Domain.Interfaces;

namespace DenerViana.Rpc.RabbitMQ.Users.Api.Application.Services;

public class UserAppServices(ILogInformation logInformation, INotify notify, IMapper mapper, IUserServices services) : IUserAppServices
{
    #region Properties

    private readonly ILogInformation _logInformation = logInformation;
    private readonly INotify _notify = notify;
    private readonly IMapper _mapper = mapper;
    private readonly IUserServices _services = services;

    #endregion

    #region Public Methods

    public async Task<IEnumerable<UserResponse>> GetAllAsync()
    {
        return _mapper.Map<IEnumerable<UserResponse>>(await _services.GetAllAsync());
    }
    public async Task<UserDetailsResponse> GetByIdAsync(Guid id)
    {
        return _mapper.Map<UserDetailsResponse>(await _services.GetByIdAsync(id));
    }
    public async Task<UserDetailsResponse> GetByNameAsync(string name)
    {
        return _mapper.Map<UserDetailsResponse>(await _services.GetByNameAsync(name));
    }

    public async Task<bool> AddAsync(UserRequest request, UserInfoDto userInfo)
    {
        if (await _services.ExistsAsync(request.Email))
        {
            _notify.AddError("Account already exists");
            return false;
        }

        var account = new User(userInfo.Origin, request.Name, request.Email, request.Password, userInfo.UserId, userInfo.UserName);

        var result = await _services.AddAsync(account);
        if (!result)
        {
            _notify.AddError("Error adding user", 500);
            return false;
        }

        _logInformation.PublicherLog("Usuário registrado com sucesso!");

        // Lançar evento de integração: Usuário registrado event

        return result;
    }

    #endregion
}