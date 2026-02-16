using Microsoft.AspNetCore.Mvc;
using ParcelDelivery.Api.DTO;
using System.Text.Json;
using ParcelDelivery.Api.Models;
using ParcelDelivery.Api.Interfaces;
namespace ParcelDelivery.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderDao _orderDao;
    private readonly IParcelClassifier _classifier;

    public OrdersController(IOrderDao orderDao, IParcelClassifier classifier)
    {
        _orderDao = orderDao;
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
                    Department = _classifier.ClassifyDepartment(p.Weight)
            }).ToList()
        };

            await _orderDao.CreateAsync(order);

        return CreatedAtAction(nameof(CreateOrder), new { id = order.Id }, new {
            Message = "Order created",
            OrderId = order.Id,
            ParcelsCount = order.Parcels.Count,
            ReceivedAt = DateTime.Now
        });
    }

}