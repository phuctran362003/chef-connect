using Microsoft.EntityFrameworkCore;

namespace ChefConnect.Domain.Entities
{
    public class ChefConnectDbContext : DbContext
    {
        public ChefConnectDbContext()
        {
        }
        public ChefConnectDbContext(DbContextOptions<ChefConnectDbContext> options) : base(options) { }

        // DbSets for tables
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ChefInformation> ChefInformations { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Explicitly map derived entities to separate tables (TPT strategy)
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<ChefInformation>().ToTable("ChefInformations");
            modelBuilder.Entity<Menu>().ToTable("Menus");
            modelBuilder.Entity<Dish>().ToTable("Dishes");
            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.Entity<OrderItem>().ToTable("OrderItems");
            modelBuilder.Entity<Payment>().ToTable("Payments");

            // Configure BaseEntity properties as default for all derived entities
            modelBuilder.Entity<User>().Property(u => u.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            modelBuilder.Entity<User>().Property(u => u.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");

            // Relationships
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ChefInformation>()
                .HasOne(c => c.User)
                .WithOne()
                .HasForeignKey<ChefInformation>(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Menu>()
                .HasOne(m => m.Chef)
                .WithMany()
                .HasForeignKey(m => m.ChefId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Dish>()
                .HasOne(d => d.Menu)
                .WithMany()
                .HasForeignKey(d => d.MenuId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany()
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Chef)
                .WithMany()
                .HasForeignKey(o => o.ChefId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Dish)
                .WithMany()
                .HasForeignKey(oi => oi.DishId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Order)
                .WithMany()
                .HasForeignKey(p => p.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            // Unique Constraints
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Role>()
                .HasIndex(r => r.Name)
                .IsUnique();
        }



    }
}
