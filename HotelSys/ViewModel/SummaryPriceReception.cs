using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelSys.ViewModel
{
    public class SummaryPriceReceptionViewModel
    {
        public double sum_Pay { set; get; }

        public double back_reception { set; get; }
        public double sum_Ex { set; get; }

        public double sum_services { set; get; }

        public double total { set; get; }
        public double final_total { set; get; }
        public double balancer { set; get; }


        public double total_befor_VAT { set; get; }
        public double totalVAT { set; get; }
        public double totalBALADI { set; get; }
    }
}
