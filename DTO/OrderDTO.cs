using ParcelDelivery.Api.Models;

namespace ParcelDelivery.Api.DTO;

public record OrderDTO(
    DateTime ShippingDate,
    OrderType Type,
    List<ParcelDTO> Parcels
);

