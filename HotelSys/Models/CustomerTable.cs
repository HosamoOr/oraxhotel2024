using System;
using System.Collections.Generic;

#nullable disable

namespace HotelSys.Models
{
    public partial class CustomerTable
    {
        public CustomerTable()
        {
            FollowerReceptionTables = new HashSet<FollowerReceptionTable>();
            MyCustomers = new HashSet<MyCustomer>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Sex { get; set; }
        public string Email { get; set; }
        public string Nationality { get; set; }
        public string TypeWork { get; set; }
        public string LocWork { get; set; }
        public string PhoneWork { get; set; }
        public string TypeProof { get; set; }
        public string NumProof { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string LocRelease { get; set; }
        public DateTime Createat { get; set; }
        public string PublicNote { get; set; }
        public int? IdArea { get; set; }

        public virtual AreaTable IdAreaNavigation { get; set; }
        public virtual ICollection<FollowerReceptionTable> FollowerReceptionTables { get; set; }
        public virtual ICollection<MyCustomer> MyCustomers { get; set; }
    }
}
