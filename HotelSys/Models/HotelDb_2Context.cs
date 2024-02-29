//using System;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata;

//#nullable disable

//namespace HotelSys.Models
//{
//    public partial class Hotel_alkheerContext : DbContext
//    {
//        public Hotel_alkheerContext()
//        {
//        }

//        public Hotel_alkheerContext(DbContextOptions<Hotel_alkheerContext> options)
//            : base(options)
//        {
//        }

//        public virtual DbSet<AccountTable> AccountTables { get; set; }
//        public virtual DbSet<AdminTable> AdminTables { get; set; }
//        public virtual DbSet<AreaTable> AreaTables { get; set; }
//        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
//        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }
//        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
//        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
//        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
//        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
//        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }
//        public virtual DbSet<BankTable> BankTables { get; set; }
//        public virtual DbSet<BillsTable> BillsTables { get; set; }
//        public virtual DbSet<BondTable> BondTables { get; set; }
//        public virtual DbSet<BoxsTable> BoxsTables { get; set; }
//        public virtual DbSet<BoxsUserTable> BoxsUserTables { get; set; }
//        public virtual DbSet<ChangeRoomTable> ChangeRoomTables { get; set; }
//        public virtual DbSet<CityTable> CityTables { get; set; }
//        public virtual DbSet<CompanyTable> CompanyTables { get; set; }
//        public virtual DbSet<ConditionReceptionTable> ConditionReceptionTables { get; set; }
//        public virtual DbSet<CurrencyTable> CurrencyTables { get; set; }
//        public virtual DbSet<CustomerTable> CustomerTables { get; set; }
//        public virtual DbSet<DetialsBillsTable> DetialsBillsTables { get; set; }
//        public virtual DbSet<DetialsHotelTable> DetialsHotelTables { get; set; }
//        public virtual DbSet<DetialsStatusTable> DetialsStatusTables { get; set; }
//        public virtual DbSet<EmpTable> EmpTables { get; set; }
//        public virtual DbSet<EntriesAccTable> EntriesAccTables { get; set; }
//        public virtual DbSet<FollowerReceptionTable> FollowerReceptionTables { get; set; }
//        public virtual DbSet<GroupAccountTable> GroupAccountTables { get; set; }
//        public virtual DbSet<GroupServicesTable> GroupServicesTables { get; set; }
//        public virtual DbSet<HotelsTable> OrgsTables { get; set; }
//        public virtual DbSet<ItemsExpensesTable> ItemsExpensesTables { get; set; }
//        public virtual DbSet<JobsNameTable> JobsNameTables { get; set; }
//        public virtual DbSet<JopEmpTable> JopEmpTables { get; set; }
//        public virtual DbSet<MyCustomer> MyCustomers { get; set; }
//        public virtual DbSet<OvertimeTable> OvertimeTables { get; set; }
//        public virtual DbSet<PriceRoomsTable> PriceRoomsTables { get; set; }
//        public virtual DbSet<ProductTable> ProductTables { get; set; }
//        public virtual DbSet<RecetionTable> RecetionTables { get; set; }
//        public virtual DbSet<RoomsTable> RoomsTables { get; set; }
//        public virtual DbSet<SettingGeneralTable> SettingGeneralTables { get; set; }
//        public virtual DbSet<SettingReceptionTable> SettingReceptionTables { get; set; }
//        public virtual DbSet<StatusCurrentTable> StatusCurrentTables { get; set; }
//        public virtual DbSet<TaxGroupTable> TaxGroupTables { get; set; }
//        public virtual DbSet<TypeRoomsTable> TypeRoomsTables { get; set; }
//        public virtual DbSet<UserTable> UserTables { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                // optionsBuilder.UseSqlServer("Server=.;Database=HotelDb_2;Trusted_Connection=True;");

//                // optionsBuilder.UseSqlServer("Server=.;Database=hotel_demo;Trusted_Connection=False;MultipleActiveResultSets=true;User ID=sa;Password=Ss123456------");
//                //  optionsBuilder.UseSqlServer("Server=154.12.237.211;Database=Hotel_talal_2;Trusted_Connection=False;MultipleActiveResultSets=true;User ID=sa;Password=orax055266");

                

//                optionsBuilder.UseSqlServer("Server=95.216.218.251,12356;Database=Hotel_alkheer;Trusted_Connection=False;MultipleActiveResultSets=true;User ID=sa;Password=orax055266");
//            }
//        }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

//            modelBuilder.Entity<AccountTable>(entity =>
//            {
//                entity.ToTable("account_table");

//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.Code).HasColumnName("code");

//                entity.Property(e => e.Createat)
//                    .HasColumnType("datetime")
//                    .HasColumnName("createat");

//                entity.Property(e => e.IdGroup).HasColumnName("id_group");

//                entity.Property(e => e.IsPrivate)
//                    .HasColumnName("is_private")
//                    .HasDefaultValueSql("((0))");

//                entity.Property(e => e.Name)
//                    .IsRequired()
//                    .HasMaxLength(100)
//                    .HasColumnName("name");

//                entity.Property(e => e.Status)
//                    .HasMaxLength(50)
//                    .HasColumnName("status")
//                    .HasDefaultValueSql("(N'active')");

