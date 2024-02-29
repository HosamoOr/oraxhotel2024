using System;
using System.Collections.Generic;

#nullable disable

namespace HotelSys.Models
{
    public partial class GroupAccountTable
    {
        public GroupAccountTable()
        {
            AccountTables = new HashSet<AccountTable>();
            InverseIdMainGroupNavigation = new HashSet<GroupAccountTable>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? IdMainGroup { get; set; }
        public bool? IsRoot { get; set; }
        public bool? IsPrivate { get; set; }
        public int? IdSub { get; set; }

        public virtual GroupAccountTable IdMainGroupNavigation { get; set; }
        public virtual ICollection<AccountTable> AccountTables { get; set; }
        public virtual ICollection<GroupAccountTable> InverseIdMainGroupNavigation { get; set; }
    }
}
