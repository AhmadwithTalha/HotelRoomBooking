using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelRoomBooking.Migrations
{
    /// <inheritdoc />
    public partial class SeedRooms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1,
                column: "PricePerNight",
                value: 50.00m);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2,
                column: "PricePerNight",
                value: 80.00m);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3,
                column: "PricePerNight",
                value: 150.00m);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "PricePerNight", "Type" },
                values: new object[] { 55.00m, "Single" });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "PricePerNight", "RoomNumber", "Type" },
                values: new object[] { 5, 90.00m, "301", "Double" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1,
                column: "PricePerNight",
                value: 45.50m);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2,
                column: "PricePerNight",
                value: 65.00m);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3,
                column: "PricePerNight",
                value: 120.00m);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "PricePerNight", "Type" },
                values: new object[] { 75.00m, "Double" });
        }
    }
}
