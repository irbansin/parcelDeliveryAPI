namespace ParcelDelivery.Api.Models;

public enum OrderType { International, Domestic }
public enum AddressType { Home, Office, Custom }
public enum ContentType { Household, Electronics, Food, HouseholdItems }

// Using 'record' for concise data structures
public record City(string Name, string Code, int Pincode);
public record Address(Guid Id, AddressType Type, string Street, string HouseNo, City City, string? CustomAddressType = null);
public record Recipient(Guid Id, string Name, Address Address, string Phone);
public record Parcel(Guid Id, double Weight, decimal Value, Recipient Recipient, ContentType Content);
public record Order(Guid Id, DateTime ShippingDate, OrderType Type, List<Parcel> Parcels);