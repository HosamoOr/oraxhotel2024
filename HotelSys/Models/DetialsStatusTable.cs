using System;
using System.Collections.Generic;

#nullable disable

namespace HotelSys.Models
{
    public partial class DetialsStatusTable
    {
        public DetialsStatusTable()
        {
            InverseIdStatusBeforeNavigation = new HashSet<DetialsStatusTable>();
        }

        public long Id { get; set; }
        public string Status { get; set; }
        public int IdRoom { get; set; }
        public string Detials { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public long? IdReception { get; set; }
        public int? IdEmp { get; set; }
        public DateTime Createat { get; set; }
        public int? IdSub { get; set; }
        public long? IdStatusBefore { get; set; }

        public virtual DetialsStatusTable IdStatusBeforeNavigation { get; set; }
        public virtual ICollection<DetialsStatusTable> InverseIdStatusBeforeNavigation { get; set; }
    }
}
