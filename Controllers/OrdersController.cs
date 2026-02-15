using Microsoft.AspNetCore.Mvc;
using ParcelDelivery.Api.DTOs;

namespace ParcelDelivery.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    [HttpPost]
    public IActionResult CreateOrder([FromBody] CreateOrderRequest request)
    {
        // For now, we just acknowledge the data
        return Ok(new { 
            Message = "Order received!", 
            ParcelsCount = request.Parcels.Count,
            ReceivedAt = DateTime.Now 
        });
    }
}