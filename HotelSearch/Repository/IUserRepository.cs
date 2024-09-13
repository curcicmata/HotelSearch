using HotelSearch.Domain.Models.User;

namespace HotelSearch.Domain.Repository
{
    public interface IUserRepository
    {
        Task<AuthenticateResponse?> Authenticate(AuthenticateRequest model);
        Task<IEnumerable<User>> GetAll();
        Task<User?> GetById(int id);
    }
}
