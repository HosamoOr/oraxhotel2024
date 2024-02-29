using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace HotelSys.Models
{
    public partial class CustomersReceptionTable
    {
        public long Id { get; set; }
        public string CuType { get; set; }
        public string Relation { get; set; }
        public long IdReceptoin { get; set; }
        public long IdCustomer { get; set; }

        public virtual CustomerTable IdCustomerNavigation { get; set; }
        public virtual RecetionTable IdReceptoinNavigation { get; set; }
    }
}
