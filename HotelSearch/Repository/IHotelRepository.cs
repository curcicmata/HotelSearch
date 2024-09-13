using HotelSearch.Domain.Models;

namespace HotelSearch.Domain.Repository;

public interface IHotelRepository
{
    Task<List<Hotel>> GetAllHotels();
    IQueryable<Hotel> GetAllHotelsQueryable();
    Task<Hotel> GetHotelById(int id);
    Task<Hotel> SaveHotel(Hotel hotel);
    Task<bool> UpdateHotel(Hotel hotel);
    Task<bool> DeleteHotelById(int id);
}

