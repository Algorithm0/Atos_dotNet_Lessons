using System;
using System.Collections.Generic;

namespace homework3.DataAccess.Entity
{
	public class Booking
	{
		public Booking() 
		{
			Rooms = new HashSet<Room>();
		}
		
		public int Id { get; set; }
		public string Comment { get; set; }
		public DateTime FromUtc { get; set; }
		public DateTime ToUtc { get; set; }
		
		public int UserId { get; set; }
		public User User { get; set; }
		// public int NumberRoom { get; set; }
		// public Room Room { get; set; }
		
		public ICollection<Room> Rooms { get; set; }
	}
}
