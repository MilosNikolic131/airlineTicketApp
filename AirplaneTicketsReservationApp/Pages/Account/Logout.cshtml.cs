using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Session;
using System.Web;

namespace AirplaneTicketsReservationApp.Pages.Account
{
    public class LogoutModel : PageModel
    {

        //public void OnPost()
        //{
        //    HttpContext.SignOutAsync("AirlineCookieAuth");
        //    RedirectToPage("/");
        //}

        public async Task<IActionResult> OnPostAsync()
        {
            HttpContext.SignOutAsync("AirlineCookieAuth");
            //FormsAuthentication.SignOut();
            //Session.Abandon();
            return RedirectToPage("/");
        }
    }
}