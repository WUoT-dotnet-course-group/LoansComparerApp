using LoansComparer.CrossCutting.Enums;
using LoansComparer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LoansComparer.DataPersistence
{
    public sealed class RepositoryDbContext : DbContext
    {
        public RepositoryDbContext(DbContextOptions<RepositoryDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Inquiry> Inquiries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Users table

            modelBuilder.Entity<User>()
                .OwnsOne(
                x => x.PersonalData,
                pd =>
                {
                    pd.Property(y => y.FirstName).HasColumnName("FirstName");
                    pd.Property(y => y.LastName).HasColumnName("LastName");
                    pd.Property(y => y.BirthDate).HasColumnName("BirthDate");
                    pd.Property(y => y.GovernmentId).HasColumnName("GovernmentId");
                    pd.Property(y => y.GovernmentIdType).HasColumnName("GovernmentIdType");
                    pd.Property(y => y.JobType).HasColumnName("JobType");
                    pd.Property(y => y.JobStartDate).HasColumnName("JobStartDate");
                    pd.Property(y => y.JobEndDate).HasColumnName("JobEndDate");
                });

            modelBuilder.Entity<User>()
                .Property(x => x.Role)
                .HasDefaultValue(UserRole.Debtor);

            modelBuilder.Entity<User>()
                .HasIndex(x => x.ID)
                .IsClustered(true);

            modelBuilder.Entity<User>()
                .HasIndex(x => x.Email);

            #endregion Users table

            #region Inquiries table

            modelBuilder.Entity<Inquiry>()
                .Property(x => x.InquireDate)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Inquiry>()
                .HasIndex(x => x.ID)
                .IsClustered(true);

            modelBuilder.Entity<Inquiry>()
                .HasIndex(x => x.UserID);

            modelBuilder.Entity<Inquiry>()
                .HasIndex(x => new { x.ChosenBankId, x.ChosenOfferId });

            #endregion Inquiries table
        }
    }
}