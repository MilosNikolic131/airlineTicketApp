using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AirplaneTicketsReservationApp.Models;
using System.Data.SqlClient;

namespace AirplaneTicketsReservationApp.Pages.Visitor
{
    public class BrowseModel : PageModel
    {
        private readonly ApplicationDBContext _adb;

        public BrowseModel(ApplicationDBContext adb)
        {
            this._adb = adb;
        }

        public List<FlightClass> results = new List<FlightClass>();


        public void OnGet()
        {
            //results = _adb.flight.ToList();

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
                            while (reader.Read())
                            {
                                FlightClass flight = new FlightClass();
                                flight.id = reader.GetInt32(0);
                                flight.departureDate = reader.GetDateTime(1);
                                flight.departure = Enum.Parse<DestinationEnum>(reader.GetString(2));
                                flight.arrival = Enum.Parse<DestinationEnum>(reader.GetString(3));
                                flight.numberOfTransfers = reader.GetInt32(4);
                                flight.numberOfSeats = reader.GetInt32(5);

                                results.Add(flight);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

            }

        }

        public void OnPost(string _from, string _to)
        {
            //DestinationEnum departureFrom = Enum.Parse<DestinationEnum>(_from);
            //DestinationEnum to = Enum.Parse<DestinationEnum>(_to);


            //results = (from x in _adb.flight where (x.departure == departureFrom) && (x.arrival == to) select x).ToList();
            results = new List<FlightClass>();
            try
            {
                string connectionString = "Data Source=.\\SQLEXPRESS2;Initial Catalog=airlineDB;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM flight WHERE departure = '" + _from + "' AND " + "arrival = '" + _to + "'";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                FlightClass flight = new FlightClass();
                                flight.id = reader.GetInt32(0);
                                flight.departureDate = reader.GetDateTime(1);
                                flight.departure = Enum.Parse<DestinationEnum>(reader.GetString(2));
                                flight.arrival = Enum.Parse<DestinationEnum>(reader.GetString(3));
                                flight.numberOfTransfers = reader.GetInt32(4);
                                flight.numberOfSeats = reader.GetInt32(5);

                                results.Add(flight);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

            }
        }
    }
}