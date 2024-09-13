using HotelSearch.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

