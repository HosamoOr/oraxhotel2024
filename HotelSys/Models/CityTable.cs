using System;
using System.Collections.Generic;

#nullable disable

namespace HotelSys.Models
{
    public partial class CityTable
    {
        public CityTable()
        {
            AreaTables = new HashSet<AreaTable>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? IdCountry { get; set; }
        public string NameEn { get; set; }

        public virtual CountryTable IdCountryNavigation { get; set; }
        public virtual ICollection<AreaTable> AreaTables { get; set; }
    }
}