//                entity.HasOne(d => d.IdGroupNavigation)
//                    .WithMany(p => p.AccountTables)
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
//                    .HasColumnType("date")
//                    .HasColumnName("lastdate_login");

//                entity.Property(e => e.Name)
//                    .IsRequired()
//                    .HasMaxLength(70)
//                    .HasColumnName("name");

//                entity.Property(e => e.Password)
//                    .IsRequired()
//                    .HasMaxLength(200)
//                    .HasColumnName("password");

//                entity.Property(e => e.Status)
//                    .HasColumnName("status")
//                    .HasDefaultValueSql("((1))");

//                entity.Property(e => e.Username)
//                    .IsRequired()
//                    .HasMaxLength(50)
//                    .HasColumnName("username");
//            });

//            modelBuilder.Entity<AreaTable>(entity =>
//            {
//                entity.ToTable("area_table");

//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.IdCity).HasColumnName("id_city");

//                entity.Property(e => e.Name)
//                    .IsRequired()
//                    .HasMaxLength(100)
//                    .HasColumnName("name");

//                entity.HasOne(d => d.IdCityNavigation)
//                    .WithMany(p => p.AreaTables)
//                    .HasForeignKey(d => d.IdCity)
//                    .HasConstraintName("FK_area_table_city_table");
//            });

//            modelBuilder.Entity<AspNetRole>(entity =>
//            {
//                entity.Property(e => e.Name).HasMaxLength(256);

//                entity.Property(e => e.NormalizedName).HasMaxLength(256);
//            });

//            modelBuilder.Entity<AspNetRoleClaim>(entity =>
//            {
//                entity.Property(e => e.RoleId)
//                    .IsRequired()
//                    .HasMaxLength(450);
//            });

//            modelBuilder.Entity<AspNetUser>(entity =>
//            {
//                entity.Property(e => e.Email).HasMaxLength(256);

//                entity.Property(e => e.FirstName).HasMaxLength(100);

//                entity.Property(e => e.LastName).HasMaxLength(100);

//                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

//                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

//                entity.Property(e => e.UserName).HasMaxLength(256);
//            });

//            modelBuilder.Entity<AspNetUserClaim>(entity =>
//            {
//                entity.Property(e => e.UserId)
//                    .IsRequired()
//                    .HasMaxLength(450);
//            });

//            modelBuilder.Entity<AspNetUserLogin>(entity =>
//            {
//                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

//                entity.Property(e => e.LoginProvider).HasMaxLength(128);

//                entity.Property(e => e.ProviderKey).HasMaxLength(128);

//                entity.Property(e => e.UserId)
//                    .IsRequired()
//                    .HasMaxLength(450);
//            });

//            modelBuilder.Entity<AspNetUserRole>(entity =>
//            {
//                entity.HasKey(e => new { e.UserId, e.RoleId });
//            });

//            modelBuilder.Entity<AspNetUserToken>(entity =>
//            {
//                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

//                entity.Property(e => e.LoginProvider).HasMaxLength(128);

//                entity.Property(e => e.Name).HasMaxLength(128);
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
//                    .HasMaxLength(300)
//                    .HasColumnName("name");

//                entity.HasOne(d => d.IdAccountNavigation)
//                    .WithMany(p => p.BankTables)
//                    .HasForeignKey(d => d.IdAccount)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("fk_bank_account");
//            });

//            modelBuilder.Entity<BillsTable>(entity =>
//            {
//                entity.ToTable("bills_table");

//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.Createat)
//                    .HasColumnType("datetime")
//                    .HasColumnName("createat");

//                entity.Property(e => e.CustomerOrCompany)
//                    .IsRequired()
//                    .HasMaxLength(50)
//                    .HasColumnName("customer_or_company")
//                    .HasDefaultValueSql("(N'customer')");

//                entity.Property(e => e.Date)
//                    .HasColumnType("datetime")
//                    .HasColumnName("date");

//                entity.Property(e => e.DeserveAmount)
//                    .HasColumnName("deserve_amount")
//                    .HasDefaultValueSql("((0))");

//                entity.Property(e => e.IdAccount).HasColumnName("id_account");

//                entity.Property(e => e.IdBank).HasColumnName("id_bank");

//                entity.Property(e => e.IdCurrancy).HasColumnName("id_currancy");

//                entity.Property(e => e.IdReception).HasColumnName("id_reception");

//                entity.Property(e => e.IncludeTax).HasColumnName("include_tax");

//                entity.Property(e => e.IsBaladiTax).HasColumnName("is_baladi_tax");

//                entity.Property(e => e.IsForRoom).HasColumnName("is_for_room");

//                entity.Property(e => e.Note).HasColumnName("note");

//                entity.Property(e => e.NumCard)
//                    .HasMaxLength(50)
//                    .HasColumnName("num_card");

//                entity.Property(e => e.NumCheck)
//                    .HasMaxLength(50)
//                    .HasColumnName("num_check");

//                entity.Property(e => e.NumReference)
//                    .HasMaxLength(100)
//                    .HasColumnName("num_reference");

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

