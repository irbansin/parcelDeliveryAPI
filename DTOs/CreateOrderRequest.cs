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
    string Phone,
    string Street,
    string HouseNo,
    string CityName,
    string CityCode,
    int Pincode,
    ContentType Content
);