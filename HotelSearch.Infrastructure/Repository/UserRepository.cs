using HotelSearch.Domain.Models.User;
using HotelSearch.Domain.Repository;
using HotelSearch.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HotelSearch.Infrastructure.Repository;


public class UserRepository : IUserRepository
{
    private readonly HotelContext _hotelContext;
    private readonly AppSettings _appSettings;

    public UserRepository(HotelContext hotelContext, IOptions<AppSettings> appSettings)
    {
        _hotelContext = hotelContext;
        _appSettings = appSettings.Value;
    }


    public async Task<User?> AddAndUpdateUser(User userObj)
    {
        bool isSuccess = false;
        if (userObj.Id > 0)
        {
            var obj = await _hotelContext.Users.FirstOrDefaultAsync(c => c.Id == userObj.Id);
            if (obj != null)
            {
                obj.FirstName = userObj.FirstName;
                obj.LastName = userObj.LastName;
                _hotelContext.Users.Update(obj);

                isSuccess = await _hotelContext.SaveChangesAsync() > 0;
            }
        }
        else
        {
            await _hotelContext.Users.AddAsync(userObj);
            isSuccess = await _hotelContext.SaveChangesAsync() > 0;
        }

        return isSuccess ? userObj : null;
    }

    public async Task<AuthenticateResponse?> Authenticate(AuthenticateRequest model)
    {
        var user = await _hotelContext.Users.SingleOrDefaultAsync(x => x.Username == model.Username && x.Password == model.Password);

        if (user == null)
        {
            return null;
        }

        var token = await GenerateJwtToken(user);


        return new AuthenticateResponse(user, token);
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        return await _hotelContext.Users.Where(x => x.isActive == true).ToListAsync();

    }

    public async Task<User?> GetById(int id)
    {
        return await _hotelContext.Users.FirstOrDefaultAsync(x => x.Id == id);

    }



    private async Task<string> GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = await Task.Run(() =>
        {

            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            return tokenHandler.CreateToken(tokenDescriptor);
        });

        return tokenHandler.WriteToken(token);
    }
}

