using Microsoft.EntityFrameworkCore;
using ParcelDelivery.Api.Data;
using ParcelDelivery.Api.Interfaces;
using ParcelDelivery.Api.Models;

namespace ParcelDelivery.Api.DAO;

public class OrderDao : IOrderDao
{
    private readonly AppDbContext _db;
    public OrderDao(AppDbContext db) => _db = db;

    public async Task<Order> CreateAsync(Order order)
    {
        if (order.Parcels != null)
        {
            foreach (var p in order.Parcels)
            {
                if (p.Recipient != null && p.Recipient.Id == Guid.Empty)
                {
                    p.Recipient.Id = Guid.NewGuid();
                }
            }
        }

        await _db.Orders.AddAsync(order);
        await _db.SaveChangesAsync();
        return order;
    }

    public async Task<Order?> GetByIdAsync(Guid id)
    {
        return await _db.Orders
            .Include(o => o.Parcels)
                .ThenInclude(p => p.Recipient)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<List<Order>> ListAsync()
    {
        return await _db.Orders
            .Include(o => o.Parcels)
                .ThenInclude(p => p.Recipient)
            .ToListAsync();
    }
}
