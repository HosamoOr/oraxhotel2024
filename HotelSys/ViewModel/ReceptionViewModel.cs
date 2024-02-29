using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelSys.ViewModel
{


	public class EnterRoomViewModel
	{
		public long IdReception { get; set; }

		public long IdBill { get; set; }

		public int IdRoom { get; set; }

		public int? IdAccount { get; set; }
		public int? IdSub { get; set; }

		

		public DateTime? StartDate { get; set; } 

		public DateTime ?EndDate { get; set; } 


	}


	public class ReceptionViewModel
    {
		 public long IdReception { get; set; } 
		 public string Source { get; set; }

		[Required]
		[DataType(DataType.Currency)]
		public double? Price { get; set; }

		[Required]
		public int? QtyTime { get; set; } // int


		 public string Unit { get; set; } // nvarchar(50)

		[Required]
		//[DataType(DataType.Date)]
		public DateTime StartDate { get; set; } // datetime

		[Required]
		//[DataType(DataType.Date)]
		public DateTime EndDate { get; set; } // datetime


		 public char? TypeDate { get; set; } // nvarchar(1)

		 public bool? IsChechin { get; set; } // bit
		 public DateTime? CheckinDate { get; set; } // datetime
		 public bool? IsChechout { get; set; } // bit
		 public DateTime? ChechoutDate { get; set; } // datetime
		 public int? IdCo { get; set; } // int

		public long IdCustomer { get; set; } // bigint

		public string NameCustomer { get; set; } // bigint

	
		public int IDAccountReception { get; set; }

		public int titlePageType { get; set; }  //1:New -2:Edit


		public int IdRoom { get; set; } 

		public double total { get; set; }

		public int? status { get; set; }

		public string WhyVisit { get; set; }
		public string AreaFrom { get; set; }

		public CustomerViewModel customer { get; set; }

		public _CompanyViewModel company { get; set; }

		public List<FollowerViewModel> followers { get; set; }

		public BillReceptionViewModel bill { get; set; }

		public PriceRoomsViewModel room { get; set; }
		public SummaryPriceReceptionViewModel summaryPrice { get; set; }
       

    }


	public class FollowerViewModel
	{
		public long Id { get; set; }

		public CustomerViewModel follwerCusomer { set; get; }
		 public string Relation { get; set; } // nvarchar(50)
		
		 public char? Duration { get; set; } // nvarchar(1)
		 public DateTime? DurationFrom { get; set; } // datetime
		 public DateTime? DurationTo { get; set; } // datetime


		//[Column("id_receptoin"), NotNull] public long IdReceptoin { get; set; } // bigint
		//[Column("id_customer"), NotNull] public long IdCustomer { get; set; } // bigint


	}
	public class ReceptionDT
	{
        public List<ReceptionViewModel> list { get; set; }
		public int rowCount { get; set; }
	}

	}



