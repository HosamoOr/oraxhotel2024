using System;
using System.Collections.Generic;

#nullable disable

namespace HotelSys.Models
{
    public partial class DetialsBillsTable
    {
        public long Id { get; set; }
        public double? Qty { get; set; }
        public double? PriceOne { get; set; }
        public double? Total { get; set; }
        public long IdBill { get; set; }
        public int IdProduct { get; set; }
        public double? TaxPrice { get; set; }
        public double? TaxRate { get; set; }
        public double? BaladiTaxPrice { get; set; }
        public double? BaladiTaxRate { get; set; }
        public bool? IsBaladiTax { get; set; }

        public virtual BillsTable IdBillNavigation { get; set; }
        public virtual ProductTable IdProductNavigation { get; set; }
    }
}
