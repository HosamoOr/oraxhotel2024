using System;
using System.Collections.Generic;

#nullable disable

namespace HotelSys.Models
{
    public partial class BoxsUserTable
    {
        public int Id { get; set; }
        public int IdBox { get; set; }
        public string IdAspUser { get; set; }
        public bool? IsDefult { get; set; }

        public virtual AspNetUser IdAspUserNavigation { get; set; }
        public virtual BoxsTable IdBoxNavigation { get; set; }
    }
}
