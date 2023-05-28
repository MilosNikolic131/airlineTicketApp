using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirplaneTicketsReservationApp.Models
{
    public class Reservation
    {
        public int userId { get; set; }
        public int flightId { get; set; }
        public int numberOfSeats { get; set; }
        public int approved { get; set; }
    }
}
