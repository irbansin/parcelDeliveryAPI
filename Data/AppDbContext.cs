using Microsoft.EntityFrameworkCore;
using ParcelDelivery.Api.Models;

namespace ParcelDelivery.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Order> Orders => Set<Order>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<Order>().OwnsMany(o => o.Parcels, p => {
            p.ToJson(); 
            p.OwnsOne(parcel => parcel.Recipient, r => {
                r.OwnsOne(recipient => recipient.Address, a => {
                    a.OwnsOne(address => address.City);
                });
            });
        });
    }
}