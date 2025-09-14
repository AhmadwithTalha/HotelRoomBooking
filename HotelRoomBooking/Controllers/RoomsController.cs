using HotelRoomBooking.Data;
using HotelRoomBooking.DTOs;
using HotelRoomBooking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelRoomBooking.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        private readonly AppDbContext _ctx;
        public RoomsController(AppDbContext ctx) => _ctx = ctx;

        [HttpGet]
        public async Task<IActionResult> GetRooms()
        {
            var rooms = await _ctx.Rooms
                                  .Select(r => new RoomDTOs(r.Id, r.RoomNumber, r.Type, r.PricePerNight))
                                  .ToListAsync();
            return Ok(rooms);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoom(RoomDTOs dto)
        {
            if (await _ctx.Rooms.AnyAsync(r => r.RoomNumber == dto.RoomNumber))
                return BadRequest("Room number must be unique.");

            var room = new Room
            {
                RoomNumber = dto.RoomNumber,
                Type = dto.Type,
                PricePerNight = dto.PricePerNight
            };

            _ctx.Rooms.Add(room);
            await _ctx.SaveChangesAsync();
            return CreatedAtAction(nameof(GetRooms), new { id = room.Id }, room);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoom(int id, RoomDTOs dto)
        {
            var room = await _ctx.Rooms.FindAsync(id);
            if (room == null)
                return NotFound("Room not found.");

            // Update properties
            room.RoomNumber = dto.RoomNumber;
            room.Type = dto.Type;
            room.PricePerNight = dto.PricePerNight;

            await _ctx.SaveChangesAsync();
            return Ok(room);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var room = await _ctx.Rooms.FindAsync(id);
            if (room == null)
                return NotFound("Room not found.");

            // Check if any reservations exist for this room
            bool hasReservations = await _ctx.Reservations.AnyAsync(r => r.RoomId == id);
            if (hasReservations)
                return BadRequest("Cannot delete room with existing reservations.");

            _ctx.Rooms.Remove(room);
            await _ctx.SaveChangesAsync();
            return NoContent();
        }

    }
}