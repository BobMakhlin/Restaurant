using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Restaurant.Front.DAL.MsSqlServer.Models;

namespace Restaurant.Front.DAL.MsSqlServer.Data
{
    public partial class RestaurantFrontContext : DbContext
    {
        public RestaurantFrontContext()
        {
        }

        public RestaurantFrontContext(DbContextOptions<RestaurantFrontContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Ingredient> Ingredient { get; set; }
        public virtual DbSet<Label> Label { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductIngredient> ProductIngredient { get; set; }
        public virtual DbSet<ProductLabel> ProductLabel { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasIndex(e => e.Title)
                    .HasName("UQ__Category__2CB664DC5A12D8FA")
                    .IsUnique();
            });

            modelBuilder.Entity<Product>(entity =>
            {
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
                    .HasConstraintName("FK__ProductIn__Ingre__32E0915F");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductIngredient)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProductIn__Produ__33D4B598");
            });

            modelBuilder.Entity<ProductLabel>(entity =>
            {
                entity.HasOne(d => d.Label)
                    .WithMany(p => p.ProductLabel)
                    .HasForeignKey(d => d.LabelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProductLa__Label__2C3393D0");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductLabel)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProductLa__Produ__2D27B809");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
