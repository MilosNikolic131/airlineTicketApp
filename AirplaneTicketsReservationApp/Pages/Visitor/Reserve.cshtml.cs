using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AirplaneTicketsReservationApp.Models;
using System.Data.SqlClient;
using AirplaneTicketsReservationApp.Pages.Flights;
using Microsoft.AspNetCore.SignalR;
using AirplaneTicketsReservationApp.Hubs;

namespace AirplaneTicketsReservationApp.Pages.Visitor
{
    public class ReserveModel : PageModel
    {

        public Reservation reservation = new Reservation();
        public IHubContext<ReservationHub> hubContext;

        public ReserveModel(IHubContext<ReservationHub> _hubContext)
        {
            this.hubContext = _hubContext;
        }

        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                string connectionString = "Data Source=.\\SQLEXPRESS2;Initial Catalog=airlineDB;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    try
                    {
                        String sql2 = "SELECT * FROM flight";
                        using (SqlCommand command2 = new SqlCommand(sql2, connection))
                        {
                            using (SqlDataReader reader = command2.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    reservation.flightId = reader.GetInt32(0);
                                  
                                }
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        return;
                    }

                }

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return;
            }

            
        }

        public async Task OnPostAsync()
        {
            reservation.flightId = int.Parse(Request.Form["flightId"]);
            reservation.userId = int.Parse(Request.Form["userId"]);
            reservation.numberOfSeats = int.Parse(Request.Form["numberOfSeats"]);
            reservation.approved = int.Parse("0");

            string connectionString = "Data Source=.\\SQLEXPRESS2;Initial Catalog=airlineDB;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    String sql2 = "INSERT INTO reservation " +
                        "(idFlight, idUser, numberOfSeats, approved)"
                        + " VALUES " + "(@idFlight, @idUser, @numberOfSeats, @approved);";
                    using (SqlCommand command2 = new SqlCommand(sql2, connection))
                    {
                        command2.Parameters.AddWithValue("@idFlight", reservation.flightId);
                        command2.Parameters.AddWithValue("@idUser", reservation.userId);
                        command2.Parameters.AddWithValue("@numberOfSeats", reservation.numberOfSeats);
                        command2.Parameters.AddWithValue("@approved", reservation.approved);


                        command2.ExecuteNonQuery();
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return;
                }

            }

           await hubContext.Clients.All.SendAsync("NewReservationReceived", reservation);

        }
    }
}
