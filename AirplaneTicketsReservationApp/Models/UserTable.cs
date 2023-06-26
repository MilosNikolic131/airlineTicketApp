using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AirplaneTicketsReservationApp.Models
{
    public class UserTable
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
