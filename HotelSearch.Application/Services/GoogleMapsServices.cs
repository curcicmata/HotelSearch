using GoogleMaps.LocationServices;
using HotelSearch.Application.Configuration;
using Microsoft.Extensions.Options;


namespace HotelSearch.Application.Services;

public class GoogleMapsServices : IGoogleMapsServices
{
    private readonly GoogleMapsSettings _settings;

    public GoogleMapsServices(IOptions<GoogleMapsSettings> option)
    {
        _settings = option.Value;
    }

    public (double Latitude, double Longitude) GetCoordinatesFromAddress(string address)
    {
        var locationService = new GoogleLocationService(_settings.ApiKey);
        var location = locationService.GetLatLongFromAddress(address);

        if (location == null)
        {
            throw new Exception("Unable to get the coordinates for the given address.");
        }

        return (location.Latitude, location.Longitude);
    }
}

