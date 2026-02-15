using Microsoft.AspNetCore.Mvc;
using ParcelDelivery.Api.DTOs;
using ParcelDelivery.Api.Models;

namespace ParcelDelivery.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    [HttpPost]
    public IActionResult CreateOrder([FromBody] CreateOrderRequest request)
    {
        return Ok(new { 
            Message = "Order received!", 
            ParcelsCount = request.Parcels.Count,
            ReceivedAt = DateTime.Now 
        });
    }

   
}