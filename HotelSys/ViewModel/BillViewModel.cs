using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelSys.ViewModel
{
    public class BillReceptionViewModel
    {
		 public long Id { get; set; } // bigint
		public string Type { get; set; } // nvarchar(20)
		public string TypePay { get; set; } // nvarchar(20)
		public string NumReference { get; set; } // nvarchar(100)
		public double? Total { get; set; } // float
		 public DateTime Date { get; set; } // datetime
		
		 public double? DeserveAmount { get; set; } // float
		 public string TypeDiscount { get; set; } // nvarchar(50)
		 public double? QtyDiscount { get; set; } // float
		public double? QtyDiscountRate { get; set; } // float
		public string Note { get; set; } // nvarchar(max)
		 public DateTime? Createat { get; set; } // datetime
		 public int IdAccount { get; set; } // int
		
		 public string CustomerOrCompany { get; set; } // nvarchar(50)
		public long? IdReception { get; set; } // bigint

		public int? IdCurrancy { get; set; }
        public double? TotalTaxPrice     { get; set; } // float
     public double? TotalTaxRate { get; set; } // float
     public bool IncludeTax { get; set; } // bit

        public double? TotalBaladiTaxPrice { get; set; } // float
       public double? TotalBaladiTaxRate { get; set; } // float
       public bool IsBaladiTax { get; set; } // bit


    }

    public class BillViewModel
	{
		
		public long Id { get; set; } // bigint

		public string Type { get; set; } // nvarchar(20)
		 public string TypePay { get; set; } // nvarchar(20)
		 public string NumReference { get; set; } // nvarchar(100)
		 public DateTime Date { get; set; } // datetime
		 public double? Total { get; set; } // float
		 public bool IsForRoom { get; set; } // bit
		 public double? DeserveAmount { get; set; } // float
		 public string TypeDiscount { get; set; } // nvarchar(50)
		 public double? QtyDiscount { get; set; } // float

		public double? QtyDiscountRate {

			set
			{
				QtyDiscountRate = value;
			}

			get {
				if(QtyDiscount == null)
                {
					return null;
                }
				else
                {
					return QtyDiscount/Total*100;

				}


			}  } // float

		
		  public double? PayAmount { get; set; } // float
		 public double? RestAmount { get; set; } // float
		 public string NumCheck { get; set; } // nvarchar(50)
		 public string NumCard { get; set; } // nvarchar(50)
		 public string Note { get; set; } // nvarchar(max)
		 public DateTime? Createat { get; set; } // datetime
		 public int IdAccount { get; set; } // int
		 public long? IdReception { get; set; } // bigint
		 public int? IdBank { get; set; } // int 

		public int? IdCurrancy { get; set; }
		public string CustomerOrCompany { get; set; } // nvarchar(50)

		public List<DetialsBillsViewModel> Items { get; set; }


		public int id_accountForm { get; set; } // int

		public int id_accountTo { get; set; } // int

		public bool IncludeTax { get; set; } // bit
		public double? TotalTaxPrice { get; set; } // float
        public double? TotalTaxRate { get; set; } // float
      
		public double? TotalBaladiTaxPrice { get; set; } // float
		public double? TotalBaladiTaxRate { get; set; } // float
		public bool IsBaladiTax { get; set; } // bit
		//---- for Print
		public String nameCustomer { get; set; }
		public String? room { get; set; } 
		public double? TotalBeforVAT { get; set; }

		public int countPro { set; get; }
		//public IEnumerable<EntriesAccTable> Entriesacctablebillstables { get; set; }
		//public AccountTable Fkbillsaccount { get; set; }

		//public IEnumerable<DetialsBillsTable> Fkbillsdetialsbills { get; set; }
		//public RecetionTable Fkbillsreception { get; set; }



	}


	public class ResultBills
	{
		public List<BillViewModel> list { get; set; }

		public int countRow { get; set; }
	}



}
