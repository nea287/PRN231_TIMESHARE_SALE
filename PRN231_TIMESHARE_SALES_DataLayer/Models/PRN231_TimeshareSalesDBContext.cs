using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace PRN231_TIMESHARE_SALES_DataLayer.Models
{
    public partial class PRN231_TimeshareSalesDBContext : DbContext
    {
        public PRN231_TimeshareSalesDBContext()
        {
        }

        public PRN231_TimeshareSalesDBContext(DbContextOptions<PRN231_TimeshareSalesDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<AvailableTime> AvailableTimes { get; set; } = null!;
        public virtual DbSet<Contract> Contracts { get; set; } = null!;
        public virtual DbSet<CustomerRequest> CustomerRequests { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Facility> Facilities { get; set; } = null!;
        public virtual DbSet<Feedback> Feedbacks { get; set; } = null!;
        public virtual DbSet<Owner> Owners { get; set; } = null!;
        public virtual DbSet<Project> Projects { get; set; } = null!;
        public virtual DbSet<Reservation> Reservations { get; set; } = null!;
        public virtual DbSet<UsageHistory> UsageHistories { get; set; } = null!;
        public virtual DbSet<UsageRight> UsageRights { get; set; } = null!;
        public virtual DbSet<StaffOfProject> StaffOfProjects { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(GetConnectionString());
            }
        }

        private string GetConnectionString()
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            var strConn = config.GetConnectionString("TimeShareSalesDB");
            return strConn;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.HasIndex(e => e.Email, "UQ_Email")
                    .IsUnique();

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.Country).HasMaxLength(50);

                entity.Property(e => e.Email)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.FullName).HasMaxLength(150);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Password)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.State).HasMaxLength(50);



            });

            modelBuilder.Entity<StaffOfProject>(entity =>
            {
                entity.HasKey(sop => new { sop.StaffId, sop.ProjectId });
                entity.ToTable("StaffOfProject");

                entity.HasOne(d => d.Account)
                .WithMany(p => p.StaffOfProjects)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("FK_StaffOfProject_Account");


                entity.HasOne(d => d.Project)
                .WithMany(p => p.StaffOfProjects)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_StaffOfProject_Project");


            });

            modelBuilder.Entity<AvailableTime>(entity =>
            {
                entity.ToTable("AvailableTime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.AvailableTimes)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK_AvailableTime_Department");
            });

            modelBuilder.Entity<Contract>(entity =>
            {
                entity.ToTable("Contract");

                entity.Property(e => e.CommissionAmount).HasColumnType("money");

                entity.Property(e => e.ContractAmount).HasColumnType("money");

                entity.Property(e => e.ContractDate).HasColumnType("datetime");

                entity.Property(e => e.ContractName).HasMaxLength(150);

                entity.Property(e => e.ContractTerm).HasColumnType("datetime");

                entity.Property(e => e.RegularPaymentAmount).HasColumnType("money");

                entity.Property(e => e.RegularPaymentDate).HasColumnType("datetime");

                entity.HasOne(d => d.AvailableTime)
                    .WithMany(p => p.Contracts)
                    .HasForeignKey(d => d.AvailableTimeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contract_AvailableTime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.ContractCustomers)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contract_Account1");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.ContractStaffs)
                    .HasForeignKey(d => d.StaffId)
                    .HasConstraintName("FK_Contract_Account");
            });

            modelBuilder.Entity<CustomerRequest>(entity =>
            {
                entity.ToTable("CustomerRequest");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.RequestDate).HasColumnType("datetime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerRequests)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerRequest_Account");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.CustomerRequests)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerRequest_Department");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.Country).HasMaxLength(50);

                entity.Property(e => e.DepartmentName).HasMaxLength(150);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.State).HasMaxLength(50);

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.Departments)
                    .HasForeignKey(d => d.OwnerId)
                    .HasConstraintName("FK_Department_Owner");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Departments)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_Department_Project");
            });

            modelBuilder.Entity<Facility>(entity =>
            {
                entity.ToTable("Facility");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.FacilityName).HasMaxLength(150);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Facilities)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK_Facility_Department");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.ToTable("Feedback");

                entity.Property(e => e.Content).HasColumnType("text");

                entity.Property(e => e.FeedbackDate).HasColumnType("datetime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Feedback_Account");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Feedback_Department");
            });

            modelBuilder.Entity<Owner>(entity =>
            {
                entity.ToTable("Owner");

                entity.Property(e => e.ContactPerson).HasMaxLength(150);

                entity.Property(e => e.Email)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.OwnerName).HasMaxLength(150);

                entity.Property(e => e.Phone)
                    .HasMaxLength(13)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("Project");

                entity.HasIndex(e => e.ProjectCode, "UQ_Project_Code")
                    .IsUnique();

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.ProjectCode).HasMaxLength(50);

                entity.Property(e => e.ProjectName).HasMaxLength(150);

                entity.Property(e => e.RegistrationEndDate).HasColumnType("datetime");

                entity.Property(e => e.RegistrationOpeningDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");



            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.ToTable("Reservation");

                entity.Property(e => e.ReservationDate).HasColumnType("datetime");

                entity.Property(e => e.ReservationFee).HasColumnType("money");

                entity.HasOne(d => d.AvailableTime)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.AvailableTimeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reservation_AvailableTime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reservation_Account");
            });

            modelBuilder.Entity<UsageHistory>(entity =>
            {
                entity.HasKey(e => e.UsageId);

                entity.ToTable("UsageHistory");

                entity.Property(e => e.CheckInDate).HasColumnType("datetime");

                entity.Property(e => e.CheckOutDate).HasColumnType("datetime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.UsageHistories)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UsageHistory_Account");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.UsageHistories)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UsageHistory_Department");
            });

            modelBuilder.Entity<UsageRight>(entity =>
            {
                entity.ToTable("UsageRight");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.UsageRights)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UsageRight_Account");

                entity.HasOne(d => d.Reservation)
                    .WithMany(p => p.UsageRights)
                    .HasForeignKey(d => d.ReservationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UsageRight_Reservation");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
