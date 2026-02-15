using Microsoft.EntityFrameworkCore;
using ParcelDelivery.Api.Data;
using ParcelDelivery.Api.Interfaces;
using ParcelDelivery.Api.Models;

namespace ParcelDelivery.Api.DAO;

public class DepartmentDao : IDepartmentDao
{
    private readonly AppDbContext _db;
    public DepartmentDao(AppDbContext db) => _db = db;

    public async Task<List<Department>> ListAsync()
    {
        return await _db.Departments.AsNoTracking().ToListAsync();
    }

    public async Task<Department?> GetByIdAsync(Guid id)
    {
        return await _db.Departments.FindAsync(id);
    }

    public async Task<Department> CreateAsync(Department dept)
    {
        await _db.Departments.AddAsync(dept);
        await _db.SaveChangesAsync();
        return dept;
    }

    public async Task UpdateAsync(Department dept)
    {
        _db.Departments.Update(dept);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var d = await _db.Departments.FindAsync(id);
        if (d is null) return;
        _db.Departments.Remove(d);
        await _db.SaveChangesAsync();
    }
}
