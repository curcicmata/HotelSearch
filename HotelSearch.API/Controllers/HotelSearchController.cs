using HotelSearch.Application.Services;
using HotelSearch.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelSearch.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HotelSearchController : ControllerBase
{
    private readonly IHotelService _hotelService;

    public HotelSearchController(IHotelService hotelService)
    {
        _hotelService = hotelService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Hotel>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<Hotel>>> GetAllHotels()
    {
        return await _hotelService.GetAllHotels();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Hotel))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<Hotel> GetHotelById(int id)
    {
        return await _hotelService.GetHotelById(id);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Hotel))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<Hotel> SaveHotel(Hotel hotel)
    {
        return await _hotelService.SaveHotel(hotel);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<bool> UpdateHotel(Hotel hotel)
    {
        return await _hotelService.UpdateHotel(hotel);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<bool> DeleteHotel(int id)
    {
        return await _hotelService.DeleteHotelById(id);
    }

    // lat=45.8132734&lng=15.976034&radius=10
    [HttpGet("search")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<Hotel>>> GetNearbyHotels(
        [FromQuery] double lat,
        [FromQuery] double lng,
        [FromQuery] double radius,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        if (pageNumber <= 0 || pageSize <= 0)
        {
            return BadRequest("Page number and page size should be greater than 0.");
        }

        var nearbyHotels = await _hotelService.GetNearbyHotelsAsync(lat, lng, radius, pageNumber, pageSize);

        return Ok(nearbyHotels);
    }
}

