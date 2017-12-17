namespace FoodPlace.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class FoodPlaceDbContext : IdentityDbContext<User>
    {
        public DbSet<City> Cities { get; set; }

        public DbSet<Menu> Menus { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Restaurant> Restaurants { get; set; }

        public DbSet<OrderProduct> OrderProducts { get; set; }

        public DbSet<MenuProduct> MenuProducts { get; set; }

        public FoodPlaceDbContext(DbContextOptions<FoodPlaceDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<City>()
                .HasMany(c => c.Restaurants)
                .WithOne(r => r.City)
                .HasForeignKey(r => r.CityId);

            builder
                .Entity<Menu>()
                .HasOne(m => m.Owner)
                .WithMany(o => o.Menus)
                .HasForeignKey(m => m.OwnerId);

            builder
                .Entity<Menu>()
                .HasMany(m => m.Restaurants)
                .WithOne(r => r.Menu)
                .HasForeignKey(r => r.MenuId);

            builder
                .Entity<MenuProduct>()
                .HasKey(mp => new { mp.MenuId, mp.ProductId });

            builder
                .Entity<MenuProduct>()
                .HasOne(mp => mp.Menu)
                .WithMany(p => p.Products)
                .HasForeignKey(mp => mp.ProductId);

            builder
                .Entity<MenuProduct>()
                .HasOne(mp => mp.Product)
                .WithMany(m => m.Menus)
                .HasForeignKey(mp => mp.MenuId);

            builder
                .Entity<Order>()
                .HasOne(o => o.Client)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.ClientId);

            builder
                .Entity<Order>()
                .HasOne(o => o.Restaurant)
                .WithMany(r => r.Orders)
                .HasForeignKey(o => o.RestaurantId);

            builder
                .Entity<OrderProduct>()
                .HasKey(op => new {op.OrderId, op.ProductId});

            builder
                .Entity<OrderProduct>()
                .HasOne(op => op.Order)
                .WithMany(p => p.Products)
                .HasForeignKey(op => op.OrderId);

            builder
                .Entity<OrderProduct>()
                .HasOne(op => op.Product)
                .WithMany(o => o.Orders)
                .HasForeignKey(op => op.ProductId);

            builder
                .Entity<Product>()
                .HasOne(p => p.Owner)
                .WithMany(o => o.Products)
                .HasForeignKey(p => p.OwnerId);

            builder
                .Entity<Restaurant>()
                .HasOne(r => r.Owner)
                .WithMany(o => o.Restaurants)
                .HasForeignKey(r => r.OwnerId);

            base.OnModelCreating(builder);
        }
    }
}
