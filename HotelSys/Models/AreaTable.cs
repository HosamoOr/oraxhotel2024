using System;
using System.Collections.Generic;

#nullable disable

namespace HotelSys.Models
{
    public partial class AreaTable
    {
        public AreaTable()
        {
            CustomerTables = new HashSet<CustomerTable>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int IdCity { get; set; }
        public string NameEn { get; set; }
        public string NameArTashkeel { get; set; }
        public string NameArNormalized { get; set; }
        public string NameEnNormalized { get; set; }

        public virtual CityTable IdCityNavigation { get; set; }
        public virtual ICollection<CustomerTable> CustomerTables { get; set; }
    }
}
