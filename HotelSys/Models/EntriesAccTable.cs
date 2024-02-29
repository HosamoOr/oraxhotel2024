using System;
using System.Collections.Generic;

#nullable disable

namespace HotelSys.Models
{
    public partial class EntriesAccTable
    {
        public long Id { get; set; }
        public string DebtOrCredit { get; set; }
        public double? Amount { get; set; }
        public string BillOrBand { get; set; }
        public long? IdDocumentDand { get; set; }
        public long? IdDocumentBill { get; set; }
        public string TypeDocument { get; set; }
        public int IdAccount { get; set; }
        public int? IdCurrancy { get; set; }
        public DateTime? Date { get; set; }
        public long? IdRecetion { get; set; }
        public string Note { get; set; }

        public virtual AccountTable IdAccountNavigation { get; set; }
    }
}
