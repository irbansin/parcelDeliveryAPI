using Microsoft.EntityFrameworkCore;
using ParcelDelivery.Api.Models;

namespace ParcelDelivery.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Order> Orders => Set<Order>();
    public DbSet<Parcel> Parcels => Set<Parcel>();
    public DbSet<Recipient> Recipients => Set<Recipient>();
    public DbSet<Department> Departments => Set<Department>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Parcels are now separate entities related to Order and Recipient
        modelBuilder.Entity<Order>()
            .HasMany(o => o.Parcels)
            .WithOne(p => p.Order)
            .HasForeignKey(p => p.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Parcel>()
            .HasOne(p => p.Recipient)
            .WithMany(r => r.Parcels)
            .HasForeignKey(p => p.RecipientId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Parcel>()
            .HasOne(p => p.Department)
            .WithMany(d => d.Parcels)
            .HasForeignKey(p => p.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        // seed Departments with stable GUIDs so migrations can map old enum ints to these IDs
        modelBuilder.Entity<Department>().HasData(
            new Department { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "Mail" },
            new Department { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Name = "Regular" },
            new Department { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), Name = "Heavy" }
        );
    }
}