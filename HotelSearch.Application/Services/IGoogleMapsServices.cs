namespace HotelSearch.Application.Services;


public interface IGoogleMapsServices
{
    (double Latitude, double Longitude) GetCoordinatesFromAddress(string address);
}

