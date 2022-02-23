using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using homework3.DataAccess;
using homework3.DataAccess.Entity;

namespace homework3.Controllers
{
    [Route("room")]
    public class RoomController : Controller
    {
        private readonly BookingContext _bookingContext;

        public RoomController(BookingContext bookingContext)
        {
            _bookingContext = bookingContext;
        }

        [HttpPost]
        public async Task<ActionResult<Room>> AddRoom([FromBody]Room room)
        {
            Room roomInDb = await _bookingContext
                .Rooms
                .FirstOrDefaultAsync(r => r.NumberRoom == room.NumberRoom);

            if (roomInDb != null)
                return Conflict("A room with this number already exists");

            _bookingContext.Rooms.Add(room);
            await _bookingContext.SaveChangesAsync();

            return Ok(room);
        }
    }
}