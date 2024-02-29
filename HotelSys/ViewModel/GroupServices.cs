using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelSys.ViewModel
{
    public class GroupServicesViewModel
    {
		
			 public int Id { get; set; } // int
			 public string Name { get; set; } // nvarchar(max)
			 public int? IdSub { get; set; } // int
			 public string NameEn { get; set; } // nvarchar(max)

		      public int countBrchService { get; set; } // int

            

        public IEnumerable<ProductViewModel> products { get; set; }

			
	}
}
