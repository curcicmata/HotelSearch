using System.ComponentModel;

namespace HotelSearch.Domain.Models.User;

public class AuthenticateRequest
{
    [DefaultValue("System")]
    public required string Username { get; set; }

    [DefaultValue("System")]
    public required string Password { get; set; }
}

