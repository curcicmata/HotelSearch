using HotelSearch.Domain.Models;
using HotelSearch.Domain.Repository;
using HotelSearch.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelSearch.Infrastructure.Repository;

public class HotelRepository : IHotelRepository
{
    private readonly HotelContext _hotelContext;

    public HotelRepository(HotelContext hotelContext)
    {
        _hotelContext = hotelContext;
    }

    public async Task<bool> DeleteHotelById(int id)
    {
        var hotel = await _hotelContext.Set<Hotel>().FindAsync(id);

        if (hotel != null)
        {
            _hotelContext.Hotels.Remove(hotel);
            await _hotelContext.SaveChangesAsync();

            return true;
        }

        return false;
    }

    public async Task<List<Hotel>> GetAllHotels()
    {
        return await _hotelContext.Hotels.ToListAsync();
    }

    public IQueryable<Hotel> GetAllHotelsQueryable()
    {
        return _hotelContext.Hotels.AsQueryable();
    }

    public async Task<Hotel> GetHotelById(int id)
    {
        var hotel = await _hotelContext.Hotels.FindAsync(id);
        return hotel;
    }

    public async Task<Hotel> SaveHotel(Hotel hotel)
    {
        await _hotelContext.Hotels.AddAsync(hotel);
        await _hotelContext.SaveChangesAsync();

        return hotel;
    }

    public async Task<bool> UpdateHotel(Hotel hotel)
    {
        var hotelToUpdate = await _hotelContext.Hotels.FirstOrDefaultAsync(x => x.Id == hotel.Id);
        if (hotelToUpdate != null)
        {
            hotelToUpdate.Name = hotel.Name;
            hotelToUpdate.Price = hotel.Price;
            hotelToUpdate.Longitude = hotel.Longitude;
            hotelToUpdate.Latitude = hotel.Latitude;

            await _hotelContext.SaveChangesAsync();

            return true;
        }

        return false;
    }
}

