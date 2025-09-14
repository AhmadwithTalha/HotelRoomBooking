namespace HotelRoomBooking.DTOs
{
    public record ReservationDTOs(
        int Id,
        int RoomId,
        string GuestName,
        DateTime CheckInDate,
        DateTime CheckOutDate,
        DateTime CreatedAt);

    public record ReservationCreateDTOs(
        int RoomId,
        string GuestName,
        DateTime CheckInDate,
        DateTime CheckOutDate,
        DateTime CreatedAt);
}