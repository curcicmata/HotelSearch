namespace HotelSearch.Domain.Models.Base;

public class BaseModel
{
    public int Id { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public DateTime? DateModified { get; set; } = null;
}

