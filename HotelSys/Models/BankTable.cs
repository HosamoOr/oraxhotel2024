using System;
using System.Collections.Generic;

#nullable disable

namespace HotelSys.Models
{
    public partial class BankTable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool? IsDefault { get; set; }
        public int IdAccount { get; set; }
        public int? IdSub { get; set; }

        public virtual AccountTable IdAccountNavigation { get; set; }
    }
}
