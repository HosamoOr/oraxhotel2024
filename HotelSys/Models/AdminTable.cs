using System;
using System.Collections.Generic;

#nullable disable

namespace HotelSys.Models
{
    public partial class AdminTable
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool? Status { get; set; }
        public DateTime? LastdateLogin { get; set; }
        public long? Adminid { get; set; }
    }
}
