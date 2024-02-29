//using System;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata;
//using DataModels;

//// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
//// If you have enabled NRTs for your project, then un-comment the following line:
//// #nullable disable

//namespace HotelSys.Models
//{
//    public partial class HotelSysContextConnection : DbContext
//    {
//        public HotelSysContextConnection()
//        {
//        }

//        public HotelSysContextConnection(DbContextOptions<HotelSysContextConnection> options)
//            : base(options)
//        {
//        }

//        public virtual DbSet<AccountTable> AccountTable { get; set; }
//        public virtual DbSet<AdminTable> AdminTable { get; set; }
//        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
//        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
//        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
//        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
//        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
//        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
//        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
//        public virtual DbSet<BankTable> BankTable { get; set; }
//        public virtual DbSet<BillsTable> BillsTable { get; set; }
//        public virtual DbSet<BondTable> BondTable { get; set; }
//        public virtual DbSet<BoxsTable> BoxsTable { get; set; }
//        public virtual DbSet<ChangeRoomTable> ChangeRoomTable { get; set; }
//        public virtual DbSet<CompanyTable> CompanyTable { get; set; }
//        public virtual DbSet<ConditionReceptionTable> ConditionReceptionTable { get; set; }
//        public virtual DbSet<CurrencyTable> CurrencyTable { get; set; }
//        public virtual DbSet<CustomerTable> CustomerTable { get; set; }
//        public virtual DbSet<CustomersReceptionTable> CustomersReceptionTable { get; set; }
//        public virtual DbSet<DetialsBillsTable> DetialsBillsTable { get; set; }
//        public virtual DbSet<DetialsHotelTable> DetialsHotelTable { get; set; }
//        public virtual DbSet<DetialsStatusTable> DetialsStatusTable { get; set; }
//        public virtual DbSet<EmpTable> EmpTable { get; set; }
//        public virtual DbSet<EntriesAccTable> EntriesAccTable { get; set; }
//        public virtual DbSet<GroupAccountTable> GroupAccountTable { get; set; }
//        public virtual DbSet<GroupServicesTable> GroupServicesTable { get; set; }
//        public virtual DbSet<HotelsTable> HotelsTable { get; set; }
//        public virtual DbSet<ItemsExpensesTable> ItemsExpensesTable { get; set; }
//        public virtual DbSet<JobsNameTable> JobsNameTable { get; set; }
//        public virtual DbSet<JopEmpTable> JopEmpTable { get; set; }
//        public virtual DbSet<OvertimeTable> OvertimeTable { get; set; }
//        public virtual DbSet<PriceRoomsTable> PriceRoomsTable { get; set; }
//        public virtual DbSet<ProductTable> ProductTable { get; set; }
//        public virtual DbSet<RecetionTable> RecetionTable { get; set; }
//        public virtual DbSet<RoomsTable> RoomsTable { get; set; }
//        public virtual DbSet<SettingReceptionTable> SettingReceptionTable { get; set; }
//        public virtual DbSet<StatusCurrentTable> StatusCurrentTable { get; set; }
//        public virtual DbSet<TypeRoomsTable> TypeRoomsTable { get; set; }
//        public virtual DbSet<UserTable> UserTable { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Server=.;Database=HORS_DB;Trusted_Connection=True;");
//            }
//        }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<AccountTable>(entity =>
//            {
//                entity.ToTable("account_table");

//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.Createat)
//                    .HasColumnName("createat")
//                    .HasColumnType("datetime");

//                entity.Property(e => e.IdGroup).HasColumnName("id_group");

//                entity.Property(e => e.IsPrivate)
//                    .HasColumnName("is_private")
//                    .HasDefaultValueSql("((0))");

//                entity.Property(e => e.Name)
//                    .IsRequired()
//                    .HasColumnName("name")
//                    .HasMaxLength(100);

//                entity.Property(e => e.Status)
//                    .HasColumnName("status")
//                    .HasMaxLength(50)
//                    .HasDefaultValueSql("('active')");

//                entity.HasOne(d => d.IdGroupNavigation)
//                    .WithMany(p => p.AccountTable)
//                    .HasForeignKey(d => d.IdGroup)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("fk_group_account_account");
//            });

//            modelBuilder.Entity<AdminTable>(entity =>
//            {
//                entity.ToTable("admin_table");

//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.Adminid).HasColumnName("adminid");

//                entity.Property(e => e.LastdateLogin)
//                    .HasColumnName("lastdate_login")
//                    .HasColumnType("date");

//                entity.Property(e => e.Name)
//                    .IsRequired()
//                    .HasColumnName("name")
//                    .HasMaxLength(70);

//                entity.Property(e => e.Password)
//                    .IsRequired()
//                    .HasColumnName("password")
//                    .HasMaxLength(200);

//                entity.Property(e => e.Status)
//                    .HasColumnName("status")
//                    .HasDefaultValueSql("((1))");

