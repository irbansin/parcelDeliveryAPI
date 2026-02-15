using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParcelDelivery.Api.Data;
using ParcelDelivery.Api.DTOs;
using ParcelDelivery.Api.Models;

namespace ParcelDelivery.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentsController : ControllerBase
{
    private readonly AppDbContext _db;

    public DepartmentsController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var list = await _db.Departments.AsNoTracking().ToListAsync();
        return Ok(list);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var d = await _db.Departments.FindAsync(id);
        if (d is null) return NotFound();
        return Ok(d);
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] DepartmentDTO dto)
    {
        var dept = new Department { Id = Guid.NewGuid(), Name = dto.Name };
        await _db.Departments.AddAsync(dept);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = dept.Id }, dept);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] DepartmentDTO dto)
    {
        var dept = await _db.Departments.FindAsync(id);
        if (dept is null) return NotFound();
        dept.Name = dto.Name;
        _db.Departments.Update(dept);
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var dept = await _db.Departments.FindAsync(id);
        if (dept is null) return NotFound();

        var inUse = await _db.Parcels.AnyAsync(p => p.DepartmentId == id);
        if (inUse) return BadRequest(new { Message = "Department is in use by parcels and cannot be deleted." });

        _db.Departments.Remove(dept);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
