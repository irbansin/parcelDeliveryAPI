using ParcelDelivery.Api.Models;

namespace ParcelDelivery.Api.DTOs;

public record OrderDTO(
    DateTime ShippingDate,
    OrderType Type,
    List<ParcelDTO> Parcels
);

