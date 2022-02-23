using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace homework3.DataAccess.Entity
{
    public class Room
    {
        public Room() 
        {
            Bookings = new HashSet<Booking>();
        }
        
        [Key]
        public int Number { get; set; }
        public bool TwoPlaces { get; set; }
        public int Floor { get; set; }
        
        public ICollection<Booking> Bookings { get; set; }
    }
}