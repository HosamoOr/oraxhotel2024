using System;
using System.Collections.Generic;

#nullable disable

namespace HotelSys.Models
{
    public partial class TaxGroupTable
    {
        public TaxGroupTable()
        {
            PriceRoomsTables = new HashSet<PriceRoomsTable>();
            ProductTables = new HashSet<ProductTable>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public int Rate { get; set; }
        public int? IdUser { get; set; }
        public bool IsBaladiTax { get; set; }
        public double? BaladiRate { get; set; }

        public virtual ICollection<PriceRoomsTable> PriceRoomsTables { get; set; }
        public virtual ICollection<ProductTable> ProductTables { get; set; }
    }
}
