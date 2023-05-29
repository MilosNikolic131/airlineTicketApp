using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AirplaneTicketsReservationApp.Models;
using System.Data.SqlClient;
using Microsoft.AspNetCore.SignalR;
using AirplaneTicketsReservationApp.Hubs;

namespace AirplaneTicketsReservationApp.Pages.Flights
{
    public class RezervationsModel : PageModel
    {
        public List<Reservation> reservations = new List<Reservation>();
        public readonly IHubContext<ReservationHub> hubContext;

        public RezervationsModel(IHubContext<ReservationHub> _hubContext)
        {
            this.hubContext = _hubContext;
        }

        public async Task NewReservationReceived(Reservation _reservation)
        {
            await hubContext.Clients.All.SendAsync("NewReservationReceived", _reservation);
            reservations.Add(_reservation);
        }

        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=.\\SQLEXPRESS2;Initial Catalog=airlineDB;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM reservation";
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

        //public async Task OnGetAsync()
        //{


            
        //    Reservation reservationAsync = new Reservation();
        //    await this.NewReservationReceived(reservationAsync);

        //    //reservations.Add(reservationAsync);

        //    //return RedirectToPage("/Flights/Rezervations");
        //}
    }
}