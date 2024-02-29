using System;
using System.Collections.Generic;

#nullable disable

namespace HotelSys.Models
{
    public partial class CurrencyTable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool? IsDefault { get; set; }
        public string Code { get; set; }
        public double? RateConvert { get; set; }
        public int? IdSub { get; set; }
    }
}
