using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace AirplaneTicketsReservationApp.Pages.Flights
{
    [Authorize(Policy = "MustBeAgent")]
    public class flightsModel : PageModel
    {
        public List<Flight> flightList = new List<Flight>();
        public string userType;

        public void OnGet()
        {
            userType = HttpContext.User.FindFirst("Type")?.Value;
            try
            {
                string connectionString = "Data Source=.\\SQLEXPRESS2;Initial Catalog=airlineDB;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM flight";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                Flight flight = new Flight();
                                flight.id = reader.GetInt32(0);
                                flight.departureDate = reader.GetDateTime(1);
                                flight.departure = Enum.Parse<DestinationEnum>(reader.GetString(2));
                                flight.arrival = Enum.Parse<DestinationEnum>(reader.GetString(3));
                                flight.numberOfTransfers = reader.GetInt32(4);
                                flight.numberOfSeats = reader.GetInt32(5);

                                flightList.Add(flight);
                            }
                        }

                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());

            }
        }
    }

    public class Flight
    {
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