using System;
using System.Collections.Generic;

#nullable disable

namespace HotelSys.Models
{
    public partial class CompanyTable
    {
        public CompanyTable()
        {
            RecetionTables = new HashSet<RecetionTable>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? IdAccount { get; set; }
        public int? IdSub { get; set; }

        public virtual AccountTable IdAccountNavigation { get; set; }
        public virtual ICollection<RecetionTable> RecetionTables { get; set; }
    }
}
