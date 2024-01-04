using Microsoft.EntityFrameworkCore;
using Npgsql;
using Comm.Core.src.Entities;

namespace Comm.WebAPI.src.Database
{
    public class DatabaseContext : DbContext // builder pattern
    {
        private readonly IConfiguration _config;
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Adresses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Avatar> Avatars { get; set; }
        public DbSet<CategoryImage> CategoryImages { get; set; }
        public DbSet<Category> Categories { get; set; }
        static DatabaseContext()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public DatabaseContext(DbContextOptions options, IConfiguration config) : base(options)
        {
            _config = config;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresEnum<Role>();

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Role).HasColumnType("role");
                entity.HasData(SeedingData.GetUsers());
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasData(SeedingData.GetProducts());
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasData(SeedingData.GetCategories());
            });


            modelBuilder.Entity<CategoryImage>(entity =>
            {
                entity.HasData(SeedingData.GetCategoryImages());
            });

            modelBuilder.Entity<Avatar>(entity =>
            {
                entity.HasData(SeedingData.GetAvatars());
            });

            modelBuilder.Entity<Image>(entity =>
           {
               entity.HasData(SeedingData.GetImages());
           });

            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasData(SeedingData.GetAddresses());
            });



            modelBuilder.HasPostgresEnum<Status>();
            modelBuilder.Entity<Order>(entity => entity.Property(o => o.Status).HasColumnType("status"));

            modelBuilder.Entity<OrderProduct>().HasKey(o => new { o.ProductId, o.OrderId });

            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

        }
    }
}