using System;
using System.Collections.Generic;

#nullable disable

namespace HotelSys.Models
{
    public partial class DetialsHotelTable
    {
        public int Id { get; set; }
        public double? CountRoom { get; set; }
        public double? CountFloot { get; set; }
        public int IdHo { get; set; }

        public virtual HotelsBranchTable IdHoNavigation { get; set; }
    }
}