//                entity.Property(e => e.TotalBaladiTaxPrice).HasColumnName("total_baladi_tax_price");

//                entity.Property(e => e.TotalBaladiTaxRate).HasColumnName("total_baladi_tax_rate");

//                entity.Property(e => e.TotalTaxPrice)
//                    .HasColumnName("total_tax_price")
//                    .HasDefaultValueSql("((0))");

//                entity.Property(e => e.TotalTaxRate)
//                    .HasColumnName("total_tax_rate")
//                    .HasDefaultValueSql("((0))");

//                entity.Property(e => e.Type)
//                    .IsRequired()
//                    .HasMaxLength(20)
//                    .HasColumnName("type");

//                entity.Property(e => e.TypeDiscount)
//                    .HasMaxLength(50)
//                    .HasColumnName("type_discount")
//                    .HasDefaultValueSql("('%')");

//                entity.Property(e => e.TypePay)
//                    .IsRequired()
//                    .HasMaxLength(20)
//                    .HasColumnName("type_pay");

//                entity.HasOne(d => d.IdAccountNavigation)
//                    .WithMany(p => p.BillsTables)
//                    .HasForeignKey(d => d.IdAccount)
//                    .HasConstraintName("fk_bills_account");

//                entity.HasOne(d => d.IdReceptionNavigation)
//                    .WithMany(p => p.BillsTables)
//                    .HasForeignKey(d => d.IdReception)
//                    .HasConstraintName("fk_bills_reception");
//            });

//            modelBuilder.Entity<BondTable>(entity =>
//            {
//                entity.ToTable("bond_table");

//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.Amount).HasColumnName("amount");

//                entity.Property(e => e.Createat)
//                    .HasColumnType("datetime")
//                    .HasColumnName("createat");

//                entity.Property(e => e.Date)
//                    .HasColumnType("datetime")
//                    .HasColumnName("date");

//                entity.Property(e => e.Hand).HasColumnName("hand");

//                entity.Property(e => e.IdAccount).HasColumnName("id_account");

//                entity.Property(e => e.IdBank).HasColumnName("id_bank");

//                entity.Property(e => e.IdBondPay).HasColumnName("id_bond_pay");

//                entity.Property(e => e.IdCurrancy).HasColumnName("id_currancy");

//                entity.Property(e => e.IdItemExpenses).HasColumnName("id_item_expenses");

//                entity.Property(e => e.IdReception).HasColumnName("id_reception");

//                entity.Property(e => e.IsDonePay).HasColumnName("is_done_pay");

//                entity.Property(e => e.LocPay)
//                    .HasMaxLength(300)
//                    .HasColumnName("loc_pay");

//                entity.Property(e => e.Note).HasColumnName("note");

//                entity.Property(e => e.NumCard)
//                    .HasMaxLength(50)
//                    .HasColumnName("num_card");

//                entity.Property(e => e.NumCheck)
//                    .HasMaxLength(50)
//                    .HasColumnName("num_check");

//                entity.Property(e => e.NumReference)
//                    .HasMaxLength(100)
//                    .HasColumnName("num_reference");

//                entity.Property(e => e.Time).HasColumnName("time");

//                entity.Property(e => e.Type)
//                    .IsRequired()
//                    .HasMaxLength(20)
//                    .HasColumnName("type");

//                entity.Property(e => e.TypePay)
//                    .IsRequired()
//                    .HasMaxLength(20)
//                    .HasColumnName("type_pay");

//                entity.Property(e => e.Why).HasColumnName("why");

//                entity.Property(e => e.WorthyDate)
//                    .HasColumnType("datetime")
//                    .HasColumnName("worthy_date");

//                entity.HasOne(d => d.IdAccountNavigation)
//                    .WithMany(p => p.BondTables)
//                    .HasForeignKey(d => d.IdAccount)
//                    .HasConstraintName("fk_bond_account");

//                entity.HasOne(d => d.IdBondPayNavigation)
//                    .WithMany(p => p.InverseIdBondPayNavigation)
//                    .HasForeignKey(d => d.IdBondPay)
//                    .HasConstraintName("fk_bond_bond");

//                entity.HasOne(d => d.IdItemExpensesNavigation)
//                    .WithMany(p => p.BondTables)
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

//                entity.Property(e => e.IsPrivate).HasColumnName("is_private");

//                entity.Property(e => e.Name)
//                    .IsRequired()
//                    .HasMaxLength(300)
//                    .HasColumnName("name");

//                entity.HasOne(d => d.IdAccountNavigation)
//                    .WithMany(p => p.BoxsTables)
//                    .HasForeignKey(d => d.IdAccount)
//                    .HasConstraintName("fk_boxs_account");
//            });

//            modelBuilder.Entity<BoxsUserTable>(entity =>
//            {
//                entity.ToTable("boxs_user_table");

//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.IdAspUser)
//                    .IsRequired()
//                    .HasMaxLength(450)
//                    .HasColumnName("id_aspUser");

//                entity.Property(e => e.IdBox).HasColumnName("id_box");

//                entity.Property(e => e.IsDefult)
//                    .HasColumnName("is_defult")
//                    .HasDefaultValueSql("((0))");

