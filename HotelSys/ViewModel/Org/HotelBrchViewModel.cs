using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace HotelSys.ViewModel
{

	public class HotelBrchShortViewModel
	{
		
		public int Id { get; set; } 

		
		public string NameH { get; set; } 
		public string NumEn { get; set; } 
		public string Country { get; set; } 
		public string City { get; set; } 
		public string Regin { get; set; } 
		public string Address { get; set; }

		public string Phone { get; set; } 
		public string Website { get; set; }

		public string Logo { get; set; }

		public int? IdCountry { get; set; }

		public  int ? countFloot { get; set; }
		public int? count_Room { get; set;}

	}
		public class HotelBrchViewModel
    {
        //[Required(ErrorMessage = "الرجاء اختيار شعار الوحدة السكنية-الفندق")]
        [Display(Name = "الصورة")]
        public IFormFile ThumbnailImage { get; set; }

       
		 public int Id { get; set; } // int

		[Required(ErrorMessage = "اسم الفرع")]
		[Display(Name = "اسم الفرع")]
		public string NameH { get; set; } // nvarchar(50)
		 public string NumEn { get; set; } // nvarchar(50)
		 public string Country { get; set; } // nvarchar(50)
		 public string City { get; set; } // nvarchar(50)
		 public string Regin { get; set; } // nvarchar(50)
		 public string Address { get; set; } // nvarchar(150)
		[Display(Name = "البريد الالكتروني")]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; } // nvarchar(max)

		[Required(ErrorMessage = "رقم الهاتف ")]
		[Display(Name = "رقم الهاتف ")]
		[DataType(DataType.PhoneNumber)]
		public string Phone { get; set; } // nvarchar(max)
		 public string Website { get; set; } // nvarchar(max)
		 public string MailBox { get; set; } // nvarchar(150)
		 public string Logo { get; set; } // nvarchar(max)
		 public int? IdSub { get; set; } // int

		public int? IdCountry { get; set; }

        public int? countFloot { get; set; }
        public int? count_Room { get; set; }

        public int? count_users { get; set; }
    }
}
