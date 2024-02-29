﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelSys.ViewModel
{

	public class BondShortViewModel
	{

		public long id { get; set; } // bigint
		public String txt { get; set; }

		public double amount { get; set; }

		public DateTime date { get; set; }
	}

		public class BondViewModel
    {
		
			 public long Id { get; set; } // bigint

		[Required(ErrorMessage ="الرجاء اختيار نوع السند")]
		[Range(1, int.MaxValue, ErrorMessage = "الرجاء اختيار نوع السند")]
		public string Type { get; set; } // nvarchar(20)

		[Required(ErrorMessage = "الرجاء طريقة الدفع")]
		public string TypePay { get; set; } // nvarchar(20)
			 public string NumReference { get; set; } // nvarchar(100)

		[Required(ErrorMessage = "الرجاء اختيار تاريخ السند")]
		public DateTime Date { get; set; } // datetime

		//[Required(ErrorMessage = "الرجاء وقت السند")]
		public TimeSpan? Time { get; set; } // time(7)

		[Required(ErrorMessage ="الرجاء كتابة المبلغ")]

			 public double Amount { get; set; } // float
			 public string LocPay { get; set; } // nvarchar(300)
			 
			 public DateTime? WorthyDate { get; set; } // datetime
		//[Required(ErrorMessage ="الرجاء كتابة من اجل ")]
		public string Why { get; set; } // nvarchar(max)
			 public string Hand { get; set; } // nvarchar(max)
			 public string NumCheck { get; set; } // nvarchar(50)
			 public string NumCard { get; set; } // nvarchar(50)
			 public string Note { get; set; } // nvarchar(max)
			 public DateTime? Createat { get; set; } // datetime
			 public bool? IsDonePay { get; set; } // bit
			 public long? IdBondPay { get; set; } // bigint


		//[Required(ErrorMessage ="الرجاء اختيار الحساب")]
		//[Range(1, int.MaxValue, ErrorMessage = "الرجاء اختيار الحساب")]
		public int IdAccount { get; set; } // int
			 public long? IdReception { get; set; } // bigint
			 public int? IdItemExpenses { get; set; } // int
			 public int? IdBank { get; set; } // int    
			 public int? IdCurrancy { get; set; }

		[Required(ErrorMessage = "الرجاء اختيار الحساب")]
		[Range(1, int.MaxValue, ErrorMessage = "الرجاء اختيار الحساب")]
		public int id_accountForm { get; set; }


		[Required(ErrorMessage = "الرجاء اختيار الحساب")]
		[Range(1, int.MaxValue, ErrorMessage = "الرجاء اختيار الحساب")]
		public int id_accountTo { get; set; }

		public String  customerOrCompany { get; set; }

		public string nameCustomer { set; get; }

		public string datewithTime { set; get; }


		//public IEnumerable<EntriesAccTable> Entriesacctablebondtables { get; set; }

		//public AccountTable Fkbondaccount { get; set; }

		//public BondTable Fkbondbond { get; set; }

		//	public IEnumerable<BondTable> FkBondBondBackReferences { get; set; }

		//public ItemsExpensesTable Fkbondexpense { get; set; }


	}

	public class ResultBond
	{
		public List<BondViewModel> list { get; set; }

		public int countRow { get; set; }
	}

}

