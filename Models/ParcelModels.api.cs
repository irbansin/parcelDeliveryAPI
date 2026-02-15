namespace ParcelDelivery.Api.Models;

public enum OrderType { International, Domestic }
public enum AddressType { Home, Office, Custom }
public enum ContentType { Household, Electronics, Food, HouseholdItems }

public record City
{
    public string Name { get; init; } = default!;
    public string Code { get; init; } = default!;
    public int Pincode { get; init; }
}

public record Address
{
    public Guid Id { get; init; }
    public AddressType Type { get; init; }
    public string Street { get; init; } = default!;
    public string HouseNo { get; init; } = default!;
    public City City { get; init; } = default!;
    public string? CustomAddressType { get; init; }
}

public record Recipient
{
    public Guid Id { get; init; }
    public string Name { get; init; } = default!;
    public Address Address { get; init; } = default!;
    public string Phone { get; init; } = default!;
}

public record Parcel
{
    public Guid Id { get; init; }
    public double Weight { get; init; }
    public decimal Value { get; init; }
    public Recipient Recipient { get; init; } = default!;
    public ContentType Content { get; init; }
}

public record Order
{
    public Guid Id { get; init; }
    public DateTime ShippingDate { get; init; }
    public OrderType Type { get; init; }
    public List<Parcel> Parcels { get; init; } = new();
}