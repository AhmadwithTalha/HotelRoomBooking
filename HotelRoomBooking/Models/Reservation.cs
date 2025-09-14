using System.ComponentModel.DataAnnotations;

namespace HotelRoomBooking.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; } = null!;

        [MaxLength(100)]
        public string GuestName { get; set; } = string.Empty;

        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}