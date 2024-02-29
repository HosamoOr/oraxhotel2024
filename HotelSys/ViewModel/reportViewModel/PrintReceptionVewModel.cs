using DataModels;
using System.Collections.Generic;

namespace HotelSys.ViewModel
{
    public class PrintReceptionVewModel
    {
      public  ReceptionViewModel receptionData { get; set; }
       public OrgViewModel hotelData { get; set; }
        public List<ConditionReceptionTable> conditions { set; get; }

    



    }
}