//                entity.HasOne(d => d.IdAspUserNavigation)
//                    .WithMany(p => p.BoxsUserTables)
//                    .HasForeignKey(d => d.IdAspUser)
//                    .HasConstraintName("FK_boxs_user_table_AspNetUsers");

//                entity.HasOne(d => d.IdBoxNavigation)
//                    .WithMany(p => p.BoxsUserTables)
//                    .HasForeignKey(d => d.IdBox)
//                    .HasConstraintName("FK_boxs_user_table_boxs_table");
//            });

//            modelBuilder.Entity<ChangeRoomTable>(entity =>
//            {
//                entity.ToTable("change_room_table");

//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.Date)
//                    .HasColumnType("datetime")
//                    .HasColumnName("date");

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

//            modelBuilder.Entity<CityTable>(entity =>
//            {
//                entity.ToTable("city_table");

//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.Name)
//                    .IsRequired()
//                    .HasColumnName("name");
//            });

//            modelBuilder.Entity<CompanyTable>(entity =>
//            {
//                entity.ToTable("company_table");

//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.IdAccount).HasColumnName("id_account");

//                entity.Property(e => e.IdSub).HasColumnName("id_sub");

//                entity.Property(e => e.Name)
//                    .IsRequired()
//                    .HasMaxLength(50)
//                    .HasColumnName("name");

//                entity.HasOne(d => d.IdAccountNavigation)
//                    .WithMany(p => p.CompanyTables)
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
//                    .HasMaxLength(5)
//                    .HasColumnName("code");

//                entity.Property(e => e.IdSub).HasColumnName("id_sub");

//                entity.Property(e => e.IsDefault)
//                    .HasColumnName("is_default")
//                    .HasDefaultValueSql("((0))");

//                entity.Property(e => e.Name)
//                    .IsRequired()
//                    .HasMaxLength(300)
//                    .HasColumnName("name");

//                entity.Property(e => e.RateConvert)
//                    .HasColumnName("rate_convert")
//                    .HasDefaultValueSql("((1))");
//            });

//            modelBuilder.Entity<CustomerTable>(entity =>
//            {
//                entity.ToTable("customer_table");

//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.Createat)
//                    .HasColumnType("datetime")
//                    .HasColumnName("createat");

//                entity.Property(e => e.Email).HasColumnName("email");

//                entity.Property(e => e.EndDate)
//                    .HasColumnType("datetime")
//                    .HasColumnName("end_date");

//                entity.Property(e => e.IdArea).HasColumnName("id_area");

//                entity.Property(e => e.LocRelease)
//                    .HasMaxLength(300)
//                    .HasColumnName("loc_release");

//                entity.Property(e => e.LocWork)
//                    .HasMaxLength(100)
//                    .HasColumnName("loc_work");

//                entity.Property(e => e.Name)
//                    .IsRequired()
//                    .HasColumnName("name");

//                entity.Property(e => e.Nationality)
//                    .IsRequired()
//                    .HasMaxLength(50)
//                    .HasColumnName("nationality");

//                entity.Property(e => e.NumProof)
//                    .IsRequired()
//                    .HasMaxLength(300)
//                    .HasColumnName("num_proof");

//                entity.Property(e => e.PhoneWork)
//                    .HasMaxLength(100)
//                    .HasColumnName("phone_work");

//                entity.Property(e => e.PublicNote).HasColumnName("public_note");

//                entity.Property(e => e.ReleaseDate)
//                    .HasColumnType("datetime")
//                    .HasColumnName("release_date");

//                entity.Property(e => e.Sex)
//                    .HasMaxLength(10)
//                    .HasColumnName("sex")
//                    .HasDefaultValueSql("('male')");

//                entity.Property(e => e.Type)
//                    .IsRequired()
//                    .HasMaxLength(10)
//                    .HasColumnName("type");

//                entity.Property(e => e.TypeProof)
//                    .IsRequired()
//                    .HasMaxLength(30)
//                    .HasColumnName("type_proof");

//                entity.Property(e => e.TypeWork)
//                    .HasMaxLength(100)
//                    .HasColumnName("type_work");

//                entity.HasOne(d => d.IdAreaNavigation)
//                    .WithMany(p => p.CustomerTables)
//                    .HasForeignKey(d => d.IdArea)
//                    .HasConstraintName("FK_customer_table_area_table");
//            });

//            modelBuilder.Entity<DetialsBillsTable>(entity =>
//            {
//                entity.ToTable("detials_bills_table");

//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.BaladiTaxPrice).HasColumnName("baladi_tax_price");

//                entity.Property(e => e.BaladiTaxRate).HasColumnName("baladi_tax_rate");

//                entity.Property(e => e.IdBill).HasColumnName("id_bill");

//                entity.Property(e => e.IdProduct).HasColumnName("id_product");

//                entity.Property(e => e.IsBaladiTax).HasColumnName("is_baladi_tax");

//                entity.Property(e => e.PriceOne)
//                    .HasColumnName("price_one")
//                    .HasDefaultValueSql("((0))");

//                entity.Property(e => e.Qty)
//                    .HasColumnName("qty")
//                    .HasDefaultValueSql("((0))");

