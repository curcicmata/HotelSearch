using HotelSearch.Application.Services;
using HotelSearch.Domain.Models;
using HotelSearch.Domain.Repository;
using HotelSearch.Test.Extensions;
using Moq;

namespace HotelSearch.Tests;

public class HotelSearchTestsBase
{

    protected readonly Mock<IHotelRepository> _mockRepository;
    protected readonly HotelService _hotelService;

    public HotelSearchTestsBase()
    {
        _mockRepository = new();
        _hotelService = new HotelService(_mockRepository.Object);
    }

    public static List<Hotel> GetHotelsForTests()
    {
        return
        [
            new()
                {
                    Id = 1,
                    Name = "The Westin Zagreb",
                    Price = 150,
                    Latitude = 45.8069425,
                    Longitude = 15.9663084
                },
                new()
                {
                    Id = 2,
                    Name = "Esplanade Zagreb Hotel",
                    Price = 200,
                    Latitude = 45.8054455,
                    Longitude = 15.9758849
                },
                new()
                {
                    Id = 3,
                    Name = "DoubleTree by Hilton Zagreb",
                    Price = 255,
                    Latitude = 45.8017439,
                    Longitude = 16.0013948
                },
                new()
                {
                    Id = 4,
                    Name = "Hotel International",
                    Price = 189,
                    Latitude = 45.7989001,
                    Longitude = 15.9739323
                },
                new()
                {
                    Id = 5,
                    Name = "Test Hotel",
                    Price = 99,
                    Latitude = 45.8069425,
                    Longitude = 15.9663084
                }
        ];
    }

    public static Hotel NewHotel()
    {
        return new()
        {
            Id = 66,
            Name = "Hotel 444",
            Price = 234,
            Latitude = 45.8069425,
            Longitude = 15.9663084
        };
    }

    public void GetHotelSearchRepository(List<Hotel> hotels)
    {
        _mockRepository
            .Setup(x => x.GetAllHotels())
            .ReturnsAsync(hotels);

        _mockRepository
            .Setup(x => x.GetHotelById(It.Is<int>(id => id.Equals(hotels.First().Id))))
            .Returns(Task.FromResult(hotels.First()));

        _mockRepository
            .Setup(x => x.SaveHotel(It.IsAny<Hotel>()))
            .ReturnsAsync((Hotel hotel) => hotel);
    }

    public void GetHotelsQueryable(IQueryable<Hotel> hotels)
    {
        var h = hotels.AsAsyncQueryable();

        _mockRepository
            .Setup(x => x.GetAllHotelsQueryable())
            .Returns(h);
    }
}
