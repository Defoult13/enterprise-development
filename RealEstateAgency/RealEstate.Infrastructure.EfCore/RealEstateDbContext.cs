using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.DataSeeder;
using RealEstate.Domain.Models;

namespace RealEstate.Infrastructure.EfCore;

/// <summary>
/// EF Core database context for the RealEstate application.
/// Configures PostgreSQL naming, keys, relationships, and column constraints.
/// </summary>
/// <param name="options">DbContext options.</param>
public class RealEstateDbContext(DbContextOptions<RealEstateDbContext> options, RealEstateDataSeeder dataSeeder) : DbContext(options)
{
    /// <summary>
    /// Counterparties (clients) table access.
    /// </summary>
    public DbSet<Counterparty> Counterparties => Set<Counterparty>();

    /// <summary>
    /// Real-estate objects table access.
    /// </summary>
    public DbSet<RealEstateObject> RealEstateObjects => Set<RealEstateObject>();

    /// <summary>
    /// Client buy/sell requests table access.
    /// </summary>
    public DbSet<RealEstateRequest> Requests => Set<RealEstateRequest>();

    /// <summary>
    /// Configures EF Core model mapping: tables, columns, keys, constraints, and relationships.
    /// </summary>
    /// <param name="modelBuilder">Model builder used to configure entity mappings.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Counterparty>(entity =>
        {
            entity.ToTable("counterparty");

            entity.HasKey(e => e.Id).HasName("pk_counterparty");

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .UseIdentityByDefaultColumn();

            entity.Property(e => e.FullName)
                .HasColumnName("full_name")
                .HasMaxLength(200)
                .IsRequired();

            entity.Property(e => e.PassportNumber)
                .HasColumnName("passport_number")
                .HasMaxLength(20)
                .IsRequired();

            entity.Property(e => e.Phone)
                .HasColumnName("phone")
                .HasMaxLength(32)
                .IsRequired();

            entity.HasIndex(e => e.PassportNumber)
                .HasDatabaseName("ix_counterparty_passport_number");

            entity.HasData(dataSeeder.Counterparties);
        });

        modelBuilder.Entity<RealEstateObject>(entity =>
        {
            entity.ToTable("real_estate_object");

            entity.HasKey(e => e.Id).HasName("pk_real_estate_object");

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .UseIdentityByDefaultColumn();

            entity.Property(e => e.Type)
                .HasColumnName("type")
                .IsRequired();

            entity.Property(e => e.Purpose)
                .HasColumnName("purpose")
                .IsRequired();

            entity.Property(e => e.CadastralNumber)
                .HasColumnName("cadastral_number")
                .HasMaxLength(32)
                .IsRequired();

            entity.Property(e => e.Address)
                .HasColumnName("address")
                .HasMaxLength(300)
                .IsRequired();

            entity.Property(e => e.FloorsTotal)
                .HasColumnName("floors_total")
                .IsRequired();

            entity.Property(e => e.TotalAreaSqM)
                .HasColumnName("total_area")
                .HasPrecision(12, 2)
                .IsRequired();

            entity.Property(e => e.Rooms)
                .HasColumnName("rooms")
                .IsRequired();

            entity.Property(e => e.CeilingHeightM)
                .HasColumnName("ceiling_height")
                .HasPrecision(4, 2);

            entity.Property(e => e.Floor)
                .HasColumnName("floor");

            entity.Property(e => e.HasEncumbrances)
                .HasColumnName("has_encumbrances")
                .IsRequired();

            entity.HasData(dataSeeder.Properties);
        });

        modelBuilder.Entity<RealEstateRequest>(entity =>
        {
            entity.ToTable("real_estate_request");

            entity.HasKey(e => e.Id).HasName("pk_real_estate_request");

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .UseIdentityByDefaultColumn();

            entity.Property(e => e.ClientId)
                .HasColumnName("client_id")
                .IsRequired();

            entity.Property(e => e.PropertyId)
                .HasColumnName("property_id")
                .IsRequired();

            entity.HasOne(e => e.Client)
                .WithMany()
                .HasForeignKey(e => e.ClientId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_real_estate_request_client");

            entity.HasOne(e => e.Property)
                .WithMany()
                .HasForeignKey(e => e.PropertyId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_real_estate_request_property");

            entity.Property(e => e.Type)
                .HasColumnName("type")
                .IsRequired();

            entity.Property(e => e.Amount)
                .HasColumnName("amount")
                .HasPrecision(18, 2)
                .IsRequired();

            entity.Property(e => e.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("date")
                .IsRequired();

            entity.HasIndex(e => e.ClientId)
                .HasDatabaseName("ix_real_estate_request_client_id");

            entity.HasIndex(e => e.PropertyId)
                .HasDatabaseName("ix_real_estate_request_property_id");

            entity.HasData(dataSeeder.Requests);
        });
    }
}