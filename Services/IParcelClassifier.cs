using ParcelDelivery.Api.Models;

namespace ParcelDelivery.Api.Services;

public interface IParcelClassifier
{
    Guid Classify(double weightKg);
}
