using Microsoft.AspNetCore.Mvc;
using ParcelDelivery.Api.DTOs;
using System.Text.Json;
using ParcelDelivery.Api.Models;
using ParcelDelivery.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace ParcelDelivery.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly ParcelDelivery.Api.Services.IParcelClassifier _classifier;

    public OrdersController(AppDbContext db, ParcelDelivery.Api.Services.IParcelClassifier classifier)
    {
        _db = db;
        _classifier = classifier;
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
                    AddressJson = JsonSerializer.Serialize(p.RecipientAddress),
                    Phone = string.Empty
                },
                DepartmentId = _classifier.Classify(p.Weight)
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