using Microsoft.EntityFrameworkCore;
using StockFlowAPI.Models;

namespace StockFlowAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Material> Materials { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<InventoryMovement> InventoryMovements { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleItem> SaleItems { get; set; }
        public DbSet<AccountPayable> AccountsPayable { get; set; }
        public DbSet<AccountReceivable> AccountsReceivable { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Material
            modelBuilder.Entity<Material>(entity =>
            {
                entity.ToTable("Materials");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasIndex(e => e.Name) //  Nome único
                    .IsUnique();

                entity.Property(e => e.Description)
                    .HasMaxLength(255);

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(10,2)")
                    .IsRequired();

                entity.Property(e => e.Quantity)
                    .IsRequired();

                entity.Property(e => e.Supplier)
                    .HasMaxLength(100);

                entity.Property(e => e.Size) 
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Color) 
                    .IsRequired()
                    .HasMaxLength(20);
            });

            // inventário
            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.ToTable("Inventory");
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.Material)
                      .WithMany()
                      .HasForeignKey(e => e.MaterialId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.Property(e => e.Quantity)
                      .IsRequired();
            });

            modelBuilder.Entity<InventoryMovement>(entity =>
            {
                entity.ToTable("InventoryMovements");
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.Material)
                      .WithMany()
                      .HasForeignKey(e => e.MaterialId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.Property(e => e.Type)
                      .IsRequired();

                entity.Property(e => e.Quantity)
                      .IsRequired();

                entity.Property(e => e.Description)
                      .HasMaxLength(200);

                entity.Property(e => e.Date)
                      .HasColumnType("datetime")
                      .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            // venda
            modelBuilder.Entity<Sale>(entity =>
            {
                entity.ToTable("Sales");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.CustomerName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Status)
                    .HasConversion<string>()
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.Total).HasColumnType("decimal(10,2)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            // item de venda
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

            // conta a pagar
            modelBuilder.Entity<AccountPayable>(entity =>
            {
                entity.ToTable("AccountsPayable");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(10,2)")
                    .IsRequired();

                entity.Property(e => e.DueDate).IsRequired();
                entity.Property(e => e.IsPaid).IsRequired();

                entity.Property(e => e.CreatedAt)
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

            });

            // conta a receber
            modelBuilder.Entity<AccountReceivable>(entity =>
            {
                entity.ToTable("AccountsReceivable");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(10,2)")
                    .IsRequired();

                entity.Property(e => e.DueDate)
                    .IsRequired();

                entity.Property(e => e.IsReceived)
                    .IsRequired();

                entity.HasOne(e => e.Sale)
                    .WithMany()
                    .HasForeignKey(e => e.SaleId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // funcionarios
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employees");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Salary)
                    .HasColumnType("decimal(10,2)")
                    .IsRequired();

                entity.Property(e => e.IsActive) 
                    .IsRequired()
                    .HasDefaultValue(true);
            });
        }
    }
}
