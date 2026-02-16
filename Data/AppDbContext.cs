using Microsoft.EntityFrameworkCore;
using ParcelDelivery.Api.Models;

namespace ParcelDelivery.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Order> Orders => Set<Order>();
    public DbSet<Parcel> Parcels => Set<Parcel>();
    public DbSet<Recipient> Recipients => Set<Recipient>();

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

        // no Department entity mapping anymore; Department is stored as enum on Parcel
    }
}