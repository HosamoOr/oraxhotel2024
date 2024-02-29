using System.Collections.Generic;

namespace HotelSys.ViewModel
{
    public class UserViewModel
    {
		public string  Id { get; set; } 
	 public string UserName { get; set; } // nvarchar(256)
	// public string NormalizedUserName { get; set; } // nvarchar(256)
	 public string Email { get; set; } // nvarchar(256)
	// public string NormalizedEmail { get; set; } // nvarchar(256)
	//public bool EmailConfirmed { get; set; } // bit
	// public string PasswordHash { get; set; } // nvarchar(max)
	 //public string SecurityStamp { get; set; } // nvarchar(max)
	 //public string ConcurrencyStamp { get; set; } // nvarchar(max)
	 public string PhoneNumber { get; set; } // nvarchar(max)
	//public bool PhoneNumberConfirmed { get; set; } // bit
	//public bool TwoFactorEnabled { get; set; } // bit
	// public DateTimeOffset? LockoutEnd { get; set; } // datetimeoffset(7)
	//public bool LockoutEnabled { get; set; } // bit
	//public int AccessFailedCount { get; set; } // int
	 public string FirstName { get; set; } // nvarchar(100)
	 public string LastName { get; set; } // nvarchar(100)

		public int IdBoxAccDef { get; set; }

		public List<BoxViewModel>  boxUser { get; set; }


	}
}
