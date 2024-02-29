using DataModels;
using HotelSys.Accounting_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DataModels.HotelAlkheerDBStoredProcedures;


//using static DataModels.HotelTalal2DBStoredProcedures;

namespace HotelSys.ViewModel
{
    public class LogoutRoomViewModel
    {
		public long IdReception { get; set; }

		public int IdRoom { get; set; }

		public int IdAccount { get; set; }
		public int IdSub { get; set; }

        public BondViewModel bondViewModel { get; set; }
        public String OutOrCancel { get; set; }
    }

    public class SummaryLogOutViewModel
    {
        public List<GetBalanceReceptionResult> item { get; set; }
        public double sumFromPrice { get; set; }
        public double sumToPrice { get; set; }
        public double balance { get; set; }
        public int countDayReception { get; set; }
        public TimingLogViewModel timinglog { get; set; }
        public item_document itemDocument { get; set; }

        
    }

    public class TimingLogViewModel
    {
        public bool isoverflow { get; set; }
        public TimeSpan timeNow { get; set; }
        public TimeSpan? timeDefualt { get; set; }
        public bool isSelectUser { get; set; }

        public int source { get; set; }  //1:from User-- 2: from system defoult

    }
}
