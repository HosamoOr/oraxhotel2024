using System;
using System.Collections.Generic;

#nullable disable

namespace HotelSys.Models
{
    public partial class ProductTable
    {
        public ProductTable()
        {
            DetialsBillsTables = new HashSet<DetialsBillsTable>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int IdGroup { get; set; }
        public string NameEn { get; set; }
        public double Price { get; set; }
        public int? IdTaxGroup { get; set; }

        public virtual GroupServicesTable IdGroupNavigation { get; set; }
        public virtual TaxGroupTable IdTaxGroupNavigation { get; set; }
        public virtual ICollection<DetialsBillsTable> DetialsBillsTables { get; set; }
    }
}