//                entity.Property(e => e.Username)
//                    .IsRequired()
//                    .HasColumnName("username")
//                    .HasMaxLength(50);
//            });

//            modelBuilder.Entity<AspNetRoleClaims>(entity =>
//            {
//                entity.Property(e => e.RoleId)
//                    .IsRequired()
//                    .HasMaxLength(450);
//            });

//            modelBuilder.Entity<AspNetRoles>(entity =>
//            {
//                entity.Property(e => e.Name).HasMaxLength(256);

//                entity.Property(e => e.NormalizedName).HasMaxLength(256);
//            });

//            modelBuilder.Entity<AspNetUserClaims>(entity =>
//            {
//                entity.Property(e => e.UserId)
//                    .IsRequired()
//                    .HasMaxLength(450);
//            });

//            modelBuilder.Entity<AspNetUserLogins>(entity =>
//            {
//                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

//                entity.Property(e => e.LoginProvider).HasMaxLength(128);

//                entity.Property(e => e.ProviderKey).HasMaxLength(128);

//                entity.Property(e => e.UserId)
//                    .IsRequired()
//                    .HasMaxLength(450);
//            });

//            modelBuilder.Entity<AspNetUserRoles>(entity =>
//            {
//                entity.HasKey(e => new { e.UserId, e.RoleId });
//            });

//            modelBuilder.Entity<AspNetUserTokens>(entity =>
//            {
//                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

//                entity.Property(e => e.LoginProvider).HasMaxLength(128);

//                entity.Property(e => e.Name).HasMaxLength(128);
//            });

//            modelBuilder.Entity<AspNetUsers>(entity =>
//            {
//                entity.Property(e => e.Email).HasMaxLength(256);

//                entity.Property(e => e.FirstName).HasMaxLength(100);

//                entity.Property(e => e.LastName).HasMaxLength(100);

//                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

//                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

//                entity.Property(e => e.UserName).HasMaxLength(256);
//            });

//            modelBuilder.Entity<BankTable>(entity =>
//            {
//                entity.ToTable("bank_table");

//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.IdAccount).HasColumnName("id_account");

//                entity.Property(e => e.IdSub).HasColumnName("id_sub");

//                entity.Property(e => e.IsDefault)
//                    .HasColumnName("is_default")
//                    .HasDefaultValueSql("((0))");

//                entity.Property(e => e.Name)
//                    .IsRequired()
//                    .HasColumnName("name")
//                    .HasMaxLength(300);

//                entity.HasOne(d => d.IdAccountNavigation)
//                    .WithMany(p => p.BankTable)
//                    .HasForeignKey(d => d.IdAccount)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("fk_bank_account");
//            });

//            modelBuilder.Entity<BillsTable>(entity =>
//            {
//                entity.ToTable("bills_table");

//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.Createat)
//                    .HasColumnName("createat")
//                    .HasColumnType("datetime");

//                entity.Property(e => e.Date)
//                    .HasColumnName("date")
//                    .HasColumnType("datetime");

//                entity.Property(e => e.DeserveAmount)
//                    .HasColumnName("deserve_amount")
//                    .HasDefaultValueSql("((0))");

//                entity.Property(e => e.IdAccount).HasColumnName("id_account");

//                entity.Property(e => e.IdBank).HasColumnName("id_bank");

//                entity.Property(e => e.IdReception).HasColumnName("id_reception");

//                entity.Property(e => e.IsForRoom).HasColumnName("is_for_room");

//                entity.Property(e => e.Note).HasColumnName("note");

//                entity.Property(e => e.NumCard)
//                    .HasColumnName("num_card")
//                    .HasMaxLength(50);

//                entity.Property(e => e.NumCheck)
//                    .HasColumnName("num_check")
//                    .HasMaxLength(50);

//                entity.Property(e => e.NumReference)
//                    .HasColumnName("num_reference")
//                    .HasMaxLength(100);

//                entity.Property(e => e.PayAmount)
//                    .HasColumnName("pay_amount")
//                    .HasDefaultValueSql("((0))");

//                entity.Property(e => e.QtyDiscount)
//                    .HasColumnName("qty_discount")
//                    .HasDefaultValueSql("((0))");

//                entity.Property(e => e.RestAmount)
//                    .HasColumnName("rest_amount")
//                    .HasDefaultValueSql("((0))");

//                entity.Property(e => e.Total)
//                    .HasColumnName("total")
//                    .HasDefaultValueSql("((0))");

//                entity.Property(e => e.Type)
//                    .IsRequired()
//                    .HasColumnName("type")
//                    .HasMaxLength(20);

//                entity.Property(e => e.TypeDiscount)
//                    .HasColumnName("type_discount")
//                    .HasMaxLength(50)
//                    .HasDefaultValueSql("('%')");

//                entity.Property(e => e.TypePay)
//                    .IsRequired()
//                    .HasColumnName("type_pay")
//                    .HasMaxLength(20);

