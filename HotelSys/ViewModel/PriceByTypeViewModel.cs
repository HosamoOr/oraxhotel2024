using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelSys.ViewModel
{
    public class PriceByTypeViewModel
    {
        public int Id { get; set; } // int

        [Required]
        [DataType(DataType.Text)]
        public string NameT { get; set; } // nvarchar(50)

        [Required]
        [DataType(DataType.Currency)]
        public double? Price { get; set; } // float

        [Required]
        [DataType(DataType.Currency)]
        public double? PriceOvertime { get; set; } // float
        [Required]
        [DataType(DataType.Currency)]
        public double? PriceMin { get; set; } // float

        public int? countRoom { set; get; }

        public int? IdSub { get; set; } // int

        public int? IdTaxGroup { get; set; } // int

    }
}
