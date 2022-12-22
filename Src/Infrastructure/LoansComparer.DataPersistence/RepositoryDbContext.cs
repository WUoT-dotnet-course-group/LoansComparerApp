﻿using LoansComparer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LoansComparer.DataPersistence
{
    public sealed class RepositoryDbContext : DbContext
    {
        public RepositoryDbContext(DbContextOptions<RepositoryDbContext> options)
            : base(options)
        {
        }

        public DbSet<Inquiry> Inquiries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .OwnsOne(
                x => x.PersonalData,
                pd =>
                {
                    pd.Property(y => y.FirstName).HasColumnName("FirstName");
                    pd.Property(y => y.LastName).HasColumnName("LastName");
                    pd.Property(y => y.GovernmentId).HasColumnName("GovernmentId");
                    pd.Property(y => y.GovernmentIdType).HasColumnName("GovernmentIdType");
                });

            modelBuilder.Entity<Inquiry>()
                .Property(x => x.InquireDate)
                .HasDefaultValueSql("getdate()");
        }
    }
}