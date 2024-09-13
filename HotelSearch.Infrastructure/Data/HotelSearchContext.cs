using HotelSearch.Domain.Models;
using HotelSearch.Domain.Models.User;
using Microsoft.EntityFrameworkCore;

namespace HotelSearch.Infrastructure.Data;

public class HotelContext : DbContext
{
    public HotelContext(DbContextOptions<HotelContext> options) : base(options) { }


    #region Data sets
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<User> Users { get; set; }

    #endregion


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasData(
                new User
                {
                    Id = 1,
                    FirstName = "System",
                    LastName = "",
                    Username = "System",
                    Password = "System",
                }
            );
    }
}