//                entity.HasOne(d => d.IdAccountNavigation)
//                    .WithMany(p => p.BillsTable)
//                    .HasForeignKey(d => d.IdAccount)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("fk_bills_account");

//                entity.HasOne(d => d.IdReceptionNavigation)
//                    .WithMany(p => p.BillsTable)
//                    .HasForeignKey(d => d.IdReception)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("fk_bills_reception");
//            });

//            modelBuilder.Entity<BondTable>(entity =>
//            {
//                entity.ToTable("bond_table");

//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.Amount)
//                    .HasColumnName("amount")
//                    .HasDefaultValueSql("((0))");

//                entity.Property(e => e.Createat)
//                    .HasColumnName("createat")
//                    .HasColumnType("datetime");

//                entity.Property(e => e.Date)
//                    .HasColumnName("date")
//                    .HasColumnType("datetime");

//                entity.Property(e => e.DeserveAmount)
//                    .HasColumnName("deserve_amount")
//                    .HasDefaultValueSql("((0))");

//                entity.Property(e => e.Hand).HasColumnName("hand");

//                entity.Property(e => e.IdAccount).HasColumnName("id_account");

//                entity.Property(e => e.IdBank).HasColumnName("id_bank");

//                entity.Property(e => e.IdBill).HasColumnName("id_bill");

//                entity.Property(e => e.IdBondPay).HasColumnName("id_bond_pay");

//                entity.Property(e => e.IdItemExpenses).HasColumnName("id_item_expenses");

//                entity.Property(e => e.IsDonePay).HasColumnName("is_done_pay");

//                entity.Property(e => e.LocPay)
//                    .HasColumnName("loc_pay")
//                    .HasMaxLength(300);

//                entity.Property(e => e.Note).HasColumnName("note");

//                entity.Property(e => e.NumCard)
//                    .HasColumnName("num_card")
//                    .HasMaxLength(50);

//                entity.Property(e => e.NumCheck)
//                    .HasColumnName("num_check")
//                    .HasMaxLength(50);

//                entity.Property(e => e.NumReference)
//                    .HasColumnName("num_reference")
//                    .HasMaxLength(100);

//                entity.Property(e => e.ReceiptsOrExpenses)
//                    .IsRequired()
//                    .HasColumnName("receipts_or_expenses")
//                    .HasMaxLength(1);

//                entity.Property(e => e.Type)
//                    .IsRequired()
//                    .HasColumnName("type")
//                    .HasMaxLength(20);

//                entity.Property(e => e.TypePay)
//                    .IsRequired()
//                    .HasColumnName("type_pay")
//                    .HasMaxLength(20);

//                entity.Property(e => e.Why).HasColumnName("why");

//                entity.Property(e => e.WorthyDate)
//                    .HasColumnName("worthy_date")
//                    .HasColumnType("datetime");

//                entity.HasOne(d => d.IdAccountNavigation)
//                    .WithMany(p => p.BondTable)
//                    .HasForeignKey(d => d.IdAccount)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("fk_bond_account");

//                entity.HasOne(d => d.IdBillNavigation)
//                    .WithMany(p => p.BondTable)
//                    .HasForeignKey(d => d.IdBill)
//                    .HasConstraintName("fk_bill_bond");

//                entity.HasOne(d => d.IdBondPayNavigation)
//                    .WithMany(p => p.InverseIdBondPayNavigation)
//                    .HasForeignKey(d => d.IdBondPay)
//                    .HasConstraintName("fk_bond_bond");

//                entity.HasOne(d => d.IdItemExpensesNavigation)
//                    .WithMany(p => p.BondTable)
//                    .HasForeignKey(d => d.IdItemExpenses)
//                    .HasConstraintName("fk_bond_expense");
//            });

//            modelBuilder.Entity<BoxsTable>(entity =>
//            {
//                entity.ToTable("boxs_table");

//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.IdAccount).HasColumnName("id_account");

//                entity.Property(e => e.IdSub).HasColumnName("id_sub");

//                entity.Property(e => e.IsDefault)
//                    .HasColumnName("is_default")
//                    .HasDefaultValueSql("((0))");

//                entity.Property(e => e.Name)
//                    .IsRequired()
//                    .HasColumnName("name")
//                    .HasMaxLength(300);

//                entity.HasOne(d => d.IdAccountNavigation)
//                    .WithMany(p => p.BoxsTable)
//                    .HasForeignKey(d => d.IdAccount)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("fk_boxs_account");
//            });

//            modelBuilder.Entity<ChangeRoomTable>(entity =>
//            {
//                entity.ToTable("change_room_table");

//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.Date)
//                    .HasColumnName("date")
//                    .HasColumnType("datetime");

//                entity.Property(e => e.IdReceptoin).HasColumnName("id_receptoin");

//                entity.Property(e => e.IdRoomFrom).HasColumnName("id_room_from");

//                entity.Property(e => e.IdRoomTo).HasColumnName("id_room_to");

