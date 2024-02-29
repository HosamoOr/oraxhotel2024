using System;
using System.Collections.Generic;

#nullable disable

namespace HotelSys.Models
{
    public partial class BillsTable
    {
        public BillsTable()
        {
            DetialsBillsTables = new HashSet<DetialsBillsTable>();
        }

        public long Id { get; set; }
        public string Type { get; set; }
        public string TypePay { get; set; }
        public string NumReference { get; set; }
        public DateTime Date { get; set; }
        public double? Total { get; set; }
        public bool IsForRoom { get; set; }
        public double? DeserveAmount { get; set; }
        public string TypeDiscount { get; set; }
        public double? QtyDiscount { get; set; }
        public double? PayAmount { get; set; }
        public double? RestAmount { get; set; }
        public string NumCheck { get; set; }
        public string NumCard { get; set; }
        public string Note { get; set; }
        public DateTime? Createat { get; set; }
        public int IdAccount { get; set; }
        public long? IdReception { get; set; }
        public int? IdBank { get; set; }
        public string CustomerOrCompany { get; set; }
        public int? IdCurrancy { get; set; }
        public double? TotalTaxPrice { get; set; }
        public double? TotalTaxRate { get; set; }
        public bool? IncludeTax { get; set; }
        public double? TotalBaladiTaxPrice { get; set; }
        public double? TotalBaladiTaxRate { get; set; }
        public bool? IsBaladiTax { get; set; }

        public virtual AccountTable IdAccountNavigation { get; set; }
        public virtual RecetionTable IdReceptionNavigation { get; set; }
        public virtual ICollection<DetialsBillsTable> DetialsBillsTables { get; set; }
    }
}
