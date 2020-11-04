using Microsoft.EntityFrameworkCore;
using Restaurant.Back.DAL.MsSqlServer.Models;

namespace Restaurant.Back.DAL.MsSqlServer.Data
{
    public partial class RestaurantBackContext : DbContext
    {
        public RestaurantBackContext()
        {
        }

        public RestaurantBackContext(DbContextOptions<RestaurantBackContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Ingredient> Ingredient { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderPosition> OrderPosition { get; set; }
        public virtual DbSet<OrderStatus> OrderStatus { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductIngredient> ProductIngredient { get; set; }
        public virtual DbSet<Status> Status { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=193.151.56.100,51433;Initial Catalog=RestaurantBack;User Id=makhlin;Password=Init1234;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Title).HasMaxLength(24);
            });

            modelBuilder.Entity<Ingredient>(entity =>
            {
                entity.Property(e => e.Photo)
                    .IsRequired()
                    .HasMaxLength(32);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Comment).HasMaxLength(128);

                entity.Property(e => e.CustomerAddress)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.CustomerPhone)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.DeliveryTime).HasColumnType("date");

                entity.Property(e => e.OrderTime).HasColumnType("date");
            });

            modelBuilder.Entity<OrderPosition>(entity =>
            {
                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderPosition)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderPosi__Order__398D8EEE");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderPosition)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderPosi__Produ__3A81B327");
            });

            modelBuilder.Entity<OrderStatus>(entity =>
            {
                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderStatus)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderStat__Order__36B12243");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.OrderStatus)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderStat__Statu__35BCFE0A");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(128);

                entity.Property(e => e.Photo).HasMaxLength(32);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Product__Categor__276EDEB3");
            });

            modelBuilder.Entity<ProductIngredient>(entity =>
            {
                entity.HasOne(d => d.Ingredient)
                    .WithMany(p => p.ProductIngredient)
                    .HasForeignKey(d => d.IngredientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProductIn__Ingre__2E1BDC42");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductIngredient)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProductIn__Produ__2F10007B");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.Property(e => e.Time).HasColumnType("date");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
