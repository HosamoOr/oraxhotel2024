using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelSys.ViewModel
{

	public class Status_Current_RoomViewModel
	{

		public int Id { get; set; } // int
		public string Status { get; set; } // nvarchar(50)

		public string color { get; set; }

		public int IdRoom { get; set; } // int
		public DateTime? StartDate { get; set; } // datetime
		public string Detials { get; set; } // nvarchar(max)
		public DateTime? EndDate { get; set; } // datetime
		public long? IdReception { get; set; } // bigint
		public int? IdEmp { get; set; } // int
		public DateTime? Createat { get; set; } // datetime


		public RoomViewModel RoomModel { get; set; }

		public double balance { get; set; }

		public String nameCuOrCo { get; set; }

		public string str_date_logout { get; set; }
		//عدد الايام الباقية
        public string qty_left { get; set; }

    }


	public class Status_Current_WithBalanceViewModel
	{

		public int Id { get; set; } // int
		public string Status { get; set; } // nvarchar(50)

		public string color { get; set; }

		public int IdRoom { get; set; } // int
		public DateTime? StartDate { get; set; } // datetime
		public string Detials { get; set; } // nvarchar(max)
		public DateTime? EndDate { get; set; } // datetime
		public long? IdReception { get; set; } // bigint
		public int? IdEmp { get; set; } // int
		public DateTime? Createat { get; set; } // datetime


		public String nameRoom { get; set; }

		public double balance { get; set; }

		public String nameCuOrCo { get; set; }

	}

    public class ListStatusDT
	{
		public List<Status_Current_RoomViewModel> lisy { set; get; }
		public int countRow { get; set; }

	}

}
