using System;
using System.Collections.Generic;

#nullable disable

namespace HotelSys.Models
{
    public partial class JopEmpTable
    {
        public int Id { get; set; }
        public int IdEmp { get; set; }
        public int IdJobName { get; set; }

        public virtual EmpTable IdEmpNavigation { get; set; }
        public virtual EmpTable IdJobNameNavigation { get; set; }
    }
}
