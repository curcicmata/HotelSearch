using HotelSearch.Domain.Models;
using HotelSearch.Test.Extensions;

namespace HotelSearch.Tests;
public class HotelSearchTests : HotelSearchTestsBase
{

    [Fact]
    public async Task GetAllHotels_ReturnsListOfHotels()
    {
        // Arrange
        var hotels = GetHotelsForTests();
        GetHotelSearchRepository(hotels);

        // Act
        var result = await _hotelService.GetAllHotels();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(5, result.Count);
        Assert.Equal("The Westin Zagreb", result[0].Name);
        Assert.Equal("Esplanade Zagreb Hotel", result[1].Name);
    }

    [Fact]
    public async Task GetHotelsById_ReturnsFirstHotelFromList()
    {
        var hotels = GetHotelsForTests();
        GetHotelSearchRepository(hotels);

        var result = await _hotelService.GetHotelById(1);

        Assert.NotNull(result);
        Assert.Equal("The Westin Zagreb", result.Name);
    }


    [Fact]
    public async Task GetHotelsById_ReturnsNonExistingHotelFromList()
    {
        var hotels = GetHotelsForTests();
        GetHotelSearchRepository(hotels);

        var result = await _hotelService.GetHotelById(999);

        Assert.Null(result);
    }

    [Fact]
    public async Task SaveNewHotel_ReturnsSavedHotel()
    {
        var hotels = GetHotelsForTests();
        GetHotelSearchRepository(hotels);

        var newHotel = NewHotel();

        var result = await _hotelService.SaveHotel(newHotel);

        Assert.NotNull(result);
        Assert.IsType<Hotel>(result);
        Assert.Equal("Hotel 444", result.Name);
    }

    [Fact]
    public async Task GetNearbyHotels_CheapestAtTheSameLocationIsFirst()
    {
        var hotels = GetHotelsForTests().AsAsyncQueryable();
        GetHotelsQueryable(hotels);

        var result = await _hotelService.GetNearbyHotelsAsync(45.8069425, 15.9663084, 50, 1, 10);

        Assert.NotNull(result);
        Assert.Equal(5, result[0].Id);
        Assert.Equal("Test Hotel", result[0].Name);
    }

    [Fact]
    public async Task GetNearbyHotels_DistanceIsTooBig_ReturnsEmptyList()
    {
        var hotels = GetHotelsForTests().AsAsyncQueryable();
        GetHotelsQueryable(hotels);

        var result = await _hotelService.GetNearbyHotelsAsync(66.8069425, 20.9663084, 10, 1, 10);

        Assert.NotNull(result);
        Assert.Empty(result);
    }
}