//                entity.Property(e => e.TaxPrice)
//                    .HasColumnName("tax_price")
//                    .HasDefaultValueSql("((0))");

//                entity.Property(e => e.TaxRate)
//                    .HasColumnName("tax_rate")
//                    .HasDefaultValueSql("((0))");

//                entity.Property(e => e.Total)
//                    .HasColumnName("total")
//                    .HasDefaultValueSql("((0))");

//                entity.HasOne(d => d.IdBillNavigation)
//                    .WithMany(p => p.DetialsBillsTables)
//                    .HasForeignKey(d => d.IdBill)
//                    .HasConstraintName("fk_bills_detials_bills");

//                entity.HasOne(d => d.IdProductNavigation)
//                    .WithMany(p => p.DetialsBillsTables)
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
//                    .WithMany(p => p.DetialsHotelTables)
//                    .HasForeignKey(d => d.IdHo)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("fk_detials_hotel_hotels");
//            });

//            modelBuilder.Entity<DetialsStatusTable>(entity =>
//            {
//                entity.ToTable("detials_status_table");

//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.Createat)
//                    .HasColumnType("datetime")
//                    .HasColumnName("createat");

//                entity.Property(e => e.Detials).HasColumnName("detials");

//                entity.Property(e => e.EndDate).HasColumnName("end_date");

//                entity.Property(e => e.IdEmp).HasColumnName("id_emp");

//                entity.Property(e => e.IdReception).HasColumnName("id_reception");

//                entity.Property(e => e.IdRoom).HasColumnName("id_room");

//                entity.Property(e => e.IdStatusCurrent).HasColumnName("id_status_current");

//                entity.Property(e => e.IdSub).HasColumnName("id_sub");

//                entity.Property(e => e.StartDate).HasColumnName("start_date");

//                entity.Property(e => e.Status)
//                    .HasMaxLength(50)
//                    .HasColumnName("status")
//                    .HasDefaultValueSql("('empty')");
//            });

//            modelBuilder.Entity<EmpTable>(entity =>
//            {
//                entity.ToTable("emp_table");

//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.Email)
//                    .HasMaxLength(50)
//                    .HasColumnName("email");

//                entity.Property(e => e.IdSub).HasColumnName("id_sub");

//                entity.Property(e => e.Img)
//                    .HasMaxLength(300)
//                    .HasColumnName("img");

//                entity.Property(e => e.Name)
//                    .IsRequired()
//                    .HasMaxLength(50)
//                    .HasColumnName("name");

//                entity.Property(e => e.NumIdentity)
//                    .HasMaxLength(50)
//                    .HasColumnName("num_identity");

//                entity.Property(e => e.Phone)
//                    .IsRequired()
//                    .HasMaxLength(15)
//                    .HasColumnName("phone");

//                entity.Property(e => e.Sex)
//                    .HasMaxLength(10)
//                    .HasColumnName("sex");
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
//                    .HasMaxLength(10)
//                    .HasColumnName("bill_or_band");

//                entity.Property(e => e.Date)
//                    .HasColumnType("datetime")
//                    .HasColumnName("date");

//                entity.Property(e => e.DebtOrCredit)
//                    .IsRequired()
//                    .HasMaxLength(1)
//                    .HasColumnName("debt_or_Credit");

//                entity.Property(e => e.IdAccount).HasColumnName("id_account");

//                entity.Property(e => e.IdCurrancy).HasColumnName("id_currancy");

//                entity.Property(e => e.IdDocumentBill).HasColumnName("id_document_bill");

//                entity.Property(e => e.IdDocumentDand).HasColumnName("id_document_dand");

//                entity.Property(e => e.IdRecetion).HasColumnName("id_recetion");

//                entity.Property(e => e.Note).HasColumnName("note");

//                entity.Property(e => e.TypeDocument)
//                    .IsRequired()
//                    .HasMaxLength(50)
//                    .HasColumnName("type_document");

//                entity.HasOne(d => d.IdAccountNavigation)
//                    .WithMany(p => p.EntriesAccTables)
//                    .HasForeignKey(d => d.IdAccount)
//                    .HasConstraintName("fk_entries_acc_account");

//                entity.HasOne(d => d.IdDocumentBillNavigation)
//                    .WithMany(p => p.EntriesAccTables)
//                    .HasForeignKey(d => d.IdDocumentBill)
//                    .HasConstraintName("FK_entries_acc_table_bills_table");
//            });

//            modelBuilder.Entity<FollowerReceptionTable>(entity =>
//            {
//                entity.ToTable("follower_reception_table");

//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.CuType)
//                    .IsRequired()
//                    .HasMaxLength(10)
//                    .HasColumnName("cu_type");

//                entity.Property(e => e.Duration)
//                    .HasMaxLength(1)
//                    .HasColumnName("duration");

//                entity.Property(e => e.DurationFrom)
//                    .HasColumnType("datetime")
//                    .HasColumnName("duration_from");

//                entity.Property(e => e.DurationTo)
//                    .HasColumnType("datetime")
//                    .HasColumnName("duration_to");

//                entity.Property(e => e.IdCustomer).HasColumnName("id_customer");

