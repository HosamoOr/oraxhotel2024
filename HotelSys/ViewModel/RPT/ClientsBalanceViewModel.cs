using System.Collections.Generic;

namespace HotelSys.ViewModel.RPT
{
	public class ItemClientsBalance
    {
		public int def { get; set; }
		public long idCustomer { get; set; }
		public string name { get; set; }
		public int? id_account { get; set; }
		public double? FromPrice { get; set; }
		public double? ToPrice { get; set; }
		public string messag { get; set; }
		public double? balance { get; set; }

		public OrgShortViewModel hotel { get; set; }
	}


	public class ClientsBalanceViewModel
    {
		public List<ItemClientsBalance> items { get; set; }

		//public OrgViewModel hotel { get; set; }

		public int count { get; set; }	


	}
}
