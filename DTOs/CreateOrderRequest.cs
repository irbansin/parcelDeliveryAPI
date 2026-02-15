using ParcelDelivery.Api.Models;

namespace ParcelDelivery.Api.DTOs;

public record CreateOrderRequest(
    DateTime ShippingDate,
    OrderType Type,
    List<ParcelInput> Parcels
);

public record ParcelInput(
    double Weight,
    decimal Value,
    string RecipientName,
    Address RecipientAddress,
    ContentType Content
);