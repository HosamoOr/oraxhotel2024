using LinqToDB.Mapping;

namespace HotelSys.ViewModel
{
    public class TaxGroupViewModel
    {
        
           public int Id { get; set; } // int
             public string Name { get; set; } // nvarchar(max)
            public string NameEn { get; set; } // nvarchar(200)
             public int Rate { get; set; } // int

        public double priceTax { get; set; } // int
        public int? IdUser { get; set; } // int
        public bool IsBaladiTax { get; set; } // bit
         public double? BaladiRate { get; set; } // float
    }
}
