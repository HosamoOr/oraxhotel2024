using System;
using System.Collections.Generic;

#nullable disable

namespace HotelSys.Models
{
    public partial class UserTable
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public int IdSub { get; set; }
    }
}
