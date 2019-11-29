using DesafioVerity.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace DesafioVerity.Repository.Infrastructure
{
    public class Context : DbContext
    {

        public Context(DbContextOptions<Context> options): base(options)
        {

        }

        public DbSet<Account> Account { get; set; }
        public DbSet<AccountHolder> AccountHolder { get; set; }
        public DbSet<Extract> Extract { get; set; }
        public DbSet<TransactionType> TransactionType { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.AccountHolderId);
                entity.Property(e => e.UpdateDate).IsRequired();
                entity.Property(e => e.Value).IsRequired().HasColumnType("decimal(10,2)");

                entity.HasOne(d => d.AccountHolder).WithOne(p => p.Account);
            });

            modelBuilder.Entity<AccountHolder>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Password).IsRequired();
                entity.Property(e => e.Agency).IsRequired();
                entity.Property(e => e.AccountNumber).IsRequired();
                entity.Property(e => e.CreateDate).IsRequired();

                entity.HasOne(d => d.Account).WithOne(p => p.AccountHolder);
            });

            modelBuilder.Entity<Extract>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.TransactionDate).IsRequired();
                entity.Property(e => e.TransactionTypeId).IsRequired();
                entity.Property(e => e.Value).IsRequired().HasColumnType("decimal(10,2)");
                entity.Property(e => e.AccountHolderId).IsRequired();

                entity.HasOne(d => d.AccountHolder).WithMany(p => p.Extracts);
                entity.HasOne(d => d.TransactionType).WithMany(p => p.Extracts);
            });

            modelBuilder.Entity<TransactionType>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Description).IsRequired().HasMaxLength(20); ;
            });

        }

    }

}
