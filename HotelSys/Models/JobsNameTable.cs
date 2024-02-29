using System;
using System.Collections.Generic;

#nullable disable

namespace HotelSys.Models
{
    public partial class JobsNameTable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? IdSub { get; set; }
    }
}
