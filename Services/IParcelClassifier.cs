using ParcelDelivery.Api.Models;

namespace ParcelDelivery.Api.Services;

public interface IParcelClassifier
{
    Department Classify(double weightKg);
}
