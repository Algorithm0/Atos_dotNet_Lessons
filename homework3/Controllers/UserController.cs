using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using homework3.DataAccess;
using homework3.DataAccess.Entity;

namespace homework3.Controllers
{
	[Route("user")]
	public class UserController : Controller
	{
		private readonly BookingContext _bookingContext;

		public UserController(BookingContext bookingContext)
		{
			_bookingContext = bookingContext;
		}

		public async Task<ActionResult<string[]>> GetAllUserNames()
		{
			return await _bookingContext.Users
				.Select(u => u.UserName)
				.ToArrayAsync();
		}

		[HttpPost]
		public async Task<ActionResult<User>> CreateUser([FromBody]User user)
		{
			User userInDb = await _bookingContext
				.Users
				.FirstOrDefaultAsync(u => u.UserName == user.UserName);

			if (userInDb != null)
				return Conflict("User with this name already exists");

			_bookingContext.Users.Add(user);
			await _bookingContext.SaveChangesAsync();

			return Ok(user);
		}
	}
}
