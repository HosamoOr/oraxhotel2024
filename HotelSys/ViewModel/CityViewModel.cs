using System;
using System.Collections.Generic;

namespace HotelSys.ViewModel
{
	public class CityViewModel
	{

		public int Id { get; set; } // int
		public string Name { get; set; } // nvarchar(max)

		public int countArea { get; set; }
		public string NameCountry { get; set; }
		public int IdCountry { get; set; }
		public List<AreaViewModel> areas { get; set; }
	}


	public class DTCityViewModel
	{
		public List<CityViewModel> list { get; set; }
		public int countRow { get; set; }

	}

}