//                entity.Property(e => e.IdReceptoin).HasColumnName("id_receptoin");

//                entity.Property(e => e.Relation)
//                    .IsRequired()
//                    .HasMaxLength(50)
//                    .HasColumnName("relation");

//                entity.HasOne(d => d.IdCustomerNavigation)
//                    .WithMany(p => p.FollowerReceptionTables)
//                    .HasForeignKey(d => d.IdCustomer)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK_follower_reception_table_customer_table");

//                entity.HasOne(d => d.IdReceptoinNavigation)
//                    .WithMany(p => p.FollowerReceptionTables)
//                    .HasForeignKey(d => d.IdReceptoin)
//                    .HasConstraintName("fk_customers_reception_reception");
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
//                    .HasMaxLength(100)
//                    .HasColumnName("name");

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
//                    .HasColumnName("name");

//                entity.Property(e => e.NameEn).HasColumnName("name_en");
//            });

//            modelBuilder.Entity<HotelsTable>(entity =>
//            {
//                entity.ToTable("hotels_table");

//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.Address)
//                    .HasMaxLength(150)
//                    .HasColumnName("address");

//                entity.Property(e => e.City)
//                    .IsRequired()
//                    .HasMaxLength(50)
//                    .HasColumnName("city");

//                entity.Property(e => e.Country)
//                    .IsRequired()
//                    .HasMaxLength(50)
//                    .HasColumnName("country");

//                entity.Property(e => e.Email).HasColumnName("email");

//                entity.Property(e => e.IdSub).HasColumnName("id_sub");

//                entity.Property(e => e.Logo).HasColumnName("logo");

//                entity.Property(e => e.MailBox)
//                    .HasMaxLength(150)
//                    .HasColumnName("mail_box");

//                entity.Property(e => e.NameH)
//                    .IsRequired()
//                    .HasMaxLength(50)
//                    .HasColumnName("name_h");

//                entity.Property(e => e.NumEn)
//                    .HasMaxLength(50)
//                    .HasColumnName("num_en");

//                entity.Property(e => e.Phone)
//                    .IsRequired()
//                    .HasColumnName("phone");

//                entity.Property(e => e.Regin)
//                    .IsRequired()
//                    .HasMaxLength(50)
//                    .HasColumnName("regin");

//                entity.Property(e => e.Website).HasColumnName("website");
//            });

//            modelBuilder.Entity<ItemsExpensesTable>(entity =>
//            {
//                entity.ToTable("items_expenses_table");

//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.CreateAt)
//                    .HasColumnType("datetime")
//                    .HasColumnName("create_at");

//                entity.Property(e => e.IdAccount).HasColumnName("id_account");

//                entity.Property(e => e.IdSub).HasColumnName("id_sub");

//                entity.Property(e => e.Name)
//                    .IsRequired()
//                    .HasMaxLength(300)
//                    .HasColumnName("name");

//                entity.HasOne(d => d.IdAccountNavigation)
//                    .WithMany(p => p.ItemsExpensesTables)
//                    .HasForeignKey(d => d.IdAccount)
//                    .HasConstraintName("FK_items_expenses_table_account_table");
//            });

//            modelBuilder.Entity<JobsNameTable>(entity =>
//            {
//                entity.ToTable("jobs_name_table");

//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.IdSub).HasColumnName("id_sub");

//                entity.Property(e => e.Name)
//                    .IsRequired()
//                    .HasMaxLength(300)
//                    .HasColumnName("name");
//            });

//            modelBuilder.Entity<JopEmpTable>(entity =>
//            {
//                entity.ToTable("jop_emp_table");

//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.IdEmp).HasColumnName("id_emp");

//                entity.Property(e => e.IdJobName).HasColumnName("id_job_name");

//                entity.HasOne(d => d.IdEmpNavigation)
//                    .WithMany(p => p.JopEmpTableIdEmpNavigations)
//                    .HasForeignKey(d => d.IdEmp)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("fk_jop_emp_emp");

//                entity.HasOne(d => d.IdJobNameNavigation)
//                    .WithMany(p => p.JopEmpTableIdJobNameNavigations)
//                    .HasForeignKey(d => d.IdJobName)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("fk_jop_emp_job_");
//            });

//            modelBuilder.Entity<MyCustomer>(entity =>
//            {
//                entity.ToTable("my_customers");

//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.Createat)
//                    .HasColumnType("datetime")
//                    .HasColumnName("createat");

//                entity.Property(e => e.IdAccount).HasColumnName("id_account");

//                entity.Property(e => e.IdCustomer).HasColumnName("id_customer");

//                entity.Property(e => e.Idsub).HasColumnName("idsub");

//                entity.Property(e => e.PrivateNote).HasColumnName("private_note");

//                entity.Property(e => e.VisitEndDate)
//                    .HasColumnType("datetime")
//                    .HasColumnName("visit_end_date");

//                entity.HasOne(d => d.IdAccountNavigation)
//                    .WithMany(p => p.MyCustomers)
//                    .HasForeignKey(d => d.IdAccount)
//                    .OnDelete(DeleteBehavior.Cascade)
//                    .HasConstraintName("FK_my_customers_account_table");

