using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;

namespace AirplaneTicketsReservationApp.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public userTable userType { get; set; } 
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
           // List<userTable> userList = new List<userTable>();
            if(!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                string connectionString = "Data Source=.\\SQLEXPRESS2;Initial Catalog=airlineDB;Integrated Security=True";
                
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM userTable";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                userTable user = new userTable();
                                user.id = reader.GetInt32(0);
                                user.Name = reader.GetString(1);
                                user.Surname = reader.GetString(2);
                                user.type = Enum.Parse<UserTypeEnum>(reader.GetString(3));
                                user.username = reader.GetString(4);
                                user.password = reader.GetString(5);

                                if(user.username == userType.username && user.password == userType.password && user.type == UserTypeEnum.Agent)
                                {
                                    userType = user;
                                    var claims = new List<Claim> {
                                        new Claim("ID", user.id.ToString()),
                                        //new Claim(ClaimTypes.Name, user.Name),
                                        //new Claim(ClaimTypes.Surname, user.Surname),
                                        new Claim("Type", "Agent")
                                    };
                                    var identity = new ClaimsIdentity(claims, "AirlineCookieAuth");
                                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                                    await HttpContext.SignInAsync("AirlineCookieAuth", claimsPrincipal);

                                    return RedirectToPage("/Index");
                                }

                                if (user.username == userType.username && user.password == userType.password && user.type == UserTypeEnum.Admin)
                                {
                                    userType = user;
                                    var claims = new List<Claim> {
                                        new Claim("ID", user.id.ToString()),
                                        //new Claim(ClaimTypes.Name, user.Name),
                                        //new Claim(ClaimTypes.Surname, user.Surname),
                                        new Claim("Type", "Admin"),
                                        new Claim("Type", "Agent")
                                    };
                                    var identity = new ClaimsIdentity(claims, "AirlineCookieAuth");
                                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                                    await HttpContext.SignInAsync("AirlineCookieAuth", claimsPrincipal);

                                    return RedirectToPage("/Index");
                                }

                                if (user.username == userType.username && user.password == userType.password && user.type == UserTypeEnum.Visitor)
                                {
                                    userType = user;
                                    var claims = new List<Claim> {
                                        new Claim("ID", user.id.ToString()),
                                        //new Claim(ClaimTypes.Name, user.Name),
                                        //new Claim(ClaimTypes.Surname, user.Surname),
                                        new Claim("Type", "Visitor")
                                    };
                                    var identity = new ClaimsIdentity(claims, "AirlineCookieAuth");
                                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                                    await HttpContext.SignInAsync("AirlineCookieAuth", claimsPrincipal);

                                    return RedirectToPage("/Index");
                                }

                                //userList.Add(user);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

            }
            return Page();
        }
    }

    public class userTable
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public UserTypeEnum type { get; set; }

        [Required]
        public string username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }

    }

    public enum UserTypeEnum
    {
        Admin, Visitor, Agent
    }
}