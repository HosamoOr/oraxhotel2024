using DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelSys.ViewModel
{
	public class _AccountViewModel
	{
		public int Id { get; set; }

		[Required]
		[DataType(DataType.Text)]
		public string Name { get; set; }
		[Required]
		public string Status { get; set; }
		public bool? IsPrivate { get; set; }
		public DateTime? Createat { get; set; }
		public int IdGroup { get; set; }

		public int? Code { get; set; }
		public virtual GroupAccountViewModel IdGroupNavigation { get; set; }
    }

	public class _AccountShortViewModel
	{
		public int Id { get; set; }

		public string Name { get; set; }
		
	}


	public partial class GroupAccountViewModel
	{
		 public int Id { get; set; } // int
		[Required]
		[DataType(DataType.Text)]
		public string Name { get; set; } // nvarchar(100)
		 public int? IdMainGroup { get; set; } // int
		 public bool? IsRoot { get; set; } // bit
		 public bool? IsPrivate { get; set; } // bit
		 public int? IdSub { get; set; } // int

		public string? NameMainGroup { get; set; } // int
		//public GroupAccountViewModel Fkgroupaccountgroupaccount { get; set; }

		///// <summary>
		///// fk_group_account_account_BackReference (dbo.account_table)
		///// </summary>
		//[Association(ThisKey = "Id", OtherKey = "IdGroup", CanBeNull = true)]
		//public IEnumerable<AccountTable> Fkgroupaccountaccounts { get; set; }

		///// <summary>
		///// fk_group_account_group_account (dbo.group_account_table)
		///// </summary>
		//[Association(ThisKey = "IdMainGroup", OtherKey = "Id", CanBeNull = true)]
		//public GroupAccountTable Fkgroupaccountgroupaccount { get; set; }

		///// <summary>
		///// fk_group_account_group_account_BackReference (dbo.group_account_table)
		///// </summary>
		//[Association(ThisKey = "Id", OtherKey = "IdMainGroup", CanBeNull = true)]
		//public IEnumerable<GroupAccountTable> FkGroupAccountGroupAccountBackReferences { get; set; }

		//#endregion
	}
	public class ResultAccount
	{
		public List<_AccountViewModel> list { get; set; }

		public int countRow { get; set; }
	}

}
