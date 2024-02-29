using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelSys.ViewModel
{
    public class CustomerViewModel
    {
		
			 public long IdmyCu { get; set; } // bigint

		public long IdcumtomerAll { get; set; } // bigint

		[Required]
		[DataType(DataType.Text)]
		public string Name { get; set; } // nvarchar(max)
			 public string Type { get; set; } // nvarchar(10)
			public string Sex { get; set; } // nvarchar(10)
		[Required]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; } // int

		[Required]
		[DataType(DataType.Text)]
		public string Nationality { get; set; } // nvarchar(50)

		[Required]
		[DataType(DataType.Text)]
		public string TypeWork { get; set; } // nvarchar(100)

		//[Required]
		//[DataType(DataType.Text)]

		public string LocWork { get; set; } // nvarchar(100)
			 public string PhoneWork { get; set; } // nvarchar(100)

		[Required]
		[DataType(DataType.Text)]
		public string TypeProof { get; set; } // nvarchar(30)
		[Required]
		[DataType(DataType.Text)]
		public string NumProof { get; set; } // nvarchar(300)

		[Required]
		[DataType(DataType.Date)]
		public DateTime? ReleaseDate { get; set; } // date
		[Required]
		[DataType(DataType.Date)]
		public DateTime? EndDate { get; set; } // date
		[Required]
		[DataType(DataType.Text)]

		public string LocRelease { get; set; } // nvarchar(300)
			 public DateTime? Createat { get; set; } // datetime
			public string PublicNote { get; set; } // nvarchar(max)
			 public string PrivateNote { get; set; } // nvarchar(max)
			 public int? IdAccount { get; set; } // int
			 public int? IdSub { get; set; } // int

		public bool? is_my { get; set; }

		 public DateTime? VisitEndDate { get; set; }

		public int ? Id_Area { get; set; } 

		public AreaViewModel area { get; set; }

        public int? IdNationality { get; set; }

        

    }

	public class DTCusViewModel
    {
		public List<CustomerViewModel> list { get; set; }
		public int countRow { get; set; }

    }

}

