using HotelSearch.Domain.Models.User;
using HotelSearch.Domain.Repository;
using Microsoft.Extensions.Options;

namespace HotelSearch.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<AuthenticateResponse?> Authenticate(AuthenticateRequest model)
    {
        return await _userRepository.Authenticate(model);
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        return await _userRepository.GetAll();
    }

    public async Task<User?> GetById(int id)
    {
        return await _userRepository.GetById(id);
    }
}

