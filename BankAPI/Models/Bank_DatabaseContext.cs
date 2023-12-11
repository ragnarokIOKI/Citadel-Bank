using System;
using System.Collections.Generic;
using BankAPI.Models.BankAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BankAPI.Models
{
    public partial class Bank_DatabaseContext : DbContext
    {
        public Bank_DatabaseContext()
        {
        }

        public Bank_DatabaseContext(DbContextOptions<Bank_DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<AccountApplication> AccountApplications { get; set; } = null!;
        public virtual DbSet<AccountApplicationHistory> AccountApplicationHistories { get; set; } = null!;
        public virtual DbSet<AccountType> AccountTypes { get; set; } = null!;
        public virtual DbSet<ApplicationStatus> ApplicationStatuses { get; set; } = null!;
        public virtual DbSet<Card> Cards { get; set; } = null!;
        public virtual DbSet<CreditAgreement> CreditAgreements { get; set; } = null!;
        public virtual DbSet<CreditAgreementHistory> CreditAgreementHistories { get; set; } = null!;
        public virtual DbSet<CreditApplication> CreditApplications { get; set; } = null!;
        public virtual DbSet<CreditApplicationHistory> CreditApplicationHistories { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Transaction> Transactions { get; set; } = null!;
        public virtual DbSet<TransactionType> TransactionTypes { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-8J6N5LS;Initial Catalog=Bank_Database;Integrated Security=True;Trust Server Certificate=True", builder =>
            {
                builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            });
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.IdAccount);

                entity.ToTable("Account");

                entity.HasIndex(e => e.IdAccount, "UQ__Account__213379EACA525188")
                    .IsUnique();

                entity.Property(e => e.IdAccount)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ID_Account");

                entity.Property(e => e.AccountDeleted)
                    .IsRequired()
                    .HasColumnName("Account_Deleted")
                    .HasDefaultValueSql("('1')");

                entity.Property(e => e.Balance).HasColumnType("decimal(38, 2)");

                entity.Property(e => e.TypeId).HasColumnName("Type_ID");

                entity.Property(e => e.UserAccountId).HasColumnName("User_Account_ID");
            });

            modelBuilder.Entity<AccountApplication>(entity =>
            {
                entity.HasKey(e => e.IdAccountApplication);

                entity.ToTable("Account_Application");

                entity.HasIndex(e => e.IdAccountApplication, "UQ__Account___5FC10316043C0245")
                    .IsUnique();

                entity.Property(e => e.IdAccountApplication)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ID_Account_Application");

                entity.Property(e => e.AccountApplicationUserId).HasColumnName("AccountApplication_User_ID");

                entity.Property(e => e.AccountDesiredPercentage)
                    .HasColumnType("decimal(38, 2)")
                    .HasColumnName("Account_Desired_Percentage");

                entity.Property(e => e.StatusId).HasColumnName("Status_ID");

                entity.Property(e => e.TypeAccountId).HasColumnName("Type_Account_ID");

            });

            modelBuilder.Entity<AccountApplicationHistory>(entity =>
            {
                entity.HasKey(e => e.IdAccountApplicationHistory)
                    .HasName("PK__Account___4896E4AF4C9610AC");

                entity.ToTable("Account_Application_History");

                entity.Property(e => e.IdAccountApplicationHistory).HasColumnName("ID_Account_Application_History");

                entity.Property(e => e.AccountApplicationId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Account_Application_ID");

                entity.Property(e => e.ChangeType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Change_Type");

                entity.Property(e => e.ChangedColumn)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Changed_Column");

                entity.Property(e => e.DateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_Time");

                entity.Property(e => e.NewValue)
                    .IsUnicode(false)
                    .HasColumnName("New_Value")
                    .HasDefaultValueSql("('-')");

                entity.Property(e => e.OldValue)
                    .IsUnicode(false)
                    .HasColumnName("Old_Value")
                    .HasDefaultValueSql("('-')");
            });

            modelBuilder.Entity<AccountType>(entity =>
            {
                entity.HasKey(e => e.IdAccountType);

                entity.ToTable("Account_Type");

                entity.HasIndex(e => e.NameAccountType, "UQ_Account_Type")
                    .IsUnique();

                entity.Property(e => e.IdAccountType).HasColumnName("ID_Account_Type");

                entity.Property(e => e.AccountTypeDeleted)
                    .IsRequired()
                    .HasColumnName("AccountType_Deleted")
                    .HasDefaultValueSql("('1')");

                entity.Property(e => e.NameAccountType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Name_Account_Type");
            });

            modelBuilder.Entity<ApplicationStatus>(entity =>
            {
                entity.HasKey(e => e.IdApplicationStatus);

                entity.ToTable("Application_Status");

                entity.HasIndex(e => e.NameStatus, "UQ_Name_Status")
                    .IsUnique();

                entity.Property(e => e.IdApplicationStatus).HasColumnName("ID_Application_Status");

                entity.Property(e => e.NameStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Name_Status");
            });

            modelBuilder.Entity<Card>(entity =>
            {
                entity.HasKey(e => e.CardNumber)
                    .HasName("PK_Card_Number");

                entity.ToTable("Card");

                entity.HasIndex(e => e.CardNumber, "UQ__Card__CD2FF786D1D7304C")
                    .IsUnique();

                entity.Property(e => e.CardNumber)
                    .HasMaxLength(19)
                    .IsUnicode(false)
                    .HasColumnName("Card_Number");

                entity.Property(e => e.CardAccountId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CardAccount_ID");

                entity.Property(e => e.CardDeleted)
                    .IsRequired()
                    .HasColumnName("Card_Deleted")
                    .HasDefaultValueSql("('1')");

                entity.Property(e => e.CardHolder)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Card_Holder");

                entity.Property(e => e.Ccv)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("CCV");

                entity.Property(e => e.Validity)
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CreditAgreement>(entity =>
            {
                entity.HasKey(e => e.IdCreditAgreement);

                entity.ToTable("Credit_Agreement");

                entity.HasIndex(e => e.IdCreditAgreement, "UQ__Credit_A__4287954AEE9B80FC")
                    .IsUnique();

                entity.Property(e => e.IdCreditAgreement)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ID_Credit_Agreement");

                entity.Property(e => e.CreditAgreementDeleted)
                    .IsRequired()
                    .HasColumnName("CreditAgreement_Deleted")
                    .HasDefaultValueSql("('1')");

                entity.Property(e => e.CreditAgreementUserId).HasColumnName("CreditAgreement_User_ID");

                entity.Property(e => e.CreditApplicationId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Credit_Application_ID");

                entity.Property(e => e.CreditRate)
                    .HasColumnType("decimal(38, 2)")
                    .HasColumnName("Credit_Rate");

                entity.Property(e => e.DateDrawing)
                    .HasColumnType("date")
                    .HasColumnName("Date_Drawing");

                entity.Property(e => e.DateTermination)
                    .HasColumnType("date")
                    .HasColumnName("Date_Termination");

                modelBuilder.Entity<OverpaymentDTO>().HasNoKey();
            });

            modelBuilder.Entity<CreditAgreementHistory>(entity =>
            {
                entity.HasKey(e => e.IdCreditAgreementHistory)
                    .HasName("PK__Credit_A__4B06C24D458F9C5D");

                entity.ToTable("Credit_Agreement_History");

                entity.Property(e => e.IdCreditAgreementHistory).HasColumnName("ID_Credit_Agreement_History");

                entity.Property(e => e.ChangeType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Change_Type");

                entity.Property(e => e.ChangedColumn)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Changed_Column");

                entity.Property(e => e.CreditAgreementId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Credit_Agreement_ID");

                entity.Property(e => e.DateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_Time");

                entity.Property(e => e.NewValue)
                    .IsUnicode(false)
                    .HasColumnName("New_Value");

                entity.Property(e => e.OldValue)
                    .IsUnicode(false)
                    .HasColumnName("Old_Value");
            });

            modelBuilder.Entity<CreditApplication>(entity =>
            {
                entity.HasKey(e => e.IdCreditApplication);

                entity.ToTable("Credit_Application");

                entity.HasIndex(e => e.IdCreditApplication, "UQ__Credit_A__389D6E786BAE5D81")
                    .IsUnique();

                entity.Property(e => e.IdCreditApplication)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ID_Credit_Application");

                entity.Property(e => e.ApplicationAmount)
                    .HasColumnType("decimal(38, 2)")
                    .HasColumnName("Application_Amount");

                entity.Property(e => e.CreditApplicationUserId).HasColumnName("CreditApplication_User_ID");

                entity.Property(e => e.CreditDesiredPercentage)
                    .HasColumnType("decimal(38, 2)")
                    .HasColumnName("Credit_Desired_Percentage");

                entity.Property(e => e.StatusId).HasColumnName("Status_ID");
            });

            modelBuilder.Entity<CreditApplicationHistory>(entity =>
            {
                entity.HasKey(e => e.IdCreditApplicationHistory)
                    .HasName("PK__Credit_A__673F6B56D4548F3C");

                entity.ToTable("Credit_Application_History");

                entity.Property(e => e.IdCreditApplicationHistory).HasColumnName("ID_Credit_Application_History");

                entity.Property(e => e.ChangeType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Change_Type");

                entity.Property(e => e.ChangedColumn)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Changed_Column");

                entity.Property(e => e.CreditApplicationId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Credit_Application_ID");

                entity.Property(e => e.DateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_Time");

                entity.Property(e => e.NewValue)
                    .IsUnicode(false)
                    .HasColumnName("New_Value")
                    .HasDefaultValueSql("('-')");

                entity.Property(e => e.OldValue)
                    .IsUnicode(false)
                    .HasColumnName("Old_Value")
                    .HasDefaultValueSql("('-')");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.IdRole);

                entity.ToTable("Role");

                entity.HasIndex(e => e.NameRole, "UQ_Name_Role")
                    .IsUnique();

                entity.Property(e => e.IdRole).HasColumnName("ID_Role");

                entity.Property(e => e.NameRole)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Name_Role");

                entity.Property(e => e.RoleDeleted)
                    .IsRequired()
                    .HasColumnName("Role_Deleted")
                    .HasDefaultValueSql("('1')");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(e => e.IdTransaction);

                entity.ToTable("Transaction");

                entity.Property(e => e.IdTransaction).HasColumnName("ID_Transaction");

                entity.Property(e => e.DateTransaction)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_Transaction");

                entity.Property(e => e.NameTransaction)
                    .IsUnicode(false)
                    .HasColumnName("Name_Transaction");

                entity.Property(e => e.SummTransaction)
                    .HasColumnType("decimal(38, 2)")
                    .HasColumnName("Summ_Transaction");

                entity.Property(e => e.TransactionAccountId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Transaction_Account_ID");

                entity.Property(e => e.TransactionDeleted)
                    .IsRequired()
                    .HasColumnName("Transaction_Deleted")
                    .HasDefaultValueSql("('1')");

                entity.Property(e => e.TransactionTypeId).HasColumnName("Transaction_Type_ID");
            });

            modelBuilder.Entity<TransactionType>(entity =>
            {
                entity.HasKey(e => e.IdTransactionType);

                entity.ToTable("Transaction_Type");

                entity.HasIndex(e => e.NameTransactionType, "UQ_Transaction_Type")
                    .IsUnique();

                entity.Property(e => e.IdTransactionType).HasColumnName("ID_Transaction_Type");

                entity.Property(e => e.NameTransactionType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Name_Transaction_Type");

                entity.Property(e => e.TransactionTypeDeleted)
                    .IsRequired()
                    .HasColumnName("TransactionType_Deleted")
                    .HasDefaultValueSql("('1')");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser);

                entity.ToTable("User");

                entity.Property(e => e.IdUser).HasColumnName("ID_User");

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("First_Name");

                entity.Property(e => e.Login)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Middle_Name")
                    .HasDefaultValueSql("('-')");

                entity.Property(e => e.PassportNumber)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("Passport_Number");

                entity.Property(e => e.PassportSeries)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("Passport_Series");

                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.RoleId)
                    .HasColumnName("Role_ID")
                    .HasDefaultValueSql("('1')");

                entity.Property(e => e.Salt)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SecondName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Second_Name");

                entity.Property(e => e.UserDeleted)
                    .IsRequired()
                    .HasColumnName("User_Deleted")
                    .HasDefaultValueSql("('1')");
            });

            
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
