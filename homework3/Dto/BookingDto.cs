using System;
using homework3.DataAccess.Entity;

namespace homework3.Dto
{
	public class BookingDto
	{
		public string Username { get; set; }
		public DateTime FromUtc { get; set; }
		public DateTime ToUtc { get; set; }
		public string Comment { get; set; }
		public int NumberRoom { get; set; }

		public Booking ToBooking(int userId, int numberRoom)
		{
			return new Booking
			{
				UserId = userId,
				FromUtc = DateTime.SpecifyKind(FromUtc, DateTimeKind.Utc),
				ToUtc = DateTime.SpecifyKind(ToUtc, DateTimeKind.Utc),
				Comment = Comment,
				NumberRoom = numberRoom
			};
		}

		public static BookingDto FromBooking(Booking booking)
		{
			return new BookingDto
			{
				Comment = booking.Comment,
				FromUtc = booking.FromUtc,
				ToUtc = booking.ToUtc,
				Username = booking.User.UserName,
				NumberRoom = booking.NumberRoom
			};
		}
	}
}
