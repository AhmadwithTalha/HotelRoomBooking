using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelRoomBooking.Models
{
    public class Room
    {
        [Key]                                       // PK
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string RoomNumber { get; set; } = string.Empty;  // = string.Empty keeps compiler happy

        [MaxLength(30)]
        public string Type { get; set; } = string.Empty;

        [Column(TypeName = "decimal(10,2)")]        // SQL money-friendly
        public decimal PricePerNight { get; set; }

        // Navigation + reverse nav (one room has many reservations)
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}