//                entity.Property(e => e.IdSub).HasColumnName("id_sub");

//                entity.Property(e => e.PriceCurrent).HasColumnName("price_current");

//                entity.Property(e => e.PriceOld).HasColumnName("price_old");

//                entity.Property(e => e.Why)
//                    .IsRequired()
//                    .HasColumnName("why");
//            });

//            modelBuilder.Entity<CompanyTable>(entity =>
//            {
//                entity.ToTable("company_table");

//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.IdAccount).HasColumnName("id_account");

//                entity.Property(e => e.IdSub).HasColumnName("id_sub");

//                entity.Property(e => e.Name)
//                    .IsRequired()
//                    .HasColumnName("name")
//                    .HasMaxLength(50);

//                entity.HasOne(d => d.IdAccountNavigation)
//                    .WithMany(p => p.CompanyTable)
//                    .HasForeignKey(d => d.IdAccount)
//                    .HasConstraintName("fk_company_account");
//            });

//            modelBuilder.Entity<ConditionReceptionTable>(entity =>
//            {
//                entity.ToTable("condition_reception_table");

//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.IdSub).HasColumnName("id_sub");

//                entity.Property(e => e.Name)
//                    .IsRequired()
//                    .HasColumnName("name");

//                entity.Property(e => e.Num).HasColumnName("num");
//            });

//            modelBuilder.Entity<CurrencyTable>(entity =>
//            {
//                entity.ToTable("currency_table");

//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.Code)
//                    .HasColumnName("code")
//                    .HasMaxLength(5);

//                entity.Property(e => e.IdSub).HasColumnName("id_sub");

//                entity.Property(e => e.IsDefault)
//                    .HasColumnName("is_default")
//                    .HasDefaultValueSql("((0))");

//                entity.Property(e => e.Name)
//                    .IsRequired()
//                    .HasColumnName("name")
//                    .HasMaxLength(300);

//                entity.Property(e => e.RateConvert)
//                    .HasColumnName("rate_convert")
//                    .HasDefaultValueSql("((1))");
//            });

//            modelBuilder.Entity<CustomerTable>(entity =>
//            {
//                entity.ToTable("customer_table");

//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.Createat)
//                    .HasColumnName("createat")
//                    .HasColumnType("datetime");

//                entity.Property(e => e.Email).HasColumnName("email");

//                entity.Property(e => e.EndDate)
//                    .HasColumnName("end_date")
//                    .HasColumnType("date");

//                entity.Property(e => e.IdAccount).HasColumnName("id_account");

//                entity.Property(e => e.IdSub).HasColumnName("id_sub");

//                entity.Property(e => e.LocRelease)
//                    .HasColumnName("loc_release")
//                    .HasMaxLength(300);

//                entity.Property(e => e.LocWork)
//                    .HasColumnName("loc_work")
//                    .HasMaxLength(100);

//                entity.Property(e => e.Name)
//                    .IsRequired()
//                    .HasColumnName("name");

//                entity.Property(e => e.Nationality)
//                    .IsRequired()
//                    .HasColumnName("nationality")
//                    .HasMaxLength(50);

//                entity.Property(e => e.NumProof)
//                    .IsRequired()
//                    .HasColumnName("num_proof")
//                    .HasMaxLength(300);

//                entity.Property(e => e.PhoneWork)
//                    .HasColumnName("phone_work")
//                    .HasMaxLength(100);

//                entity.Property(e => e.PrivateNote).HasColumnName("private_note");

//                entity.Property(e => e.PublicNot).HasColumnName("public_not");

//                entity.Property(e => e.ReleaseDate)
//                    .HasColumnName("release_date")
//                    .HasColumnType("date");

//                entity.Property(e => e.Sex)
//                    .HasColumnName("sex")
//                    .HasMaxLength(10)
//                    .HasDefaultValueSql("('male')");

//                entity.Property(e => e.Type)
//                    .IsRequired()
//                    .HasColumnName("type")
//                    .HasMaxLength(10);

//                entity.Property(e => e.TypeProof)
//                    .IsRequired()
//                    .HasColumnName("type_proof")
//                    .HasMaxLength(30);

//                entity.Property(e => e.TypeWork)
//                    .HasColumnName("type_work")
//                    .HasMaxLength(100);

//                entity.HasOne(d => d.IdAccountNavigation)
//                    .WithMany(p => p.CustomerTable)
//                    .HasForeignKey(d => d.IdAccount)
//                    .HasConstraintName("fk_customer_account");
//            });

//            modelBuilder.Entity<CustomersReceptionTable>(entity =>
//            {
//                entity.ToTable("customers_reception_table");

//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.CuType)
//                    .IsRequired()
//                    .HasColumnName("cu_type")
//                    .HasMaxLength(10);

//                entity.Property(e => e.IdCustomer).HasColumnName("id_customer");

//                entity.Property(e => e.IdReceptoin).HasColumnName("id_receptoin");

