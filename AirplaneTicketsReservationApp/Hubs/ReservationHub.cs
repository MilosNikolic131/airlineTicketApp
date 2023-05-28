using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using AirplaneTicketsReservationApp.Models;

namespace AirplaneTicketsReservationApp.Hubs
{
    public class ReservationHub: Hub
    {

        public Task SendReservation(string user, Reservation _reservation)
        {
            return Clients.All.SendAsync("ReceiveMessage", user, _reservation);
        }

        public async Task NewReservationReceived(Reservation _reservation)
        {
            await Clients.All.SendAsync("NewReservationReceived", _reservation);
        }
    }
}
