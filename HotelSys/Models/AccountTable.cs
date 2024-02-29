using System;
using System.Collections.Generic;

#nullable disable

namespace HotelSys.Models
{
    public partial class AccountTable
    {
        public AccountTable()
        {
            BankTables = new HashSet<BankTable>();
            BillsTables = new HashSet<BillsTable>();
            BondTables = new HashSet<BondTable>();
            BoxsTables = new HashSet<BoxsTable>();
            CompanyTables = new HashSet<CompanyTable>();
            EntriesAccTables = new HashSet<EntriesAccTable>();
            ItemsExpensesTables = new HashSet<ItemsExpensesTable>();
            MyCustomers = new HashSet<MyCustomer>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public bool? IsPrivate { get; set; }
        public DateTime? Createat { get; set; }
        public int IdGroup { get; set; }
        public int? Code { get; set; }

        public virtual GroupAccountTable IdGroupNavigation { get; set; }
        public virtual ICollection<BankTable> BankTables { get; set; }
        public virtual ICollection<BillsTable> BillsTables { get; set; }
        public virtual ICollection<BondTable> BondTables { get; set; }
        public virtual ICollection<BoxsTable> BoxsTables { get; set; }
        public virtual ICollection<CompanyTable> CompanyTables { get; set; }
        public virtual ICollection<EntriesAccTable> EntriesAccTables { get; set; }
        public virtual ICollection<ItemsExpensesTable> ItemsExpensesTables { get; set; }
        public virtual ICollection<MyCustomer> MyCustomers { get; set; }
    }
}