//                entity.Property(e => e.Relation)
//                    .IsRequired()
//                    .HasColumnName("relation")
//                    .HasMaxLength(50);

//                entity.HasOne(d => d.IdCustomerNavigation)
//                    .WithMany(p => p.CustomersReceptionTable)
//                    .HasForeignKey(d => d.IdCustomer)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("fk_customers_reception_customer");

//                entity.HasOne(d => d.IdReceptoinNavigation)
//                    .WithMany(p => p.CustomersReceptionTable)
//                    .HasForeignKey(d => d.IdReceptoin)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("fk_customers_reception_reception");
//            });

//            modelBuilder.Entity<DetialsBillsTable>(entity =>
//            {
//                entity.ToTable("detials_bills_table");

//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.IdBill).HasColumnName("id_bill");

//                entity.Property(e => e.IdProduct).HasColumnName("id_product");

//                entity.Property(e => e.PriceOne)
//                    .HasColumnName("price_one")
//                    .HasDefaultValueSql("((0))");

//                entity.Property(e => e.Qty)
//                    .HasColumnName("qty")
//                    .HasDefaultValueSql("((0))");

//                entity.Property(e => e.Total)
//                    .HasColumnName("total")
//                    .HasDefaultValueSql("((0))");

//                entity.HasOne(d => d.IdBillNavigation)
//                    .WithMany(p => p.DetialsBillsTable)
//                    .HasForeignKey(d => d.IdBill)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("fk_bills_detials_bills");

//                entity.HasOne(d => d.IdProductNavigation)
//                    .WithMany(p => p.DetialsBillsTable)
//                    .HasForeignKey(d => d.IdProduct)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("fk_product_services_detials");
//            });

//            modelBuilder.Entity<DetialsHotelTable>(entity =>
//            {
//                entity.ToTable("detials_hotel_table");

//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.CountFloot)
//                    .HasColumnName("count_floot")
//                    .HasDefaultValueSql("((0))");

//                entity.Property(e => e.CountRoom)
//                    .HasColumnName("count_room")
//                    .HasDefaultValueSql("((0))");

//                entity.Property(e => e.IdHo).HasColumnName("id_ho");

//                entity.HasOne(d => d.IdHoNavigation)
//                    .WithMany(p => p.DetialsHotelTable)
//                    .HasForeignKey(d => d.IdHo)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("fk_detials_hotel_hotels");
//            });

//            modelBuilder.Entity<DetialsStatusTable>(entity =>
//            {
//                entity.ToTable("detials_status_table");

//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.Createat)
//                    .HasColumnName("createat")
//                    .HasColumnType("datetime");

//                entity.Property(e => e.Detials).HasColumnName("detials");

//                entity.Property(e => e.EndDate)
//                    .HasColumnName("end_date")
//                    .HasColumnType("datetime");

//                entity.Property(e => e.IdEmp).HasColumnName("id_emp");

//                entity.Property(e => e.IdReception).HasColumnName("id_reception");

//                entity.Property(e => e.IdRoom).HasColumnName("id_room");

//                entity.Property(e => e.IdSub).HasColumnName("id_sub");

//                entity.Property(e => e.StartDate)
//                    .HasColumnName("start_date")
//                    .HasColumnType("datetime");

//                entity.Property(e => e.Status)
//                    .HasColumnName("status")
//                    .HasMaxLength(50)
//                    .HasDefaultValueSql("('empty')");
//            });

//            modelBuilder.Entity<EmpTable>(entity =>
//            {
//                entity.ToTable("emp_table");

//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.Email)
//                    .HasColumnName("email")
//                    .HasMaxLength(50);

//                entity.Property(e => e.IdSub).HasColumnName("id_sub");

//                entity.Property(e => e.Img)
//                    .HasColumnName("img")
//                    .HasMaxLength(300);

//                entity.Property(e => e.Name)
//                    .IsRequired()
//                    .HasColumnName("name")
//                    .HasMaxLength(50);

//                entity.Property(e => e.NumIdentity)
//                    .HasColumnName("num_identity")
//                    .HasMaxLength(50);

//                entity.Property(e => e.Phone)
//                    .IsRequired()
//                    .HasColumnName("phone")
//                    .HasMaxLength(15);

//                entity.Property(e => e.Sex)
//                    .HasColumnName("sex")
//                    .HasMaxLength(10);
//            });

//            modelBuilder.Entity<EntriesAccTable>(entity =>
//            {
//                entity.ToTable("entries_acc_table");

//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.Amount)
//                    .HasColumnName("amount")
//                    .HasDefaultValueSql("((0))");

//                entity.Property(e => e.BillOrBand)
//                    .IsRequired()
//                    .HasColumnName("bill_or_band")
//                    .HasMaxLength(10);

//                entity.Property(e => e.DebtOrCredit)
//                    .IsRequired()
//                    .HasColumnName("debt_or_Credit")
//                    .HasMaxLength(1);

