using System;
using System.Collections.Generic;

#nullable disable

namespace HotelSys.Models
{
    public partial class SettingReceptionTable
    {
        public int Id { get; set; }
        public bool? IsCheckinTime { get; set; }
        public TimeSpan? TimeCheckin { get; set; }
        public bool? IsCheckoutTime { get; set; }
        public TimeSpan? TimeCheckout { get; set; }
        public bool? IsIntervalChangeroom { get; set; }
        public int? IntervalChangeroom { get; set; }
        public bool? IsMounthReceptionCheckout { get; set; }
        public int IdSub { get; set; }
        public bool? IncudePriceTax { get; set; }
    }
}
