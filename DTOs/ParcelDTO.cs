using ParcelDelivery.Api.Models;

namespace ParcelDelivery.Api.DTOs;


public record ParcelDTO(
    double Weight,
    decimal Value,
    string RecipientName,
    Address RecipientAddress,
    ContentType Content
);