//                entity.Property(e => e.IdAccount).HasColumnName("id_account");

//                entity.Property(e => e.IdDocument).HasColumnName("id_document");

//                entity.Property(e => e.TypeDocument)
//                    .IsRequired()
//                    .HasColumnName("type_document")
//                    .HasMaxLength(50);

//                entity.HasOne(d => d.IdAccountNavigation)
//                    .WithMany(p => p.EntriesAccTable)
//                    .HasForeignKey(d => d.IdAccount)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("fk_entries_acc_account");
//            });

//            modelBuilder.Entity<GroupAccountTable>(entity =>
//            {
//                entity.ToTable("group_account_table");

//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.IdMainGroup).HasColumnName("id_main_group");

//                entity.Property(e => e.IdSub).HasColumnName("id_sub");

//                entity.Property(e => e.IsPrivate)
//                    .HasColumnName("is_private")
//                    .HasDefaultValueSql("((0))");

//                entity.Property(e => e.IsRoot)
//                    .HasColumnName("is_root")
//                    .HasDefaultValueSql("((0))");

//                entity.Property(e => e.Name)
//                    .IsRequired()
//                    .HasColumnName("name")
//                    .HasMaxLength(100);

//                entity.HasOne(d => d.IdMainGroupNavigation)
//                    .WithMany(p => p.InverseIdMainGroupNavigation)
//                    .HasForeignKey(d => d.IdMainGroup)
//                    .HasConstraintName("fk_group_account_group_account");
//            });

//            modelBuilder.Entity<GroupServicesTable>(entity =>
//            {
//                entity.ToTable("group_services_table");

//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.IdSub).HasColumnName("id_sub");

//                entity.Property(e => e.Name)
//                    .IsRequired()
//                    .HasColumnName("name")
//                    .HasMaxLength(300);
//            });

//            modelBuilder.Entity<HotelsTable>(entity =>
//            {
//                entity.ToTable("hotels_table");

//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.Address)
//                    .HasColumnName("address")
//                    .HasMaxLength(150);

//                entity.Property(e => e.City)
//                    .IsRequired()
//                    .HasColumnName("city")
//                    .HasMaxLength(50);

//                entity.Property(e => e.Country)
//                    .IsRequired()
//                    .HasColumnName("country")
//                    .HasMaxLength(50);

//                entity.Property(e => e.Email).HasColumnName("email");

//                entity.Property(e => e.IdSub).HasColumnName("id_sub");

//                entity.Property(e => e.Logo).HasColumnName("logo");

//                entity.Property(e => e.MailBox)
//                    .HasColumnName("mail_box")
//                    .HasMaxLength(150);

//                entity.Property(e => e.NameH)
//                    .IsRequired()
//                    .HasColumnName("name_h")
//                    .HasMaxLength(50);

//                entity.Property(e => e.NumEn)
//                    .HasColumnName("num_en")
//                    .HasMaxLength(50);

//                entity.Property(e => e.Phone)
//                    .IsRequired()
//                    .HasColumnName("phone");

//                entity.Property(e => e.Regin)
//                    .IsRequired()
//                    .HasColumnName("regin")
//                    .HasMaxLength(50);

//                entity.Property(e => e.Website).HasColumnName("website");
//            });

//            modelBuilder.Entity<ItemsExpensesTable>(entity =>
//            {
//                entity.ToTable("items_expenses_table");

//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.IdSub).HasColumnName("id_sub");

//                entity.Property(e => e.Name)
//                    .IsRequired()
//                    .HasColumnName("name")
//                    .HasMaxLength(300);
//            });

//            modelBuilder.Entity<JobsNameTable>(entity =>
//            {
//                entity.ToTable("jobs_name_table");

//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.IdSub).HasColumnName("id_sub");

//                entity.Property(e => e.Name)
//                    .IsRequired()
//                    .HasColumnName("name")
//                    .HasMaxLength(300);
//            });

//            modelBuilder.Entity<JopEmpTable>(entity =>
//            {
//                entity.ToTable("jop_emp_table");

//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.IdEmp).HasColumnName("id_emp");

//                entity.Property(e => e.IdJobName).HasColumnName("id_job_name");

//                entity.HasOne(d => d.IdEmpNavigation)
//                    .WithMany(p => p.JopEmpTableIdEmpNavigation)
//                    .HasForeignKey(d => d.IdEmp)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("fk_jop_emp_emp");

//                entity.HasOne(d => d.IdJobNameNavigation)
//                    .WithMany(p => p.JopEmpTableIdJobNameNavigation)
//                    .HasForeignKey(d => d.IdJobName)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("fk_jop_emp_job_");
//            });

//            modelBuilder.Entity<OvertimeTable>(entity =>
//            {
//                entity.ToTable("overtime_table");

//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.Createat)
//                    .HasColumnName("createat")
//                    .HasColumnType("datetime");

