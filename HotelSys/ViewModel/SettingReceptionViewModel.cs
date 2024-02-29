using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelSys.ViewModel
{
    public class SettingReceptionViewModel
    {
		 public int Id { get; set; } // int
		 public bool IsCheckinTime { get; set; } // bit
		 public TimeSpan? TimeCheckin { get; set; } // time(7)
		 public bool IsCheckoutTime { get; set; } // bit
		 public TimeSpan? TimeCheckout { get; set; } // time(7)
		 public bool IsIntervalChangeroom { get; set; } // bit
		 public int? IntervalChangeroom { get; set; } // int
		 public bool IsMounthReceptionCheckout { get; set; } // bit
		 public int IdSub { get; set; } // int

        public bool IncudePriceTax { get; set; }

    }
}
