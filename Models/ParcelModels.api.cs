namespace ParcelDelivery.Api.Models;

public enum OrderType { International, Domestic }
public enum AddressType { Home, Office, Custom }
public enum ContentType { Household, Electronics, Food, HouseholdItems }

public enum Department { Mail = 0, Regular = 1, Heavy = 2 , Insurance = 3 }

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

public class Recipient
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    // store the address as JSON in the DB for now; we may split Address into its own table later
    public string? AddressJson { get; set; }
    public string Phone { get; set; } = default!;

    // navigation
    public ICollection<Parcel>? Parcels { get; set; }
}

public class Parcel
{
    public Guid Id { get; set; }
    public double Weight { get; set; }
    public decimal Value { get; set; }

    public bool Approved { get; set; } = true;


    // foreign keys
    public Guid? RecipientId { get; set; }
    public Recipient? Recipient { get; set; }

    public Department Department { get; set; } = Department.Regular;
    public ContentType Content { get; set; }

    // relation to Order
    public Guid? OrderId { get; set; }
    public Order? Order { get; set; }
}

public record Order
{
    public Guid Id { get; init; }
    public DateTime ShippingDate { get; init; }
    public OrderType Type { get; init; }
    public List<Parcel> Parcels { get; init; } = new();
}