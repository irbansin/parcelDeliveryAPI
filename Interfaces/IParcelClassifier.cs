using ParcelDelivery.Api.Models;

namespace ParcelDelivery.Api.Interfaces;

public interface IParcelClassifier
{
    Guid Classify(double weightKg);
}
