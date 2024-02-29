using HotelSys.ViewModel;
using System.Collections.Generic;

namespace HotelSys
{
    public class CustRPTViewModel
    {
        public long IdReception { get; set; }

        public string WhyVisit { get; set; }
        public string AreaFrom { get; set; }

        public CustomerViewModel customer { get; set; }

         public List<FollowerViewModel> followers { get; set; }

       public string followersStr { get; set; }



        public OrgShortViewModel hotel { get; set; }

        public int def { get; set; }

        public string interval { get; set; }

    }
}
