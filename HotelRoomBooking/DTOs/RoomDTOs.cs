namespace HotelRoomBooking.DTOs
{
    public record RoomDTOs(int Id, string RoomNumber, string Type, decimal PricePerNight);
    public record RoomCreateDTOs(string RoomNumber, string Type, decimal PricePerNight);
}