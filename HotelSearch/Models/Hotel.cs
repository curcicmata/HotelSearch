using HotelSearch.Domain.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace HotelSearch.Domain.Models;

public class Hotel : BaseModel
{
    [Required]
    public string Name { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Required]
    public double Latitude { get; set; }
    [Required]
    public double Longitude { get; set; }
}