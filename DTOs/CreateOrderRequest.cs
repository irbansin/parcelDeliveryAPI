public record CreateOrderRequest(
    DateTime OrderDate,
    DateTime PickupDate,
    DateTime ShippingDate,
    DateTime DeliveryDate,
    string OrderType,
    List<ParcelDto> Parcels
);
