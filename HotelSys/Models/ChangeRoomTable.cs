using System;
using System.Collections.Generic;

#nullable disable

namespace HotelSys.Models
{
    public partial class ChangeRoomTable
    {
        public int Id { get; set; }
        public int IdRoomFrom { get; set; }
        public int IdRoomTo { get; set; }
        public string Why { get; set; }
        public DateTime Date { get; set; }
        public double PriceOld { get; set; }
        public double PriceCurrent { get; set; }
        public long IdReceptoin { get; set; }
        public int? IdSub { get; set; }
    }
}
