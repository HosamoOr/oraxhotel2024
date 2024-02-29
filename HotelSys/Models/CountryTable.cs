using System;
using System.Collections.Generic;

#nullable disable

namespace HotelSys.Models
{
    public partial class CountryTable
    {
        public CountryTable()
        {
            CityTables = new HashSet<CityTable>();
            HotelsBranchTables = new HashSet<HotelsBranchTable>();
            OrgsTables = new HashSet<OrgsTable>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }

        public virtual ICollection<CityTable> CityTables { get; set; }
        public virtual ICollection<HotelsBranchTable> HotelsBranchTables { get; set; }
        public virtual ICollection<OrgsTable> OrgsTables { get; set; }
    }
}