//                entity.Property(e => e.EndDate)
//                    .HasColumnName("end_date")
//                    .HasColumnType("date");

//                entity.Property(e => e.EndTime).HasColumnName("end_time");

//                entity.Property(e => e.IdSub).HasColumnName("id_sub");

//                entity.Property(e => e.IdUser).HasColumnName("id_user");

//                entity.Property(e => e.StartDate)
//                    .HasColumnName("start_date")
//                    .HasColumnType("date");

//                entity.Property(e => e.StartTime).HasColumnName("start_time");
//            });

//            modelBuilder.Entity<PriceRoomsTable>(entity =>
//            {
//                entity.ToTable("price_rooms_table");

//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.IdRoom).HasColumnName("id_room");

//                entity.Property(e => e.IdSub).HasColumnName("id_sub");

//                entity.Property(e => e.Price)
//                    .HasColumnName("price")
//                    .HasDefaultValueSql("((0))");

//                entity.Property(e => e.PriceMin)
//                    .HasColumnName("price_min")
//                    .HasDefaultValueSql("((0))");

//                entity.Property(e => e.PriceOvertime)
//                    .HasColumnName("price_overtime")
//                    .HasDefaultValueSql("((0))");

//                entity.HasOne(d => d.IdRoomNavigation)
//                    .WithMany(p => p.PriceRoomsTable)
//                    .HasForeignKey(d => d.IdRoom)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("fk_rooms_price_rooms");
//            });

//            modelBuilder.Entity<ProductTable>(entity =>
//            {
//                entity.ToTable("product_table");

//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.IdGroup).HasColumnName("id_group");

//                entity.Property(e => e.Name)
//                    .IsRequired()
//                    .HasColumnName("name");

//                entity.HasOne(d => d.IdGroupNavigation)
//                    .WithMany(p => p.ProductTable)
//                    .HasForeignKey(d => d.IdGroup)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("fk_group_services_product_services");
//            });

//            modelBuilder.Entity<RecetionTable>(entity =>
//            {
//                entity.ToTable("recetion_table");

//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.ChechoutDate)
//                    .HasColumnName("chechout_date")
//                    .HasColumnType("datetime");

//                entity.Property(e => e.CheckinDate)
//                    .HasColumnName("checkin_date")
//                    .HasColumnType("datetime");

//                entity.Property(e => e.EndDate)
//                    .HasColumnName("end_date")
//                    .HasColumnType("datetime");

//                entity.Property(e => e.IdCo).HasColumnName("id_co");

//                entity.Property(e => e.IsChechin)
//                    .HasColumnName("is_chechin")
//                    .HasDefaultValueSql("((0))");

//                entity.Property(e => e.IsChechout)
//                    .HasColumnName("is_chechout")
//                    .HasDefaultValueSql("((0))");

//                entity.Property(e => e.Price)
//                    .HasColumnName("price")
//                    .HasDefaultValueSql("((0))");

//                entity.Property(e => e.QtyTime)
//                    .HasColumnName("qty_time")
//                    .HasDefaultValueSql("((0))");

//                entity.Property(e => e.Source)
//                    .IsRequired()
//                    .HasColumnName("source")
//                    .HasMaxLength(50);

//                entity.Property(e => e.StartDate)
//                    .HasColumnName("start_date")
//                    .HasColumnType("datetime");

//                entity.Property(e => e.TypeDate)
//                    .HasColumnName("type_date")
//                    .HasMaxLength(1)
//                    .HasDefaultValueSql("('m')");

//                entity.Property(e => e.Unit)
//                    .IsRequired()
//                    .HasColumnName("unit")
//                    .HasMaxLength(50);

//                entity.HasOne(d => d.IdCoNavigation)
//                    .WithMany(p => p.RecetionTable)
//                    .HasForeignKey(d => d.IdCo)
//                    .HasConstraintName("fk_company_recetion");
//            });

//            modelBuilder.Entity<RoomsTable>(entity =>
//            {
//                entity.ToTable("rooms_table");

//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.CountBathroom)
//                    .HasColumnName("count_bathroom")
//                    .HasDefaultValueSql("((0))");

//                entity.Property(e => e.CountBedDouble)
//                    .HasColumnName("count_bed_double")
//                    .HasDefaultValueSql("((0))");

//                entity.Property(e => e.CountBedSingle)
//                    .HasColumnName("count_bed_single")
//                    .HasDefaultValueSql("((0))");

//                entity.Property(e => e.CountRooms)
//                    .HasColumnName("count_rooms")
//                    .HasDefaultValueSql("((0))");

//                entity.Property(e => e.CountTv)
//                    .HasColumnName("count_tv")
//                    .HasDefaultValueSql("((0))");

//                entity.Property(e => e.CountWallet)
//                    .HasColumnName("count_wallet")
//                    .HasDefaultValueSql("((0))");

//                entity.Property(e => e.IdHo).HasColumnName("id_ho");

//                entity.Property(e => e.IdType).HasColumnName("id_type");

