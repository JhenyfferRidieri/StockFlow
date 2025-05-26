using Microsoft.EntityFrameworkCore;
using StockFlowAPI.Models;

namespace StockFlowAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Material> Materials { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleItem> SaleItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Material
            modelBuilder.Entity<Material>(entity =>
            {
                entity.ToTable("Materials");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(255);
            });

            // Inventory
            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.ToTable("Inventory");
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.Material)
                    .WithMany(m => m.Inventory)
                    .HasForeignKey(e => e.MaterialId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Sale
            modelBuilder.Entity<Sale>(entity =>
            {
                entity.ToTable("Sales");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.CustomerName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Status).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Total).HasColumnType("decimal(10,2)");
            });

            // SaleItem
            modelBuilder.Entity<SaleItem>(entity =>
            {
                entity.ToTable("SaleItems");
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.Sale)
                    .WithMany(s => s.SaleItems)
                    .HasForeignKey(e => e.SaleId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Material)
                    .WithMany(m => m.SaleItems)
                    .HasForeignKey(e => e.MaterialId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(10,2)");
                entity.Property(e => e.Quantity).IsRequired();
            });
        }

    }
}
