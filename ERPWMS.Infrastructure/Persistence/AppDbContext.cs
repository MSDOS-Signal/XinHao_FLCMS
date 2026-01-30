using ERPWMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ERPWMS.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<QualityCheck> QualityChecks { get; set; }
        public DbSet<ProductionPlan> ProductionPlans { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<BomItem> BomItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<WorkOrder> WorkOrders { get; set; }
        public DbSet<Device> Devices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure schemas according to architecture requirements
            modelBuilder.Entity<Product>().ToTable("Products", "erp"); // Basic product info usually in ERP
            modelBuilder.Entity<Order>().ToTable("Orders", "erp");
            
            modelBuilder.Entity<Inventory>().ToTable("Inventories", "wms");
            
            modelBuilder.Entity<WorkOrder>().ToTable("WorkOrders", "mes");
            modelBuilder.Entity<BomItem>().ToTable("BomItems", "bom");
            
            modelBuilder.Entity<Device>().ToTable("Devices", "scada");
            
            modelBuilder.Entity<Supplier>().ToTable("Suppliers", "srm");
            
            modelBuilder.Entity<Shipment>().ToTable("Shipments", "tms");
            
            modelBuilder.Entity<QualityCheck>().ToTable("QualityChecks", "qms");
            
            modelBuilder.Entity<ProductionPlan>().ToTable("ProductionPlans", "aps");
            
            modelBuilder.Entity<Asset>().ToTable("Assets", "eam");
            
            modelBuilder.Entity<Operation>().ToTable("Operations", "mom");
        }
    }
}