//                entity.HasOne(d => d.IdCustomerNavigation)
//                    .WithMany(p => p.MyCustomers)
//                    .HasForeignKey(d => d.IdCustomer)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK_my_customers_customer_table");
//            });

//            modelBuilder.Entity<OvertimeTable>(entity =>
//            {
//                entity.ToTable("overtime_table");

//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.Createat)
//                    .HasColumnType("datetime")
//                    .HasColumnName("createat");

//                entity.Property(e => e.EndDate)
//                    .HasColumnType("datetime")
//                    .HasColumnName("end_date");

//                entity.Property(e => e.EndTime).HasColumnName("end_time");

//                entity.Property(e => e.IdSub).HasColumnName("id_sub");

//                entity.Property(e => e.IdUser).HasColumnName("id_user");

//                entity.Property(e => e.StartDate)
//                    .HasColumnType("datetime")
//                    .HasColumnName("start_date");

//                entity.Property(e => e.StartTime).HasColumnName("start_time");
//            });

//            modelBuilder.Entity<PriceRoomsTable>(entity =>
//            {
//                entity.ToTable("price_rooms_table");

//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.IdRoom).HasColumnName("id_room");

//                entity.Property(e => e.IdSub).HasColumnName("id_sub");

//                entity.Property(e => e.IdTaxGroup)
//                    .HasColumnName("id_tax_group")
//                    .HasDefaultValueSql("((0))");

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
//                    .WithMany(p => p.PriceRoomsTables)
//                    .HasForeignKey(d => d.IdRoom)
//                    .HasConstraintName("fk_rooms_price_rooms");

//                entity.HasOne(d => d.IdTaxGroupNavigation)
//                    .WithMany(p => p.PriceRoomsTables)
//                    .HasForeignKey(d => d.IdTaxGroup)
//                    .HasConstraintName("FK_price_rooms_table_tax_group_table");
//            });

//            modelBuilder.Entity<ProductTable>(entity =>
//            {
//                entity.ToTable("product_table");

//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.IdGroup).HasColumnName("id_group");

//                entity.Property(e => e.IdTaxGroup).HasColumnName("id_tax_group");

//                entity.Property(e => e.Name)
//                    .IsRequired()
//                    .HasColumnName("name");

//                entity.Property(e => e.NameEn).HasColumnName("name_en");

//                entity.Property(e => e.Price).HasColumnName("price");

//                entity.HasOne(d => d.IdGroupNavigation)
//                    .WithMany(p => p.ProductTables)
//                    .HasForeignKey(d => d.IdGroup)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("fk_group_services_product_services");

//                entity.HasOne(d => d.IdTaxGroupNavigation)
//                    .WithMany(p => p.ProductTables)
//                    .HasForeignKey(d => d.IdTaxGroup)
//                    .HasConstraintName("FK_product_table_tax_group_table");
//            });

//            modelBuilder.Entity<RecetionTable>(entity =>
//            {
//                entity.ToTable("recetion_table");

//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.AreaFrom).HasColumnName("area_from");

//                entity.Property(e => e.ChechoutDate)
//                    .HasColumnType("datetime")
//                    .HasColumnName("chechout_date");

//                entity.Property(e => e.CheckinDate)
//                    .HasColumnType("datetime")
//                    .HasColumnName("checkin_date");

//                entity.Property(e => e.EndDate)
//                    .HasColumnType("datetime")
//                    .HasColumnName("end_date");

//                entity.Property(e => e.IdCo).HasColumnName("id_co");

//                entity.Property(e => e.IdMyCustomer).HasColumnName("id_my_customer");

//                entity.Property(e => e.IdRoom).HasColumnName("id_room");

//                entity.Property(e => e.IsChechin)
//                    .HasColumnName("is_chechin")
//                    .HasDefaultValueSql("((0))");

//                entity.Property(e => e.IsChechout)
//                    .HasColumnName("is_chechout")
//                    .HasDefaultValueSql("((0))");

//                entity.Property(e => e.Note).HasColumnName("note");

//                entity.Property(e => e.Price)
//                    .HasColumnName("price")
//                    .HasDefaultValueSql("((0))");

//                entity.Property(e => e.QtyTime)
//                    .HasColumnName("qty_time")
//                    .HasDefaultValueSql("((0))");

//                entity.Property(e => e.Source)
//                    .IsRequired()
//                    .HasMaxLength(50)
//                    .HasColumnName("source");

//                entity.Property(e => e.StartDate)
//                    .HasColumnType("datetime")
//                    .HasColumnName("start_date");

//                entity.Property(e => e.Status).HasColumnName("status");

//                entity.Property(e => e.TypeDate)
//                    .HasMaxLength(1)
//                    .HasColumnName("type_date")
//                    .HasDefaultValueSql("('m')");

//                entity.Property(e => e.Unit)
//                    .IsRequired()
//                    .HasMaxLength(50)
//                    .HasColumnName("unit");

//                entity.Property(e => e.WhyVisit).HasColumnName("why_visit");

//                entity.HasOne(d => d.IdCoNavigation)
//                    .WithMany(p => p.RecetionTables)
//                    .HasForeignKey(d => d.IdCo)
//                    .HasConstraintName("fk_company_recetion");

