using HotelSearch.Domain.Models;

namespace HotelSearch.Application.Services;


public interface IHotelService
{
    Task<List<Hotel>> GetAllHotels();
    Task<Hotel> GetHotelById(int id);
    Task<Hotel> SaveHotel(Hotel hotel);
    Task<bool> UpdateHotel(Hotel hotel);
    Task<bool> DeleteHotelById(int id);
    Task<List<Hotel>> GetNearbyHotelsAsync(double lat, double lng, double radius, int pageNumber, int pageSize);
}

