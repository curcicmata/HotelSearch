using HotelSearch.Application.Services;
using HotelSearch.Domain.Models;

using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using static HotelSearch.API.Helpers.JwtMiddleware;

namespace HotelSearch.API.Controllers;

//[Authorize]
[Route("api/[controller]")]
[ApiController]
public class GoogleMapsCoordinatesController : ControllerBase
{
    private readonly IGoogleMapsServices _googleMapsServices;

    public GoogleMapsCoordinatesController(IGoogleMapsServices googleMapsServices)
    {
        _googleMapsServices = googleMapsServices;
    }


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddressCoordinates))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public AddressCoordinates GetAddressCoordinates([SwaggerParameter(Description = "Enter address here to get the coordinates")] string address)
    {
        var (latitude, longitude) = _googleMapsServices.GetCoordinatesFromAddress(address);

        var coordinates = new AddressCoordinates 
        { 
            Latitude = latitude, 
            Longitude = longitude 
        };

        return coordinates;
    }
}

