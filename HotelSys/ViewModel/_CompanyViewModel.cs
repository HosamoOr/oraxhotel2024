using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelSys.ViewModel
{
    public class _CompanyViewModel
	{
        
		 public int ?IdCo { get; set; }
		[Required]
		[DataType(DataType.Text)]
		public string NameCo { get; set; } // nvarchar(50)
		 public int? IdAccountCo { get; set; } 
		 public int? IdSub { get; set; } 

		
		//public AccountTable Fkcompanyaccount { get; set; }

		//public IEnumerable<RecetionTable> Fkcompanyrecetions { get; set; }

		
	
}
}
