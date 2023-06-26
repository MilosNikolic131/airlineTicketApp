using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AirplaneTicketsReservationApp.Models;
using System.Data.SqlClient;

namespace AirplaneTicketsReservationApp.Pages.Visitor
{
    public class VisitorPageModel : PageModel
    {
        public List<Reservation> reservations = new List<Reservation>();

        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=.\\SQLEXPRESS2;Initial Catalog=airlineDB;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string userId = HttpContext.User.FindFirst("ID")?.Value;
                    String sql = "SELECT * FROM reservation WHERE idUser = " + userId;
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Reservation reservation = new Reservation();
                                reservation.flightId = reader.GetInt32(0);
                                reservation.userId = reader.GetInt32(1);
                                reservation.numberOfSeats = reader.GetInt32(2);
                                reservation.approved = reader.GetInt32(3);

                                reservations.Add(reservation);
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