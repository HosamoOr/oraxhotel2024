using System;
using System.Collections.Generic;

#nullable disable

namespace HotelSys.Models
{
    public partial class RoomsTable
    {
        public RoomsTable()
        {
            PriceRoomsTables = new HashSet<PriceRoomsTable>();
            RecetionTables = new HashSet<RecetionTable>();
            StatusCurrentTables = new HashSet<StatusCurrentTable>();
        }

        public int Id { get; set; }
        public string NameR { get; set; }
        public string NumFloor { get; set; }
        public int? CountRooms { get; set; }
        public int? CountBedSingle { get; set; }
        public int? CountBedDouble { get; set; }
        public int? CountBathroom { get; set; }
        public int? CountTv { get; set; }
        public int? CountWallet { get; set; }
        public string TypeCondition { get; set; }
        public string PublicFeatures { get; set; }
        public string PrivateFeatures { get; set; }
        public string Note { get; set; }
        public int IdHo { get; set; }
        public int IdType { get; set; }

        public virtual HotelsBranchTable IdHoNavigation { get; set; }
        public virtual TypeRoomsTable IdTypeNavigation { get; set; }
        public virtual ICollection<PriceRoomsTable> PriceRoomsTables { get; set; }
        public virtual ICollection<RecetionTable> RecetionTables { get; set; }
        public virtual ICollection<StatusCurrentTable> StatusCurrentTables { get; set; }
    }
}
