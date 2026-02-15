using ParcelDelivery.Api.Models;

namespace ParcelDelivery.Api.Interfaces;

public interface IDepartmentDao
{
    Task<List<Department>> ListAsync();
    Task<Department?> GetByIdAsync(Guid id);
    Task<Department> CreateAsync(Department dept);
    Task UpdateAsync(Department dept);
    Task DeleteAsync(Guid id);
}
