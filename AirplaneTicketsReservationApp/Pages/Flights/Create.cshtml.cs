using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;

namespace AirplaneTicketsReservationApp.Pages.Flights
{

    [Authorize(Policy = "MustBeAgent")]
    public class CreateModel : PageModel
    {
        public Flight flight = new Flight();
        public string errorMessage = "";
        public void OnGet()
        {

        }

        public void OnPost()
        {
            flight.arrival = Enum.Parse<DestinationEnum>(Request.Form["arrival"]);
            flight.departure = Enum.Parse<DestinationEnum>(Request.Form["departure"]);
            flight.departureDate = DateTime.Parse(Request.Form["departureDate"]);
            flight.numberOfSeats = Int32.Parse(Request.Form["numberOfSeats"]);
            flight.numberOfTransfers = Int32.Parse(Request.Form["numberOfTransfers"]);




            int maxId = -1;

            string connectionString = "Data Source=.\\SQLEXPRESS2;Initial Catalog=airlineDB;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();

                String sql3 = "SELECT * from flight where arrival ='" + flight.arrival + "' AND departure='" + flight.departure + "' AND flightDate = '" + flight.departureDate + "'";
                //using (SqlCommand command3 = new SqlCommand(sql3, connection))
                //{
                //    using (SqlDataReader reader = command3.ExecuteReader())
                //    {
                //        while (reader.Read())
                //        {
                //            DestinationEnum departure = Enum.Parse<DestinationEnum>(reader.GetString(2));
                //            DestinationEnum arrival = Enum.Parse<DestinationEnum>(reader.GetString(3));
                //            DateTime flightDate = reader.GetDateTime(1);

                //            if(flight.departure == departure && flight.arrival == arrival && 
                //                flight.departureDate == flightDate)
                //            {

                //                return;
                //            }
                //        }

                //    }

                    String sql = "SELECT max(id) FROM flight";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                maxId = reader.GetInt32(0);
                            }
                        }

                    }
                    flight.id = ++maxId;

                    try
                    {
                        String sql2 = "INSERT INTO flight " +
                            "(id, departureDate, departure, arrival, numberOfTransfers, numberOfSeats)"
                            + " VALUES " + "(@id, @departureDate, @departure, @arrival, @numberOfTransfers, @numberOfSeats);";
                        using (SqlCommand command2 = new SqlCommand(sql2, connection))
                        {
                            command2.Parameters.AddWithValue("@id", flight.id);
                            command2.Parameters.AddWithValue("@departureDate", flight.departureDate);
                            command2.Parameters.AddWithValue("@departure", flight.departure);
                            command2.Parameters.AddWithValue("@arrival", flight.arrival);
                            command2.Parameters.AddWithValue("@numberOfTransfers", flight.numberOfTransfers);
                            command2.Parameters.AddWithValue("@numberOfSeats", flight.numberOfSeats);

                            command2.ExecuteNonQuery();
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        return;
                    }


                }



                //flight.arrival = "";
                //flight.departure = "";
                flight.departureDate = new DateTime(2023, 05, 20, 0, 0, 0);
                flight.numberOfSeats = 0;
                flight.numberOfTransfers = 0;
                flight.id = 0;

                Response.Redirect("/Flights/flights");
            }
        }
    }