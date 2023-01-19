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
        public DbSet<Bank> Banks { get; set; }
        public DbSet<Inquiry> Inquiries { get; set; }
        public DbSet<InquirySearch> InquirySearch { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

            modelBuilder.Entity<Inquiry>()
                .Property(x => x.InquireDate)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<InquirySearch>()
                .ToView(nameof(InquirySearch))
                .HasKey(x => x.InquiryID);
        }
    }
}