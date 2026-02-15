using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParcelDelivery.Api.Data;
using ParcelDelivery.Api.DAO;
using ParcelDelivery.Api.DTO;
using ParcelDelivery.Api.Models;
using ParcelDelivery.Api.Interfaces;

namespace ParcelDelivery.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentsController : ControllerBase
{
    private readonly IDepartmentDao _dao;
    private readonly AppDbContext _db;

    public DepartmentsController(IDepartmentDao dao, AppDbContext db)
    {
        _dao = dao;
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var list = await _dao.ListAsync();
        return Ok(list);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var d = await _dao.GetByIdAsync(id);
        if (d is null) return NotFound();
        return Ok(d);
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] DepartmentDTO dto)
    {
        var dept = new Department { Id = Guid.NewGuid(), Name = dto.Name };
        await _dao.CreateAsync(dept);
        return CreatedAtAction(nameof(Get), new { id = dept.Id }, dept);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] DepartmentDTO dto)
    {
        var dept = await _dao.GetByIdAsync(id);
        if (dept is null) return NotFound();
        dept.Name = dto.Name;
        await _dao.UpdateAsync(dept);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var dept = await _dao.GetByIdAsync(id);
        if (dept is null) return NotFound();

        // still need to check parcels usage directly on DbContext
        var inUse = await _db.Parcels.AnyAsync(p => p.DepartmentId == id);
        if (inUse) return BadRequest(new { Message = "Department is in use by parcels and cannot be deleted." });

        await _dao.DeleteAsync(id);
        return NoContent();
    }
}
