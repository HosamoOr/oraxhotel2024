using System;
using System.Collections.Generic;

#nullable disable

namespace HotelSys.Models
{
    public partial class BondTable
    {
        public BondTable()
        {
            InverseIdBondPayNavigation = new HashSet<BondTable>();
        }

        public long Id { get; set; }
        public string Type { get; set; }
        public string TypePay { get; set; }
        public string NumReference { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public string LocPay { get; set; }
        public DateTime? WorthyDate { get; set; }
        public string Why { get; set; }
        public string Hand { get; set; }
        public string NumCheck { get; set; }
        public string NumCard { get; set; }
        public string Note { get; set; }
        public DateTime? Createat { get; set; }
        public bool? IsDonePay { get; set; }
        public long? IdBondPay { get; set; }
        public int IdAccount { get; set; }
        public long? IdReception { get; set; }
        public int? IdItemExpenses { get; set; }
        public int? IdBank { get; set; }
        public int? IdCurrancy { get; set; }
        public TimeSpan? Time { get; set; }

        public virtual AccountTable IdAccountNavigation { get; set; }
        public virtual BondTable IdBondPayNavigation { get; set; }
        public virtual ItemsExpensesTable IdItemExpensesNavigation { get; set; }
        public virtual ICollection<BondTable> InverseIdBondPayNavigation { get; set; }
    }
}