//                entity.Property(e => e.NameR)
//                    .IsRequired()
//                    .HasColumnName("name_r")
//                    .HasMaxLength(50);

//                entity.Property(e => e.Note).HasColumnName("note");

//                entity.Property(e => e.NumFloor)
//                    .HasColumnName("num_floor")
//                    .HasMaxLength(50)
//                    .HasDefaultValueSql("((0))");

//                entity.Property(e => e.PrivateFeatures).HasColumnName("private_features");

//                entity.Property(e => e.PublicFeatures).HasColumnName("public_features");

//                entity.Property(e => e.TypeCondition)
//                    .HasColumnName("type_condition")
//                    .HasMaxLength(50);

//                entity.HasOne(d => d.IdHoNavigation)
//                    .WithMany(p => p.RoomsTable)
//                    .HasForeignKey(d => d.IdHo)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("fk_rooms_hotels");

//                entity.HasOne(d => d.IdTypeNavigation)
//                    .WithMany(p => p.RoomsTable)
//                    .HasForeignKey(d => d.IdType)
//                    .HasConstraintName("FK_rooms_table_type_rooms_table");
//            });

//            modelBuilder.Entity<SettingReceptionTable>(entity =>
//            {
//                entity.ToTable("setting_reception_table");

//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.IdSub).HasColumnName("id_sub");

//                entity.Property(e => e.IntervalChangeroom)
//                    .HasColumnName("interval_changeroom")
//                    .HasDefaultValueSql("((0))");

//                entity.Property(e => e.IsCheckinTime)
//                    .HasColumnName("is_checkin_time")
//                    .HasDefaultValueSql("((0))");

//                entity.Property(e => e.IsCheckoutTime)
//                    .HasColumnName("is_checkout_time")
//                    .HasDefaultValueSql("((0))");

//                entity.Property(e => e.IsIntervalChangeroom)
//                    .HasColumnName("is_interval_changeroom")
//                    .HasDefaultValueSql("((0))");

//                entity.Property(e => e.IsMounthReceptionCheckout)
//                    .HasColumnName("is_mounth_reception_checkout")
//                    .HasDefaultValueSql("((0))");

//                entity.Property(e => e.TimeCheckin).HasColumnName("time_checkin");

//                entity.Property(e => e.TimeCheckout).HasColumnName("time_checkout");
//            });

//            modelBuilder.Entity<StatusCurrentTable>(entity =>
//            {
//                entity.ToTable("status_current_table");

//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.IdDetials).HasColumnName("id_detials");

//                entity.Property(e => e.IdRoom).HasColumnName("id_room");

//                entity.Property(e => e.Status)
//                    .HasColumnName("status")
//                    .HasMaxLength(50)
//                    .HasDefaultValueSql("('empty')");

//                entity.HasOne(d => d.IdDetialsNavigation)
//                    .WithMany(p => p.StatusCurrentTable)
//                    .HasForeignKey(d => d.IdDetials)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("fk_detials_status_status_current");

//                entity.HasOne(d => d.IdRoomNavigation)
//                    .WithMany(p => p.StatusCurrentTable)
//                    .HasForeignKey(d => d.IdRoom)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("fk_detials_status_room");
//            });

//            modelBuilder.Entity<TypeRoomsTable>(entity =>
//            {
//                entity.ToTable("type_rooms_table");

//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.IdSub).HasColumnName("id_sub");

//                entity.Property(e => e.NameT)
//                    .IsRequired()
//                    .HasColumnName("name_t")
//                    .HasMaxLength(50);
//            });

//            modelBuilder.Entity<UserTable>(entity =>
//            {
//                entity.ToTable("user_table");

//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.IdSub).HasColumnName("id_sub");

//                entity.Property(e => e.Password)
//                    .IsRequired()
//                    .HasColumnName("password")
//                    .HasMaxLength(50);

//                entity.Property(e => e.Role)
//                    .IsRequired()
//                    .HasColumnName("role")
//                    .HasMaxLength(50);

//                entity.Property(e => e.Username)
//                    .IsRequired()
//                    .HasColumnName("username")
//                    .HasMaxLength(50);
//            });

//            OnModelCreatingPartial(modelBuilder);
//        }

//        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

//        public DbSet<DataModels.AccountTable> AccountTable_1 { get; set; }

//        public DbSet<DataModels.GroupAccountTable> GroupAccountTable_1 { get; set; }

//        public DbSet<DataModels.ConditionReceptionTable> ConditionReceptionTable_1 { get; set; }

//        public DbSet<DataModels.GroupServicesTable> GroupServicesTable_1 { get; set; }

//        public DbSet<DataModels.SettingGeneralTable> SettingGeneralTable { get; set; }

//        public DbSet<DataModels.TaxGroupTable> TaxGroupTable { get; set; }

//        public DbSet<DataModels.HotelsTable> HotelsTable_1 { get; set; }
//    }
//}
