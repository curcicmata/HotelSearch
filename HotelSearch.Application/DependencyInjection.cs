using HotelSearch.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HotelSearch.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddHotelServices();
        services.AddGoogleMapsService();
        services.AddAuthService();

        return services;
    }

    private static void AddHotelServices(this IServiceCollection services)
    {
        services.AddTransient<IHotelService, HotelService>();
    }

    private static void AddGoogleMapsService(this IServiceCollection services) 
    { 
        services.AddScoped<IGoogleMapsServices, GoogleMapsServices>();
    }

    private static void AddAuthService(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
    }
}

