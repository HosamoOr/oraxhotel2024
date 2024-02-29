using System;
using System.Collections.Generic;

#nullable disable

namespace HotelSys.Models
{
    public partial class OvertimeTable
    {
        public int Id { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateTime Createat { get; set; }
        public int? IdUser { get; set; }
        public int? IdSub { get; set; }
    }
}
