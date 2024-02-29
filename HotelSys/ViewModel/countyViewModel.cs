using System.Collections.Generic;

namespace HotelSys.ViewModel
{
    public class CountyViewModel
    {
		 public int Id { get; set; } 
	    public string Name { get; set; } 
		public string NameEn { get; set; } 
		public int countCity { get; set; }	

		public List<CityViewModel> Citys { get; set; }
	}
}
