using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AirplaneTicketsReservationApp.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data.SqlClient;

namespace AirplaneTicketsReservationApp.Pages.Admin
{

    [Authorize(Policy = "MustBeAdmin")]
    public class AdminPageModel : PageModel
    {
        public UserTable userTable = new UserTable();

        public void OnGet()
        {

        }

        public void OnPost()
        {
            userTable.type = Enum.Parse<UserTypeEnum>(Request.Form["Type"]);
            userTable.Name = Request.Form["name"];
            userTable.Surname = Request.Form["surname"];
            userTable.username = Request.Form["username"];
            userTable.password = Request.Form["password"];

            int maxId = -1;

            string connectionString = "Data Source=.\\SQLEXPRESS2;Initial Catalog=airlineDB;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();

                String sql = "SELECT max(id) FROM userTable";
                
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
                userTable.id = ++maxId;

                try
                {
                    String sql2 = "INSERT INTO userTable " +
                        "(id, givenName, surname, type, username, password)"
                        + " VALUES " + "(@id, @givenName, @surname, @type, @username, @password);";
                    using (SqlCommand command2 = new SqlCommand(sql2, connection))
                    {
                        command2.Parameters.AddWithValue("@id", userTable.id);
                        command2.Parameters.AddWithValue("@givenName", userTable.Name);
                        command2.Parameters.AddWithValue("@surname", userTable.Surname);
                        command2.Parameters.AddWithValue("@type", userTable.type.ToString());
                        command2.Parameters.AddWithValue("@username", userTable.username);
                        command2.Parameters.AddWithValue("@password", userTable.password);

                        command2.ExecuteNonQuery();
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return;
                }


            }

            userTable.Name = "";
            userTable.Surname = "";
            userTable.username = "";
            userTable.password = "";

            Response.Redirect("/");
        }
    }
}
