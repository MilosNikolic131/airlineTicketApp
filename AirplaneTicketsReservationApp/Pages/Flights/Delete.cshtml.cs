using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace AirplaneTicketsReservationApp.Pages.Flights
{
    [Authorize(Policy = "MustBeAdmin")]
    public class DeleteModel : PageModel
    {
        public void OnGet()
        {

        }
    }
}