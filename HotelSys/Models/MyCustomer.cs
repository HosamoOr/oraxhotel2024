using System;
using System.Collections.Generic;

#nullable disable

namespace HotelSys.Models
{
    public partial class MyCustomer
    {
        public MyCustomer()
        {
            RecetionTables = new HashSet<RecetionTable>();
        }

        public long Id { get; set; }
        public long IdCustomer { get; set; }
        public int? Idsub { get; set; }
        public string PrivateNote { get; set; }
        public int? IdAccount { get; set; }
        public DateTime? Createat { get; set; }
        public DateTime? VisitEndDate { get; set; }

        public virtual AccountTable IdAccountNavigation { get; set; }
        public virtual CustomerTable IdCustomerNavigation { get; set; }
        public virtual ICollection<RecetionTable> RecetionTables { get; set; }
    }
}
