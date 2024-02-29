using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelSys.ViewModel
{
    public class HomeReceptionViewModel
    {
        public int countAll { set; get; }
        public int countEmpty { set; get; }
        public int countClean { set; get; }
        public int countRepair { set; get; }
        public int countReservation_without_entry { set; get; }
        public int countBusy { set; get; }

        public List<Status_Current_RoomViewModel> RoomsStatusItems { set; get; }
    }
}
