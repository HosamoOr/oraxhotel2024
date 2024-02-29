using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelSys.ViewModel
{
    public class DetialsBillsViewModel
    {

		public bool isSelect  { get; set; }
		public long Id { get; set; } // bigint
			 public double? Qty { get; set; } // float
			 public double? PriceOne { get; set; } // float
			 public double? Total { get; set; } // float
			 public long IdBill { get; set; } // bigint
			 public int IdProduct { get; set; } // int

		public string NameProduct { get; set; } // int

		public double? TaxPrice { get; set; } // float
	      public double? TaxRate { get; set; } // float
		 public double? BaladiTaxPrice { get; set; } // float
		 public double? BaladiTaxRate { get; set; } // float
		 public bool? IsBaladiTax { get; set; } // bit
																				   //	public BillsTable Fkbillsdetialsbill { get; set; }

		//public ProductTable Fkproductservicesdetial { get; set; }



	}
}
