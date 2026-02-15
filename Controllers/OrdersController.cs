using Microsoft.AspNetCore.Mvc;
using ParcelDelivery.Api.DTOs;
using ParcelDelivery.Api.Models;
using ParcelDelivery.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace ParcelDelivery.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly AppDbContext _db;

    public OrdersController(AppDbContext db)
    {
        _db = db;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] OrderDTO request)
    {
        var order = new Order
        {
            Id = Guid.NewGuid(),
            ShippingDate = request.ShippingDate,
            Type = request.Type,
            Parcels = request.Parcels.Select(p => new Parcel
            {
                Id = Guid.NewGuid(),
                Weight = p.Weight,
                Value = p.Value,
                Content = p.Content,
                Recipient = new Recipient
                {
                    Id = Guid.NewGuid(),
                    Name = p.RecipientName,
                    Address = p.RecipientAddress,
                    Phone = string.Empty
                }
            }).ToList()
        };

        await _db.Orders.AddAsync(order);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(CreateOrder), new { id = order.Id }, new {
            Message = "Order created",
            OrderId = order.Id,
            ParcelsCount = order.Parcels.Count,
            ReceivedAt = DateTime.Now
        });
    }

}