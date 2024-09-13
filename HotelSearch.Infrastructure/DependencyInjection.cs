using HotelSearch.Domain.Repository;
using HotelSearch.Infrastructure.Data;
using HotelSearch.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HotelSearch.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<HotelContext>(options =>
            options.UseInMemoryDatabase("HotelList"));


        services.AddHotelRepositories();

        return services;
    }

    private static void AddHotelRepositories(this IServiceCollection services)
    {
        services.AddTransient<IHotelRepository, HotelRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
    }
}

