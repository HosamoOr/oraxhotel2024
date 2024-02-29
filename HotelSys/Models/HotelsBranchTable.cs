using System;
using System.Collections.Generic;

#nullable disable

namespace HotelSys.Models
{
    public partial class HotelsBranchTable
    {
        public HotelsBranchTable()
        {
            DetialsHotelTables = new HashSet<DetialsHotelTable>();
            RoomsTables = new HashSet<RoomsTable>();
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
        public int? IdOrg { get; set; }

        public virtual CountryTable IdCountryNavigation { get; set; }
        public virtual OrgsTable IdOrgNavigation { get; set; }
        public virtual ICollection<DetialsHotelTable> DetialsHotelTables { get; set; }
        public virtual ICollection<RoomsTable> RoomsTables { get; set; }
    }
}
