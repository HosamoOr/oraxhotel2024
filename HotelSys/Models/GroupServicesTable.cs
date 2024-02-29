using System;
using System.Collections.Generic;

#nullable disable

namespace HotelSys.Models
{
    public partial class GroupServicesTable
    {
        public GroupServicesTable()
        {
            ProductTables = new HashSet<ProductTable>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? IdSub { get; set; }
        public string NameEn { get; set; }

        public virtual ICollection<ProductTable> ProductTables { get; set; }
    }
}
