using HotelRoomBooking.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelRoomBooking.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Room> Rooms { get; set; } = null!;
        public DbSet<Reservation> Reservations { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder b)
        {
            // Unique room number
            b.Entity<Room>()
                .HasIndex(r => r.RoomNumber)
                .IsUnique();

            // Prevent cascade delete
            b.Entity<Reservation>()
                .HasOne(r => r.Room)
                .WithMany(room => room.Reservations)
                .HasForeignKey(r => r.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed data (IDs hard-coded for HasData)
            b.Entity<Room>().HasData(
                new Room { Id = 1, RoomNumber = "101", Type = "Single", PricePerNight = 50.00m },
                new Room { Id = 2, RoomNumber = "102", Type = "Double", PricePerNight = 80.00m },
                new Room { Id = 3, RoomNumber = "201", Type = "Suite", PricePerNight = 150.00m },
                new Room { Id = 4, RoomNumber = "202", Type = "Single", PricePerNight = 55.00m },
                new Room { Id = 5, RoomNumber = "301", Type = "Double", PricePerNight = 90.00m }
            );
        }
    }
}