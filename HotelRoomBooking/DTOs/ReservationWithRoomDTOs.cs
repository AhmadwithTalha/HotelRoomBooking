
using System;

namespace HotelRoomBooking.DTOs
{
    public record ReservationWithRoomDTOs(
        int Id,
        int RoomId,
        string GuestName,
        DateTime CheckInDate,
        DateTime CheckOutDate,
        DateTime CreatedAt,
        RoomDTOs Room
    );
}
