using HotelSearch.Domain.Models.User;

namespace HotelSearch.Application.Services;

public interface IUserService
{
    Task<AuthenticateResponse?> Authenticate(AuthenticateRequest model);
    Task<IEnumerable<User>> GetAll();
    Task<User?> GetById(int id);
}

