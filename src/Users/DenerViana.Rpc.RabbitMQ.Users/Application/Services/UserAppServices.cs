using AutoMapper;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Dtos;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Interfaces;
using DenerViana.Rpc.RabbitMQ.Users.Application.Interfaces;
using DenerViana.Rpc.RabbitMQ.Users.Application.Models.Request;
using DenerViana.Rpc.RabbitMQ.Users.Application.Models.Response;
using DenerViana.Rpc.RabbitMQ.Users.Domain.Entities;
using DenerViana.Rpc.RabbitMQ.Users.Domain.Interfaces;

namespace DenerViana.Rpc.RabbitMQ.Users.Application.Services;

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

    public async Task<bool> RegisterUserAsync(RegisterUserRequest request, UserInfoDto userInfo)
    {
        if (await _services.ExistsAsync(request.Email))
        {
            _notify.AddError("Account already exists");
            return false;
        }

        var account = new User(userInfo.Origin, request.Name, request.Email, request.Password, userInfo.UserId);

        var result = await _services.RegisterUserAsync(account);
        if (result)
        {
            _logInformation.PublicherLog("Usuário registrado com sucesso!");

            // Lançar evento de integração: Usuário registrado event

            //var aux = RegisteredUserEvent();
        }


        return result;
    }

    #endregion

    #region Preivate Methods


    #endregion
}