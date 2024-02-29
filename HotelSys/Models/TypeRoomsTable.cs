using System;
using System.Collections.Generic;

#nullable disable

namespace HotelSys.Models
{
    public partial class TypeRoomsTable
    {
        public TypeRoomsTable()
        {
            RoomsTables = new HashSet<RoomsTable>();
        }

        public int Id { get; set; }
        public string NameT { get; set; }
        public string Color { get; set; }
        public int? IdSub { get; set; }

        public virtual ICollection<RoomsTable> RoomsTables { get; set; }
    }
}
