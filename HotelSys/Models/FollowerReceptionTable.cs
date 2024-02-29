using System;
using System.Collections.Generic;

#nullable disable

namespace HotelSys.Models
{
    public partial class FollowerReceptionTable
    {
        public long Id { get; set; }
        public string CuType { get; set; }
        public string Relation { get; set; }
        public long IdReceptoin { get; set; }
        public long IdCustomer { get; set; }
        public string Duration { get; set; }
        public DateTime? DurationFrom { get; set; }
        public DateTime? DurationTo { get; set; }

        public virtual CustomerTable IdCustomerNavigation { get; set; }
        public virtual RecetionTable IdReceptoinNavigation { get; set; }
    }
}
