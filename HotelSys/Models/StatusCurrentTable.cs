using System;
using System.Collections.Generic;

#nullable disable

namespace HotelSys.Models
{
    public partial class StatusCurrentTable
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public int IdRoom { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public long? IdReception { get; set; }
        public int? IdEmp { get; set; }
        public DateTime? Createat { get; set; }
        public string Detials { get; set; }

        public virtual RoomsTable IdRoomNavigation { get; set; }
    }
}
