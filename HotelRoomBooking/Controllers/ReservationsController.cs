using HotelRoomBooking.Data;
using HotelRoomBooking.DTOs;
using HotelRoomBooking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelRoomBooking.DTOs;
using Microsoft.EntityFrameworkCore;
namespace HotelRoomBooking.Controllers
{



    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly AppDbContext _ctx;
        public ReservationsController(AppDbContext ctx) => _ctx = ctx;

        //[HttpGet]
        //public async Task<IActionResult> GetReservations([FromQuery] int? roomId,
        //                                                 [FromQuery] DateTime? from,
        //                                                 [FromQuery] DateTime? to)
        //{
        //    var q = _ctx.Reservations.AsQueryable();
        //    if (roomId.HasValue) q = q.Where(r => r.RoomId == roomId);
        //    if (from.HasValue) q = q.Where(r => r.CheckOutDate > from);
        //    if (to.HasValue) q = q.Where(r => r.CheckInDate < to);

        //    var list = await q.Select(r => new ReservationDTOs(
        //                                    r.Id, r.RoomId, r.GuestName,
        //                                    r.CheckInDate, r.CheckOutDate, r.CreatedAt))
        //                      .ToListAsync();
        //    return Ok(list);
        //}

        // GET: api/Reservations/CheckAvailability
        [HttpGet("CheckAvailability")]
        public async Task<IActionResult> CheckAvailability([FromQuery] int roomId,
                                                           [FromQuery] DateTime from,
                                                           [FromQuery] DateTime to)
        {
            // Find any overlapping reservation
            var conflict = await _ctx.Reservations
                .Where(r => r.RoomId == roomId && r.CheckInDate < to && from < r.CheckOutDate)
                .FirstOrDefaultAsync();

            if (conflict != null)
            {
                // Room is booked, return false + existing reservation data
                return Ok(new
                {
                    available = false,
                    reservation = new ReservationDTOs(
                    conflict.Id,
                    conflict.RoomId,
                    conflict.GuestName,
                    conflict.CheckInDate,
                    conflict.CheckOutDate,
                    conflict.CreatedAt
                )
                });
            }

            
            return Ok(new { available = true });
        }


      
        // GET: api/Reservations/all
        [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<ReservationWithRoomDTOs>>> GetAllReservations()
    {
        var list = await _ctx.Reservations
            .Include(r => r.Room)
            .Select(r => new ReservationWithRoomDTOs(
                r.Id,
                r.RoomId,
                r.GuestName,
                r.CheckInDate,
                r.CheckOutDate,
                r.CreatedAt,
                new RoomDTOs(
                    r.Room.Id,
                    r.Room.RoomNumber,
                    r.Room.Type,
                    r.Room.PricePerNight
                )
            ))
            .ToListAsync();

        return Ok(list);
    }




    [HttpPost]
        public async Task<IActionResult> CreateReservation(ReservationDTOs dto)
        {
            if (dto.CheckOutDate <= dto.CheckInDate)
                return BadRequest("Checkout must be after checkin.");

            bool overlap = await _ctx.Reservations
                .AnyAsync(r => r.RoomId == dto.RoomId &&
                               r.CheckInDate < dto.CheckOutDate &&
                               dto.CheckInDate < r.CheckOutDate);

            if (overlap)
                return BadRequest("Room is already booked for those dates.");

            var res = new Reservation
            {
                RoomId = dto.RoomId,
                GuestName = dto.GuestName,
                CheckInDate = dto.CheckInDate,
                CheckOutDate = dto.CheckOutDate,
                CreatedAt = DateTime.UtcNow
            };

            try
            {
                _ctx.Reservations.Add(res);
                await _ctx.SaveChangesAsync();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
            {
               
                return BadRequest(new { error = ex.InnerException?.Message ?? ex.Message });
            }

            return CreatedAtAction(nameof(CheckAvailability), new { id = res.Id }, res);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ExtendReservation(int id, ReservationCreateDTOs dto)
        {
            var reservation = await _ctx.Reservations.FindAsync(id);
            if (reservation == null)
                return NotFound("Reservation not found.");

            // Check if the room is available for the new period
            bool isRoomOccupied = await _ctx.Reservations
                .Where(r => r.RoomId == reservation.RoomId && r.Id != id)
                .AnyAsync(r =>
                    dto.CheckInDate < r.CheckOutDate && dto.CheckOutDate > r.CheckInDate
                );

            if (isRoomOccupied)
                return BadRequest("Cannot extend reservation: room is already booked during this period.");

            // Extend reservation dates
            reservation.CheckInDate = dto.CheckInDate;
            reservation.CheckOutDate = dto.CheckOutDate;

            await _ctx.SaveChangesAsync();
            return Ok(reservation);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var reservation = await _ctx.Reservations.FindAsync(id);
            if (reservation == null)
                return NotFound("Reservation not found.");

            _ctx.Reservations.Remove(reservation);
            await _ctx.SaveChangesAsync();
            return NoContent();
        }



    }
}