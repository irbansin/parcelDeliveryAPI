using ParcelDelivery.Api.Models;

namespace ParcelDelivery.Api.Interfaces;

public interface IOrderDao
{
    Task<Order> CreateAsync(Order order);
    Task<Order?> GetByIdAsync(Guid id);
    Task<List<Order>> ListAsync();
}
