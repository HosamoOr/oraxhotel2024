using System;
using System.Collections.Generic;

#nullable disable

namespace HotelSys.Models
{
    public partial class PriceRoomsTable
    {
        public int Id { get; set; }
        public double? Price { get; set; }
        public double? PriceOvertime { get; set; }
        public double? PriceMin { get; set; }
        public int? IdSub { get; set; }
        public int IdRoom { get; set; }
        public int? IdTaxGroup { get; set; }

        public virtual RoomsTable IdRoomNavigation { get; set; }
        public virtual TaxGroupTable IdTaxGroupNavigation { get; set; }
    }
}
