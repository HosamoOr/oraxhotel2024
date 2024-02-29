using DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelSys.ViewModel
{
    public class RoomViewModel
    {
		
			 public int IdR { get; set; } // int
			 public string NameR { get; set; } // nvarchar(50)
			 public string NumFloor { get; set; } // nvarchar(50)
			 public int? CountRooms { get; set; } // int
			 public int? CountBedSingle { get; set; } // int
			 public int? CountBedDouble { get; set; } // int
			 public int? CountBathroom { get; set; } // int
			public int? CountTv { get; set; } // int
			 public int? CountWallet { get; set; } // int
			 public string TypeCondition { get; set; } // nvarchar(50)
			 public string PublicFeatures { get; set; } // nvarchar(max)
			 public string PrivateFeatures { get; set; } // nvarchar(max)
			public string Note { get; set; } // nvarchar(max)
			 public int? IdType { get; set; } // int
			 public int IdHo { get; set; } // int

		//public PriceRoomsViewModel priceModel;


		public TypeRoomsTable Roomstabletyperoomstable { get; set; }

		//public IEnumerable<StatusCurrentTable> Fkdetialsstatusrooms { get; set; }


		//public HotelsTable Fkroomshotel { get; set; }


		//public IEnumerable<PriceRoomsTable> Fkroomspricerooms { get; set; }





	}

	public partial class PriceRoomsViewModel
	{
		 public int Id { get; set; } // int

	
		[Required]
		[DataType(DataType.Currency)]
		public double? Price { get; set; } // float

		[Required]
		//[DataType(DataType.Currency)]
		public double? PriceOvertime { get; set; } // float
		[Required]
		//[DataType(DataType.Currency)]
		public double? PriceMin { get; set; } // float
		 public int? IdSub { get; set; } // int
		 public int IdRoom { get; set; } // int

		[Required]
		public string NameRoom { get; set; } // nvarchar(50)
		public string NumFloor { get; set; } // nvarchar(50)
		public int? CountRooms { get; set; } // int
		public string NameType { get; set; }


        public int? IdTaxGroup { get; set; } // int


        public TaxGroupViewModel TaxGroup { get; set; }



    }





}
