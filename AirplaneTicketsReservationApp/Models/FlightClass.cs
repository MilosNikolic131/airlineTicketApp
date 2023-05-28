using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace AirplaneTicketsReservationApp.Models
{
    public class FlightClass
    {
        [Key]
        public int id { get; set; }
        public DateTime departureDate { get; set; }
        public DestinationEnum departure { get; set; }
        public DestinationEnum arrival { get; set; }
        public int numberOfTransfers { get; set; }
        public int numberOfSeats { get; set; }
    }

    public enum DestinationEnum
    {
        Beograd, Nis, Kraljevo, Pristina
    }
}
