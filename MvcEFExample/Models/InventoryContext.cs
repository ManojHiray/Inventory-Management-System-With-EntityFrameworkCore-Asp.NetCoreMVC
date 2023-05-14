using System;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MvcEFExample.Connections;

namespace MvcEFExample.Models
{
    public partial class InventoryContext : DbContext
    {
        public InventoryContext(DbContextOptions<InventoryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Products> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                try
                {


                    var connectionString = Config.GetConnectionString();
                    if (connectionString is not null)
                    {
                        optionsBuilder.UseSqlServer(connectionString);
                    }
                }
                catch(ArgumentNullException ex)
                {
                    // Handle the null argument exception
                    Console.WriteLine($"Error: {ex.Message}");
                }
                catch (SqlException ex)
                {
                    // Handle the SQL exception
                    Console.WriteLine($"Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    // Handle any other exception
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasKey(x => x.ProductId)
                    .HasName("PK__Products__B40CC6CD2C9052CF");

                entity.Property(e => e.Category)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Color)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 0)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
