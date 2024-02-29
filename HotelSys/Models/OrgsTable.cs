using System;
using System.Collections.Generic;

#nullable disable

namespace HotelSys.Models
{
    public partial class OrgsTable
    {
        public OrgsTable()
        {
            HotelsBranchTables = new HashSet<HotelsBranchTable>();
        }

        public int Id { get; set; }
        public string NameH { get; set; }
        public string NumEn { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Regin { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public string MailBox { get; set; }
        public string Logo { get; set; }
        public int? IdSub { get; set; }
        public int? IdCountry { get; set; }

        public virtual CountryTable IdCountryNavigation { get; set; }
        public virtual ICollection<HotelsBranchTable> HotelsBranchTables { get; set; }
    }
}
