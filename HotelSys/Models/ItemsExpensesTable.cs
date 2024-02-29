using System;
using System.Collections.Generic;

#nullable disable

namespace HotelSys.Models
{
    public partial class ItemsExpensesTable
    {
        public ItemsExpensesTable()
        {
            BondTables = new HashSet<BondTable>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? IdSub { get; set; }
        public int IdAccount { get; set; }
        public DateTime? CreateAt { get; set; }

        public virtual AccountTable IdAccountNavigation { get; set; }
        public virtual ICollection<BondTable> BondTables { get; set; }
    }
}
