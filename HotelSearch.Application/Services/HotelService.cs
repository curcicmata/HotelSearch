using HotelSearch.Domain.Models;
using HotelSearch.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;

namespace HotelSearch.Application.Services;

public class HotelService : IHotelService
{
    private readonly IHotelRepository _repository;

    public HotelService(IHotelRepository repository)
    {
        _repository = repository;
    }

    public Task<bool> DeleteHotelById(int id)
    {
        return _repository.DeleteHotelById(id);
    }

    public async Task<List<Hotel>> GetAllHotels()
    {
        return await _repository.GetAllHotels();
    }

    public async Task<Hotel> GetHotelById(int id)
    {
        return await _repository.GetHotelById(id);
    }

    public async Task<Hotel> SaveHotel(Hotel hotel)
    {
        return await _repository.SaveHotel(hotel);
    }

    public async Task<bool> UpdateHotel(Hotel hotel)
    {
        return await _repository.UpdateHotel(hotel);
    }

    public async Task<List<Hotel>> GetNearbyHotelsAsync(double lat, double lng, double radius, int pageNumber = 1, int pageSize = 10)
    {
        var nearbyHotels = await _repository.GetAllHotelsQueryable()
            .Select(hotel => new
            {
                Hotel = hotel,
                Distance = GetDistance(lat, lng, hotel.Latitude, hotel.Longitude)
            })
            .Where(h => h.Distance <= radius)
            .OrderBy(h => h.Distance)
                .ThenBy(h => h.Hotel.Price)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(h => h.Hotel)
            .ToListAsync();


        return nearbyHotels;
    }

    /// <summary>
    /// Calculate the distance between two points on the Earth's surface using the Haversine formula.
    /// It converts the differences in latitude (dLat) and longitude (dLng) between the two points from degrees to radians (since trigonometric functions work in radians).
    /// </summary>
    /// <param name="lat1">34.0522</param>
    /// <param name="lng1">-118.2437</param>
    /// <param name="lat2"></param>
    /// <param name="lng2"></param>
    /// <returns></returns>
    private static double GetDistance(double lat1, double lng1, double lat2, double lng2)
    {
        var R = 6371; // Radius of the Earth in kilometers
        var dLat = ToRadians(lat2 - lat1);
        var dLng = ToRadians(lng2 - lng1);
        var a =
            System.Math.Sin(dLat / 2) * System.Math.Sin(dLat / 2) +
            System.Math.Cos(ToRadians(lat1)) * System.Math.Cos(ToRadians(lat2)) *
            System.Math.Sin(dLng / 2) * System.Math.Sin(dLng / 2);
        var c = 2 * System.Math.Atan2(System.Math.Sqrt(a), System.Math.Sqrt(1 - a));
        return R * c; // Distance in kilometers
    }

    /// <summary>
    /// Converts an angle from degrees to radians (for distance calculation).
    /// </summary>
    /// <param name="deg"></param>
    /// <returns></returns>
    private static double ToRadians(double deg)
    {
        return deg * (System.Math.PI / 180);
    }
}

