using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelSys.ViewModel
{


    public class _ReportBalanceAccountViewModel
    {
        public long id { get; set; }
        public int id_account { get; set; }
        public double? FromPrice { get; set; }
        public double? ToPrice { get; set; }
        public string NameAccount { get; set; }
        public string billOrBandDocumnet { get; set; }
        public string typeDocumnet { get; set; }
        public long? IdDocument { get; set; }
        public string dateDocumnet { get; set; }
        public string Note { get; set; }
        public int? id_currancy { get; set; }
        public char debt_or_Credit { get; set; }

        public double? Balance { get; set; }


        public OrgShortViewModel hotel { get; set; }

        public int def { get; set; }

        public string interval { get; set; }

        public string title { get; set; }

    }


}
