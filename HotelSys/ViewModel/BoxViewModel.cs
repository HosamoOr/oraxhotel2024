namespace HotelSys.ViewModel
{
    public class BoxViewModel
    {
		public int Id { get; set; } // int
		 public string Name { get; set; } // nvarchar(300)
		 public bool IsDefault { get; set; } // bit
		 public int IdAccount { get; set; } // int
		 public int? IdSub { get; set; } // int
		public bool IsPrivate { get; set; } // bit

	}
}
