using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelSys.ViewModel
{
    public class ProductViewModel
    {
			 public int Id { get; set; } // int

		
		public string Name { get; set; } // nvarchar(max)
			 public int IdGroup { get; set; } // int
			 public string NameEn { get; set; } // nvarchar(max)
			 public double Price { get; set; } // float

        public int? IdTaxGroup { get; set; } // int

        public String? nameTaxGroup { get; set; } // int

        public double ? tax_rate { get; set; }

        public double? tax_price { get; set; }

        public double? baladi_rate { get; set; }

        public double? baladi_price { get; set; }

        public bool isBaladi { set; get; }



        //	public GroupServicesViewModel ParentService { get; set; }


        //public IEnumerable<DetialsBillsTable> Fkproductservicesdetials { get; set; }


    }
}
