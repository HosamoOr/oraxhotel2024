using System;
using System.Collections.Generic;

namespace HotelSys.ViewModel
{
    public class ServicesReceptionViewModel
    {
       public List<GroupServicesViewModel> dataGroup { get; set; }
       
        public bool ServicesIncludeTax { get; set; }

       public String txtIncludeTax { get; set; }
    }
}
