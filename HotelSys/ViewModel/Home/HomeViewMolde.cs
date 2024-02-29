using System;
using System.Collections.Generic;

namespace HotelSys.ViewModel.Home
{
    public class HomeViewModel
    {
        public String newRecord { set; get; }

        public String totalInvBoy{ set; get; } // جمالي المبيعات لليوم

        public List<SimpleReportViewModel> dountData { set; get; }

        public List<SimpleReportViewModel> barData { set; get; }

        public List<SimpleReportViewModel> pieData { set; get; }
    }
}