//                entity.HasOne(d => d.IdMyCustomerNavigation)
//                    .WithMany(p => p.RecetionTables)
//                    .HasForeignKey(d => d.IdMyCustomer)
//                    .OnDelete(DeleteBehavior.Cascade)
//                    .HasConstraintName("FK_recetion_table_my_customers");

//                entity.HasOne(d => d.IdRoomNavigation)
//                    .WithMany(p => p.RecetionTables)
//                    .HasForeignKey(d => d.IdRoom)
//                    .HasConstraintName("FK_recetion_table_rooms_table");
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
//                    .HasMaxLength(50)
//                    .HasColumnName("name_r");

//                entity.Property(e => e.Note).HasColumnName("note");

//                entity.Property(e => e.NumFloor)
//                    .HasMaxLength(50)
//                    .HasColumnName("num_floor")
//                    .HasDefaultValueSql("((0))");

//                entity.Property(e => e.PrivateFeatures).HasColumnName("private_features");

//                entity.Property(e => e.PublicFeatures).HasColumnName("public_features");

//                entity.Property(e => e.TypeCondition)
//                    .HasMaxLength(50)
//                    .HasColumnName("type_condition");

//                entity.HasOne(d => d.IdHoNavigation)
//                    .WithMany(p => p.RoomsTables)
//                    .HasForeignKey(d => d.IdHo)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("fk_rooms_hotels");

//                entity.HasOne(d => d.IdTypeNavigation)
//                    .WithMany(p => p.RoomsTables)
//                    .HasForeignKey(d => d.IdType)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("fk_rooms_type_rooms");
//            });

//            modelBuilder.Entity<SettingGeneralTable>(entity =>
//            {
//                entity.ToTable("setting_general_table");

//                entity.Property(e => e.Id)
//                    .ValueGeneratedNever()
//                    .HasColumnName("id");

//                entity.Property(e => e.ServicesIncludeTax).HasColumnName("services_include_tax");
//            });

//            modelBuilder.Entity<SettingReceptionTable>(entity =>
//            {
//                entity.ToTable("setting_reception_table");

//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.IdSub).HasColumnName("id_sub");

//                entity.Property(e => e.IncudePriceTax)
//                    .HasColumnName("incude_price_tax")
//                    .HasDefaultValueSql("((1))");

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

//                entity.Property(e => e.Createat)
//                    .HasColumnType("datetime")
//                    .HasColumnName("createat");

//                entity.Property(e => e.Detials).HasColumnName("detials");

//                entity.Property(e => e.EndDate).HasColumnName("end_date");

//                entity.Property(e => e.IdEmp).HasColumnName("id_emp");

//                entity.Property(e => e.IdReception).HasColumnName("id_reception");

//                entity.Property(e => e.IdRoom).HasColumnName("id_room");

//                entity.Property(e => e.StartDate).HasColumnName("start_date");

//                entity.Property(e => e.Status)
//                    .HasMaxLength(50)
//                    .HasColumnName("status")
//                    .HasDefaultValueSql("('empty')");

//                entity.HasOne(d => d.IdRoomNavigation)
//                    .WithMany(p => p.StatusCurrentTables)
//                    .HasForeignKey(d => d.IdRoom)
//                    .HasConstraintName("fk_detials_status_room");
//            });

//            modelBuilder.Entity<TaxGroupTable>(entity =>
//            {
//                entity.ToTable("tax_group_table");

//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.BaladiRate).HasColumnName("baladi_rate");

//                entity.Property(e => e.IdUser).HasColumnName("id_user");

//                entity.Property(e => e.IsBaladiTax).HasColumnName("is_baladi_tax");

//                entity.Property(e => e.Name)
//                    .IsRequired()
//                    .HasColumnName("name");

//                entity.Property(e => e.NameEn)
//                    .HasMaxLength(200)
//                    .HasColumnName("name_en");

//                entity.Property(e => e.Rate).HasColumnName("rate");
//            });

//            modelBuilder.Entity<TypeRoomsTable>(entity =>
//            {
//                entity.ToTable("type_rooms_table");

//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.Color)
//                    .HasMaxLength(50)
//                    .HasColumnName("color")
//                    .HasDefaultValueSql("('#FFFFFF')");

//                entity.Property(e => e.IdSub).HasColumnName("id_sub");

//                entity.Property(e => e.NameT)
//                    .IsRequired()
//                    .HasColumnName("name_t");
//            });

//            modelBuilder.Entity<UserTable>(entity =>
//            {
//                entity.ToTable("user_table");

//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.IdSub).HasColumnName("id_sub");

//                entity.Property(e => e.Password)
//                    .IsRequired()
//                    .HasMaxLength(50)
//                    .HasColumnName("password");

//                entity.Property(e => e.Role)
//                    .IsRequired()
//                    .HasMaxLength(50)
//                    .HasColumnName("role");

//                entity.Property(e => e.Username)
//                    .IsRequired()
//                    .HasMaxLength(50)
//                    .HasColumnName("username");
//            });

//            OnModelCreatingPartial(modelBuilder);
//        }

//        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
//    }
//}
