using System;
using System.Collections.Generic;

#nullable disable

namespace HotelSys.Models
{
    public partial class RecetionTable
    {
        public RecetionTable()
        {
            BillsTables = new HashSet<BillsTable>();
            FollowerReceptionTables = new HashSet<FollowerReceptionTable>();
        }

        public long Id { get; set; }
        public string Source { get; set; }
        public double? Price { get; set; }
        public int? QtyTime { get; set; }
        public string Unit { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string TypeDate { get; set; }
        public bool? IsChechin { get; set; }
        public DateTime? CheckinDate { get; set; }
        public bool? IsChechout { get; set; }
        public DateTime? ChechoutDate { get; set; }
        public int IdRoom { get; set; }
        public string Note { get; set; }
        public int? IdCo { get; set; }
        public long? IdMyCustomer { get; set; }
        public int? Status { get; set; }
        public string WhyVisit { get; set; }
        public string AreaFrom { get; set; }

        public virtual CompanyTable IdCoNavigation { get; set; }
        public virtual MyCustomer IdMyCustomerNavigation { get; set; }
        public virtual RoomsTable IdRoomNavigation { get; set; }
        public virtual ICollection<BillsTable> BillsTables { get; set; }
        public virtual ICollection<FollowerReceptionTable> FollowerReceptionTables { get; set; }
    }
}
